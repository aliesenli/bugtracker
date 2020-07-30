import axios from 'axios'
const BASE_URL = process.env.VUE_APP_BASEURL

const state = {
    projectTickets: [],
    projectDetails: {},
    staffs: []
};

const getters = {
    projectTickets: (state) => state.projectTickets,
    projectDetails: (state) => state.projectDetails,
    staffs: (state) => state.staffs
};

const actions = {
    async fetchProjectTickets({ commit }, projectId) {
        const response = await axios(BASE_URL + `/api/projects/${projectId}`);
        commit('setProjectDetails', response.data)
    },

    createTicket({commit}, {vm ,payload}){
        return new Promise((resolve, reject) => {
            axios.post(BASE_URL + `/api/tickets/create`, {
                title: payload.title, 
                description: payload.description, 
                priority: payload.priority, 
                assigneeId: payload.assigneeId, 
                projectId: payload.projectId 
            })
            .then(response => {
                commit('addTicket', response.data);
                resolve(response)
            })
            .catch(error => {
                vm.$bvToast.toast(`insufficient permissions`, {
                    title : 'Ticket not created',
                    variant : 'danger'
                })
                reject(error)
            })
        })
    },

    async deleteTicket({ commit }, ticketId) {
        await axios.delete(BASE_URL + `/api/tickets${ticketId}`);
        commit('deleteTicket', ticketId);
    },

    async editProject({ commit }, payload) {
        const response = await axios.put(BASE_URL + `/api/projects/${payload.projectId}`, 
        {
            name: payload.projectName,
            description: payload.projectDescription
        });
        commit('editProject', response.data);
    },

    async fetchStaffs({commit}) {
        const response = await axios(BASE_URL + '/api/users');
        commit('setStaffs', response.data);
    }

};

const mutations = {
    setProjectDetails: (state, data) => { 
        state.projectDetails = { name: data.name, description: data.description, createdOn: data.createdOn, completion: data.completion }
        state.projectTickets = data.tickets
    },
    editProject: (state, data) => {
        state.projectName = data.name 
        state.projectDescription = data.description
    },
    addTicket: (state, ticket) => state.projectTickets.unshift(ticket),
    deleteTicket: (state, ticketId) => state.tickets.filter(ticketId),
    setStaffs: (state, staffs) => state.staffs = staffs.map(element => {
        return {
            text: element.name,
            value: element.userId
        }
    })
};

export default ({
    state,
    getters,
    actions,
    mutations
})
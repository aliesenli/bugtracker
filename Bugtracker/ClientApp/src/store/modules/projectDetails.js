import axios from 'axios'

const state = {
    projectDetails: {},
    projectName: "",
    projectDescription: "",
    projectTickets: [],
    staffs: []
};

const getters = {
    projectTickets: (state) => state.projectTickets,
    projectName: (state) => state.projectName,
    projectDescription: (state) => state.projectDescription,
    projectDetails: (state) => state.projectDetails,
    staffs: (state) => state.staffs
};

const actions = {
    async fetchProjectTickets({ commit }, test) {
        const response = await axios(`https://localhost:5001/api/projects/${test}`, {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('setProjectDetails', response.data)
    },


    async createTicket({ commit }, payload ) {
        const response = await axios.post('https://localhost:5001/api/tickets/create', { 
            title: payload.title,
            description: payload.description,
            priority: payload.priority, 
            assigneeId: payload.assigneeId, 
            projectId: payload.projectId 
        },
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            },
        });
 
        commit('addTicket', response.data);
    },

    async deleteTicket({ commit }, ticketId) {
        const respone = await axios.delete(`https://localhost:5001/api/tickets${ticketId}`);
        console.log(respone.data);
        
        commit('deleteTicket', ticketId);
    },

    async fetchStaffs({commit}) {
        const response = await axios('https://localhost:5001/api/staffs', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('setStaffs', response.data);
    }

};

const mutations = {
    setProjectDetails: (state, data) => { 
        state.projectDescription = data.description,
        state.projectName = data.name,
        state.projectTickets = data.tickets,
        state.projectDetails = data
    },
    addTicket: (state, ticket) => state.projectTickets.unshift(ticket),
    deleteTicket: (state, ticketId) => state.tickets.filter(ticketId),

    setStaffs: (state, staffs) => state.staffs = staffs.map(element => {
        return {
            text: element.name,
            value: element.staffId
        }
    })
};

export default ({
    state,
    getters,
    actions,
    mutations
})
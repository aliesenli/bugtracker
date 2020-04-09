import axios from 'axios'

const state = {
    projectDetails: {},
    projectName: "",
    projectDescription: "",
    projectTickets: []
};

const getters = {
    projectTickets: (state) => state.projectTickets,
    projectName: (state) => state.projectName,
    projectDescription: (state) => state.projectDescription,
    projectDetails: (state) => state.projectDetails
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

    async createTicket({ commit }, name, prio, projectId) {
        const response = await axios.post('https://localhost:5001/api/tickets/create', { name: name, priority: prio, projectId: projectId },
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
    }

};

const mutations = {
    setProjectDetails: (state, data) => { 
        state.projectDescription = data.description,
        state.projectName = data.name,
        state.projectTickets = data.tickets,
        state.projectDetails = data,
        console.log(data)
    },
    addTicket: (state, ticket) => state.tickets.unshift(ticket),
    deleteTicket: (state, ticketId) => state.tickets.filter(ticketId)
};

export default ({
    state,
    getters,
    actions,
    mutations
})
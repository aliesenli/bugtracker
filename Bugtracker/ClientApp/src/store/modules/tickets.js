import axios from 'axios'

const state = {
    tickets: []
};

const getters = {
    allTickets: (state) => state.tickets
};

const actions = {
    async fetchTickets({ commit }, test) {
        const response = await axios(`https://localhost:5001/api/projects/${test}`, {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });
        console.log(response.data);
        commit('setTickets', response.data)
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
    setTickets: (state, tickets) => state.tickets = tickets,
    addTicket: (state, ticket) => state.tickets.unshift(ticket),
    deleteTicket: (state, ticketId) => state.tickets.filter(ticketId)
};

export default ({
    state,
    getters,
    actions,
    mutations
})
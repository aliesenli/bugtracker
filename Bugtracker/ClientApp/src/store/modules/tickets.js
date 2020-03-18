import axios from 'axios'

const state = {
    tickets: []
};

const getters = {
    allTickets: (state) => state.tickets
};

const actions = {
    async fetchTickets({ commit }) {
        const response = await axios('http://localhost:44340/api/tickets');
        
        commit('setTickets', response.data)
    },

    async createTicket({ commit }, name, prio, projectId) {
        const response = await axios.post('http://localhost:44340/api/tickets/create',
        { name: name, priority: prio, projectId: projectId }
        );

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
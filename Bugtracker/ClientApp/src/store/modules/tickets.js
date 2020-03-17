import axios from 'axios'

const state = {
    tickets: []
};

const getters = {
    allTickets: (state) => state.tickets
};

const actions = {
    async fetchTickets({ commit }) {
        const response = await axios('https://localhost:5001/api/tickets');
        
        commit('setTickets', response.data)
    },
    async createTicket({ commit }, ticket) {
        const response = await axios.post(`https://localhost:5001/api/tickets/create${ticket}`);
        console.log(response.data);

        commit('addTicket', ticket);
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
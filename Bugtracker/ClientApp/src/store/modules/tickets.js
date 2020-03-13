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
    }
};

const mutations = {
    setTickets: (state, tickets) => state.tickets = tickets,
};

export default ({
    state,
    getters,
    actions,
    mutations
})
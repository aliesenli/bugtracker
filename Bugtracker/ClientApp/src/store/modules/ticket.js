import axios from 'axios'

const state = {
    ticket: {}
};

const getters = {
    getTicket: (state) => state.ticket
};

const actions = {
    async fetchTicket({ commit }, test) {
        const response = await axios(`https://localhost:5001/api/tickets/${test}`, {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('setTicket', response.data)
    },

};

const mutations = {
    setTicket: (state, data) => { 
        state.ticket = data
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
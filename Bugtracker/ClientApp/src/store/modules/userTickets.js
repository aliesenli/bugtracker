import axios from 'axios'

const state = {
    userTickets: []
};

const getters = {
    userTickets: (state) => state.userTickets,
};

const actions = {
    async fetchUserTickets({ commit }, userId) {
        const response = await axios(`https://localhost:5001/api/tickets/user/${userId}`);
        commit('setTickets', response.data)
    },
};

const mutations = {
    setTickets: (state, data) => { state.userTickets = data },
};

export default ({
    state,
    getters,
    actions,
    mutations
})
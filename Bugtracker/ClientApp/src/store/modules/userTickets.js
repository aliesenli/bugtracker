import axios from 'axios'

const state = {
    userTickets: []
};

const getters = {
    userTickets: (state) => state.userTickets,
};

const actions = {
    async fetchUserTickets({ commit }, test) {
        const response = await axios(`https://localhost:5001/api/tickets/user/${test}`, {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });
        console.log(response.data)
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
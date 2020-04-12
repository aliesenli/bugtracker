import axios from 'axios'

const state = {
    ticket: {}
};

const getters = {
    getTicket: (state) => state.ticket,
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

    async editTicket({ commit }, payload) {
        const response = await axios.put(`https://localhost:5001/api/tickets/${payload.ticketId}`, {
            title: payload.title,
            description: payload.description,
            priority: payload.priority,
            status: payload.status,
            assigneeId: payload.assigneeId
        },
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });
        
        commit('updateTicket', response.data)
    },



};

const mutations = {
    setTicket: (state, data) => { state.ticket = data },
    updateTicket: (state, data) => {
        state.ticket.title = data.title
        state.ticket.description = data.description,
        state.ticket.status = data.status,
        state.ticket.priority = data.priority,
        state.ticket.assignee = data.assignee
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
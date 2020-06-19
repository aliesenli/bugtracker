import axios from 'axios'
const BASE_URL = process.env.VUE_APP_BASEURL

const state = {
    ticket: {}
};

const getters = {
    getTicket: (state) => state.ticket,
};

const actions = {
    async fetchTicket({ commit }, payload) {
        const response = await axios(BASE_URL + `/api/tickets/${payload}`, {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('access_token')
            }
        });
        commit('setTicket', response.data)
    },

    async editTicket({ commit }, payload) {
        const response = await axios.put(BASE_URL + `/api/tickets/${payload.ticketId}`, {
            title: payload.title,
            description: payload.description,
            priority: payload.priority,
            status: payload.status,
            assigneeId: payload.assigneeId
        },
        {
            headers: {
                "Authorization": "bearer " + localStorage.getItem('access_token') 
            }
        });
        commit('updateTicket', response.data)
    },

    async postComment({commit}, payload) {
        const response = await axios.post(BASE_URL + `/api/tickets/${payload.ticketId}/comments/create`, {
            message: payload.message
        },
        {
            headers: {
                "Authorization": "bearer " + localStorage.getItem('access_token')
            }
        });
        commit('setComment', response.data)
    }
};

const mutations = {
    setTicket: (state, data) => { state.ticket = data },
    updateTicket: (state, data) => {
        state.ticket.title = data.title
        state.ticket.description = data.description,
        state.ticket.updatedOn = data.updatedOn,
        state.ticket.status = data.status,
        state.ticket.priority = data.priority,
        state.ticket.assignee = data.assignee,
        state.ticket.audits = data.audits
    },
    setComment: (state, comment) => state.ticket.comments.unshift(comment)
};

export default ({
    state,
    getters,
    actions,
    mutations
})
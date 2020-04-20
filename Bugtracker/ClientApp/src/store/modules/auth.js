import axios from 'axios'

const state = {
    status: '',
    loading: false,
    token: localStorage.getItem('token') || '',
    user : {}
};

const getters = {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
};

const actions = {
    login({commit}, user){
        return new Promise((resolve, reject) => {
            commit('auth_request')
            axios({url: 'https://localhost:5001/api/identity/login', data: user, method: 'POST' })
            .then(resp => {
                const token = resp.data.token
                const user = resp.data.user
                localStorage.setItem('token', token)
                axios.defaults.headers.common['Authorization'] = token
                commit('auth_success', token, user)
                resolve(resp)
            })
            .catch(err => {
                commit('auth_error')
                localStorage.removeItem('token')
                reject(err)
            })
        })
    },
    register({commit}, user){
        return new Promise((resolve, reject) => {
            commit('auth_request')
            axios({url: 'https://localhost:5001/api/identity/register', data: user, method: 'POST' })
            .then(resp => {
                const token = resp.data.token
                const user = resp.data.user
                localStorage.setItem('token', token)

                axios.defaults.headers.common['Authorization'] = token
                commit('auth_success', token, user)
                resolve(resp)
            })
            .catch(err => {
                commit('auth_error', err)
                localStorage.removeItem('token')
                reject(err)
            })
        })
    },
      logout({commit}){
        return new Promise((resolve) => {
              commit('logout')
              localStorage.removeItem('token')
              delete axios.defaults.headers.common['Authorization']
              resolve()
        })
      }
};

const mutations = {

    auth_request(state) {
    state.status = 'loading',
    state.loading = true
    },
    auth_success(state, token, user) {
    state.status = 'success'
    state.loading = false
    state.token = token
    state.user = user
    },
    auth_error(state) {
    state.status = 'error'
    state.loading = false
    },
    logout(state) {
    state.status = ''
    state.token = ''
    }

};

export default ({
    state,
    getters,
    actions,
    mutations
})
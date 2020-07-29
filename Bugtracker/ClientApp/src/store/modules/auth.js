import axios from 'axios'
const BASE_URL = process.env.VUE_APP_BASEURL

const state = {
    status: '',
    loading: false,
    token: localStorage.getItem('access_token') || '',
};

const getters = {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
};

const actions = {
    login({commit}, {vm ,payload}){
        return new Promise((resolve, reject) => {
            commit('auth_request')
            axios.post(BASE_URL + `/api/identity/login`, {
            email: payload.email,
            password: payload.password
            })
            .then(response => {
                const access_token = response.data.token
                const refresh_token = response.data.refreshToken
                localStorage.setItem('access_token', access_token);
                localStorage.setItem('refresh_token', refresh_token);
                axios.defaults.headers.common['Authorization'] = access_token
                commit('auth_success', access_token, payload)
                resolve(response)
            })
            .catch(error => {
                vm.$bvToast.toast(`${error.response.data.errors}`, {
                    title : 'Login failed',
                    variant : 'danger'
                })
                commit('auth_error')
                localStorage.removeItem('access_token')
                reject(error)
            })
        })
    },
    register({commit}, user){
        return new Promise((resolve, reject) => {
            commit('auth_request')
            axios.post(BASE_URL + '/api/identity/register', {
                email: user.email,
                password: user.password
            })
            .then(response => {
                const token = response.data.token
                const refreshToken = response.data.refreshToken
                localStorage.setItem('access_token', token)
                localStorage.setItem('refresh_token', refreshToken);
                axios.defaults.headers.common['Authorization'] = token
                commit('auth_success', token, user)
                resolve(response)
            })
            .catch(error => {
                commit('auth_error', error)
                localStorage.removeItem('access_token')
                reject(error)
            })
        })
    },
    logout({commit}){
        return new Promise((resolve) => {
            commit('logout')
            localStorage.removeItem('access_token')
            localStorage.removeItem('refresh_token')
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
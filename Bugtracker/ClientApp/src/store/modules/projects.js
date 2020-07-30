import axios from 'axios'
const BASE_URL = process.env.VUE_APP_BASEURL

const state = {
    projects: []
};

const getters = {
    allProjects: (state) => state.projects
};

const actions = {
    async fetchProjects({ commit }) {
        return new Promise((resolve, reject) => {
            axios.get(BASE_URL + '/api/projects')
            .then(response => {
                commit('setProjects', response.data)
                resolve(response)
            }).catch(error => {
                reject(error)
            })
        })
    },

    async createProject({ commit }, {vm, payload}) {
        return new Promise((resolve, reject) => {
            axios.post(BASE_URL + `/api/projects/create`, {
                name: payload.name, 
                description: payload.description, 
                completion: payload.completion
            })
            .then(response => {
                commit('addProject', response.data);
                resolve(response)
            })
            .catch(error => {
                vm.$bvToast.toast(`insufficient permissions`, {
                    title : 'Project not created',
                    variant : 'danger'
                })
                reject(error)
            })
        })
    }
};

const mutations = {
    setProjects: (state, projects) => state.projects = projects,
    addProject: (state, project) => state.projects.unshift(project)
};

export default ({
    state,
    getters,
    actions,
    mutations
})
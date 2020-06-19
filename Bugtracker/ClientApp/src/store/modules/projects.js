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
        const response = await axios(BASE_URL + '/api/projects', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('access_token')
            }
        });
        
        commit('setProjects', response.data);
    },

    async createProject({ commit }, payload) {
        const response = await axios.post(BASE_URL + '/api/projects/create', 
        { 
            name: payload.name, 
            description: payload.description, 
            completion: payload.completion
        },
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('access_token')
            }
        });

        commit('addProject', response.data);
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
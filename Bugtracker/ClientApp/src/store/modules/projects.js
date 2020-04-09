import axios from 'axios'

const state = {
    projects: []
};

const getters = {
    allProjects: (state) => state.projects
};

const actions = {
    async fetchProjects({ commit }) {
        const response = await axios('https://localhost:5001/api/projects', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });
        
        commit('setProjects', response.data);
    },

    async createProject({ commit }, name, description, completion ) {
        const response = await axios.post('https://localhost:5001/api/projects/create', { name, description, completion },
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('addProject', response.data);
    }
};

const mutations = {
    setProjects: (state, projects) => state.projects = projects,
    addProject: (state, project) => state.projects.push(project)
};

export default ({
    state,
    getters,
    actions,
    mutations
})
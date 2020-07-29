import axios from 'axios'
const BASE_URL = process.env.VUE_APP_BASEURL

const state = {
    tableStaffs: []
};

const getters = {
    tableStaffs: (state) => state.tableStaffs
};

const actions = {
    async fetchTableStaffs({commit}) {
        const response = await axios(BASE_URL + '/api/users', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('access_token')
            }
        });

        commit('setTableStaffs', response.data);
    },

    async assignRole({commit}, payload) {
        const response = await axios.post(BASE_URL + '/api/users/role', 
        {
            user: payload.user,
            role: payload.role
        }, 
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('access_token')
            }
        });

        commit('updateTable', response.data);
    }

};

const mutations = {
    setTableStaffs: (state, staffs) => {
        state.tableStaffs = staffs
    },
    updateTable: (state, staff) => {
       state.tableStaffs.find(element => element.userId == staff.userId).role = staff.role
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
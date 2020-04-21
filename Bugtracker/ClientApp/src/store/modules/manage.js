import axios from 'axios'

const state = {
    tableStaffs: []
};

const getters = {
    tableStaffs: (state) => state.tableStaffs
};

const actions = {
    async fetchTableStaffs({commit}) {
        const response = await axios('https://localhost:5001/api/users', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('setTableStaffs', response.data);
    },

    async assignRole({commit}, payload) {
        const response = await axios.post('https://localhost:5001/api/users/role', 
        {
            user: payload.user,
            role: payload.role
        }, 
        {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
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
       state.tableStaffs.find(element => element.staffId == staff.staffId).role = staff.role
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
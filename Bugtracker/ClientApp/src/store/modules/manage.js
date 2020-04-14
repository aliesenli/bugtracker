import axios from 'axios'

const state = {
    tableStaffs: []
};

const getters = {
    tableStaffs: (state) => state.tableStaffs
};

const actions = {
    async fetchTableStaffs({commit}) {
        const response = await axios('https://localhost:5001/api/staffs', {
            headers: {
                "Authorization": "bearer "+ localStorage.getItem('token') ,
                "Accept": "application/json",
                "cache-control": "no-cache"
            }
        });

        commit('setTableStaffs', response.data);
    }

};

const mutations = {
    setTableStaffs: (state, staffs) => {
        state.tableStaffs = staffs
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
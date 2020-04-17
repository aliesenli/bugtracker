const state = {
    spacing: false
};

const getters = {
    spacing: (state) => state.spacing
}

const actions = {
    set({commit}){
        commit("setSpacing")
    },

    clear({commit}) {
        commit("clearSpacing")
    }
};

const mutations = {
    setSpacing(state) {
        state.spacing = true
    },

    clearSpacing(state) {
        state.spacing = false
    }
};

export default ({
    state,
    getters,
    actions,
    mutations
})
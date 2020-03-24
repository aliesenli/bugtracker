const state = {
    spacing: true
};

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
    actions,
    mutations
})
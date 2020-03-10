const state = {
    tickets: [
        {
            id: 1,
            name: "test"
        },
        {
            id: 2,
            name:"test2"
        }
    ]
};

const getters = {
    allTickets: state => state.tickets
};

const actions = {};

const mutations = {};

export default ({
    state,
    getters,
    actions,
    mutations
})
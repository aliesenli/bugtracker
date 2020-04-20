import Vuex from 'vuex'
import Vue from 'vue'
import projectDetails from './modules/projectDetails'
import projects from './modules/projects'
import ticket from './modules/ticket.js'
import userTickets from './modules/userTickets.js'
import manage from './modules/manage.js'
import auth from './modules/auth'

//load Vuex
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        projectDetails,
        projects,
        ticket,
        userTickets,
        manage,
        auth,
    }
});

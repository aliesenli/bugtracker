import Vuex from 'vuex'
import Vue from 'vue'
import projectDetails from './modules/projectDetails'
import projects from './modules/projects'
import ticket from './modules/ticket.js'
import auth from './modules/auth'
import menu from './modules/menu'

//load Vuex
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        projectDetails,
        projects,
        ticket,
        auth,
        menu
    }
});

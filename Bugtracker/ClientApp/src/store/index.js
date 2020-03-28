import Vuex from 'vuex'
import Vue from 'vue'
import projectDetails from './modules/projectDetails'
import projects from './modules/projects'
import auth from './modules/auth'
import menu from './modules/menu'

//load Vuex
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        projectDetails,
        projects,
        auth,
        menu
    }
});

import Vuex from 'vuex'
import Vue from 'vue'
import tickets from './modules/tickets'

//load Vuex
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        tickets
    }
});

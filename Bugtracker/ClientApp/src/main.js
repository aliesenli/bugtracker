import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import { BCalendar } from 'bootstrap-vue'


Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.component('Date', BCalendar)

Vue.config.productionTip = false;
Vue.config.devtools = true;

window.bus = new Vue();

new Vue({
    store,
    router,
    render: h => h(App)
}).$mount('#app')

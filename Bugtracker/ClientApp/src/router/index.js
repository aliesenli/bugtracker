import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Projects from '../views/Projects.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/page/all-projects',
    name: 'Projects',
    component: Projects
  } 
]

const router = new VueRouter({
    mode: 'history',
    routes
})

export default router

import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store'
import Home from '../views/Home.vue'
import Projects from '../views/Projects.vue'
import Project from '../views/Project.vue'
import Ticket from '../views/Ticket.vue'
import Manage from '../views/Manage.vue'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta: { 
      requiresAuth: true
    }
  },
  {
    path: '/projects',
    name: 'Projects',
    component: Projects
  },
  { 
    path: '/project/:projectId',
    name: 'Project',
    component: Project
  },
  {
    path: '/ticket/:ticketId',
    name: 'Ticket',
    component: Ticket
  },
  {
    path: '/manage-roles',
    name: 'Manage',
    component: Manage
  },
  {
    path: '/login',
    name: 'login',
    component: Login,
    meta: {
      layout: "no-sidebar"
    }
  },
  {
    path: '/register',
    name: 'register',
    component: Register,
    meta: {
      layout: "no-sidebar"
    }
  },
]

const router = new VueRouter({
    mode: 'history',
    routes
})

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next()
      return
    }
    next('/login') 
  } else {
    next() 
  }
})

export default router

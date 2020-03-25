import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store'
import Home from '../views/Home.vue'
import Projects from '../views/Projects.vue'
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
  { path: '/projects/:projectId', name: 'project', component: Home },
  {
    path: '/login',
    name: 'login',
    component: Login,
    meta: {
      hideNavigation: true
    }
  },
  {
    path: '/register',
    name: 'register',
    component: Register,
    meta: {
      hideNavigation: true
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

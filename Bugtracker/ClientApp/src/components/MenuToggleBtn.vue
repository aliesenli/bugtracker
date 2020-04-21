<template>
  <div>
    <div class="main-content__top" v-show="!$route.meta.hideNavigation">
      <a
        href="#"
        @click.prevent="toggleMenu"
        class="btn menu-toggle-btn"
      >
        <i class="fa fa-bars" aria-hidden="true"></i>
      </a>
      <div class="main-content__title">
          <span v-if="isLoggedIn">
            <span>Logged in as: {{ loggedInAs.sub }} </span>
          </span>
      </div>
    </div>

  </div>
</template>

<script>
import jwt_decode from 'jwt-decode'

import axios from "axios"
import LocalStorageService from "./support/localStorageService"
import router from "../router"

export default {

  computed : {
      isLoggedIn : function(){ return this.$store.getters.isLoggedIn },
      loggedInAs : function(){ return jwt_decode(this.$store.state.auth.token) }
    },

   methods: {
    toggleMenu() {
      window.bus.$emit('menu/toggle');
    },

    logout: function () {
        this.$store.dispatch('logout')
        .then(() => {
          this.$router.push('/login')
        })
    }

  },

  created: function () {
    // LocalstorageService
    const localStorageService = LocalStorageService.getService();

    //Add a request interceptor
    axios.interceptors.request.use(
      config => {
          const token = localStorageService.getAccessToken();
          if (token) {
              config.headers['Authorization'] = 'Bearer ' + token;
          }
          // config.headers['Content-Type'] = 'application/json';
          return config;
      },
      error => {
          Promise.reject(error)
      });

    //Add a response interceptor
    axios.interceptors.response.use((response) => {
      return response
    }, function (error) {
      const originalRequest = error.config;

      if (error.response.status === 401 && originalRequest.url === 'http://localhost:8080/api/identity/refresh') {
          router.push('/login');
          return Promise.reject(error);
      }

      if (error.response.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true;
          const refreshToken = localStorageService.getRefreshToken();
          return axios.post('/api/identity/refresh', {
              "refresh_token": refreshToken
          })
          .then(res => {
              if (res.status === 201) {
                  localStorageService.setToken(res.data);
                  console.log("new token set after refresh")
                  axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorageService.getAccessToken();
                  return axios(originalRequest);
              }
          })
      }
      return Promise.reject(error);
    });
  }

};

</script>
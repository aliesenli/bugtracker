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
            <b-button @click="logout">Logout</b-button>
          </span>
      </div>
    </div>

  </div>
</template>

<script>
  import jwt_decode from 'jwt-decode'

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
      this.$http.interceptors.response.use(undefined, function (err) {
        return new Promise(function () {
          if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
            this.$store.dispatch(this.logout)
          }
          throw err;
        });
      });
    }

};

</script>

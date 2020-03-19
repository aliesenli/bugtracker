<template>
  <div>

    <div class="main-content__top">
      <a
        href="#"
        @click.prevent="toggleMenu"
        class="btn menu-toggle-btn"
      >
        <i class="fa fa-bars" aria-hidden="true"></i>
      </a>
      <h1 class="main-content__title">
          Home
          <span v-if="isLoggedIn">
            <button @click="logout">Logout</button>
          </span>
      </h1>
    </div>

  </div>
</template>

<script>

export default {

  computed : {
      isLoggedIn : function(){ return this.$store.getters.isLoggedIn}
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

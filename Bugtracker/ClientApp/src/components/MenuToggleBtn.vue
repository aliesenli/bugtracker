<template>
  <div>
    <a
      href="#"
      @click.prevent="toggleMenu"
      class="btn menu-toggle-btn"
    >
      <i class="fa fa-bars" aria-hidden="true"></i>
    </a>


    <div class="main-content__top">
        <h1 class="main-content__title">
            Home
        </h1>
    </div>

  </div>
</template>

<script>

export default {

   methods: {

    toggleMenu() {
      window.bus.$emit('menu/toggle');
    },

  },

};

</script>

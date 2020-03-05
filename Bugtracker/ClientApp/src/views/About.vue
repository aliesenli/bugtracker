<template>
  <div class="about">
    <NavigationBar/>
    <h1>This is an about page</h1>
  </div>
</template>

<script>
     import NavigationBar from '@/components/NavigationBar.vue'

export default {
  name: 'About',
  components: {
      NavigationBar
  }
}
</script>

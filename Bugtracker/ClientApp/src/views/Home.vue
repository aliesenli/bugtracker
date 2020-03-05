<template>
    <div class="home">
        <NavigationBar/>
        <img alt="Vue logo" src="../assets/logo.png">
        <HelloWorld msg="Welcome to Your Vue.js App" />
        <Ticket ticket="this is a ticket" />
    </div>
</template>

<script>
// @ is an alias to /src
    import HelloWorld from '@/components/HelloWorld.vue'
    import Ticket from '@/components/Ticket.vue'
    import NavigationBar from '@/components/NavigationBar.vue'

export default {
  name: 'Home',
  components: {
      HelloWorld,
      Ticket,
      NavigationBar
  }
}
</script>

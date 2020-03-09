<template>
    <div class="home">

        <div class="main-content">

            <div class="main-content__top">
                <h1 class="main-content__title">
                    Home
                </h1>
            </div>

            <div class="main-content__body">
                <p>
                    The menu can be easily customized changing:
                </p>

                <ol>

                    <li>
                        <strong>src/components/Menu.vue</strong>
                        where the root level itens can be found.
                    </li>

                    <li>
                        <strong>src/components/support/menu-data.js</strong>
                        where the childs of root level itens can be found.
                    </li>

                    <li>
                        <strong>src/router.js</strong>
                        where each route can be mapped to load the correspondent component.
                        For the sake of simplicity, with exception of the <strong>home route</strong>, the
                        sections are loaded dynamically in this example.
                    </li>

                    <p>
                        <img alt="Vue logo" src="../assets/logo.png">
                        <HelloWorld msg="Welcome to Your Vue.js App" />
                        <Ticket ticket="this is a ticket" />
                    </p>

                </ol>

            </div>

        </div>
        
    </div>
</template>

<script>
// @ is an alias to /src
    import HelloWorld from '@/components/HelloWorld.vue'
    import Ticket from '@/components/Ticket.vue'

export default {
  name: 'Home',
  components: {
      HelloWorld,
      Ticket
  }
}
</script>

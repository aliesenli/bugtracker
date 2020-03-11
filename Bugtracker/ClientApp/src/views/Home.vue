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
                    <img alt="Vue logo" src="../assets/logo.png">
                    <Ticket />
                    <Date/>
                </p>
            </div>

        </div>

    </div>
</template>

<script>
    // @ is an alias to /src
    import Ticket from '@/components/Ticket.vue'
    import Date from '@/components/Date.vue'

    export default {
        name: 'Home',
        components: {
            Ticket,
            Date
        }
    }
</script>

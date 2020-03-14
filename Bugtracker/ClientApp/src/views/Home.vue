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

<style lang="scss">
    @import '../styles/layout.scss';
    @import '../styles/menu-toggle-btn.scss';
    @import '../styles/menu.scss';
    @import '../styles/content-overlay.scss';
    @import '../styles/media-queries.scss';
</style>

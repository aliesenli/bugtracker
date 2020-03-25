<template>
    <div class="home">
        <div class="main-content"> 
            <div class="main-content__body">
                <Ticket />
            </div>
        </div>
    </div>
</template>

<script>
    // @ is an alias to /src
    import Ticket from '@/components/Ticket.vue'

    export default {
        name: 'Home',
        components: {
            Ticket
        }
    }
</script>

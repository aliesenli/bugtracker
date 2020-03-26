<template>
    <div class="main-content"> 
        <div class="main-content__body">
            <Ticket/>
        </div>
    </div>
</template>

<script>
    // @ is an alias to /src
    import Ticket from '@/components/Ticket.vue'

    export default {
        components: {
            Ticket
        }
    }
</script>

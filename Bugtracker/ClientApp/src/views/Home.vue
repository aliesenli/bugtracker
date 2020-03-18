<template>
    <div class="home">
        <div class="main-content">
            
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

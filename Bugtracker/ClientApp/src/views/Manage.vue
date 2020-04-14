<template>
    <div class="main-content"> 
        <div class="main-content__body">
            <Manage/>
        </div>
    </div>
</template>

<script>
    // @ is an alias to /src
    import Manage from '@/components/Manage.vue'

    export default {
        components: {
            Manage
        }
    }
</script>

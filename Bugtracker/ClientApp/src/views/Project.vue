<template>
    <div class="main-content" id="wrapper"> 
        <div class="main-content__body">
            <ProjectDetail/>
            <ProjectTickets/>
        </div>
    </div>
</template>

<script>
    // @ is an alias to /src
    import ProjectTickets from '@/components/ProjectTickets.vue'
    import ProjectDetail from '@/components/ProjectDetails.vue'

    export default {
        components: {
            ProjectDetail,
            ProjectTickets
        }
    }
</script>

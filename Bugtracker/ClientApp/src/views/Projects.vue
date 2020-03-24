<template>
    <div class="main-content"> 
            <div class="main-content__body">
                <Project />
            </div>
        </div>
</template>

<script>
    // @ is an alias to /src
    import Project from '@/components/Project.vue'

    export default {
        name: 'Projects',
        components: {
            Project
        }
    }
</script>

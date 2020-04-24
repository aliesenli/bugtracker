<template>
    <div class="main-content" id="wrapper"> 
        <div class="main-content__body">
            <Projects />
        </div>
    </div>
</template>

<script>
    // @ is an alias to /src
    import Projects from '@/components/Projects.vue'

    export default {
        components: {
            Projects
        }
    }
</script>

<template>
    <div>
        <h1>Salam</h1>

        <ul id="example-1">
            <li v-for="project in allProjects" :key="project.id">
                {{ project.name }}
            </li>
        </ul>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

export default {

    name: 'Project',

    methods: {
        ...mapActions([
            'fetchProjects'
        ]),
    },

    computed: {
        ...mapGetters([
            'allProjects'
        ]),      
    },
    
    created() {
        this.fetchProjects();
    },

}

</script>
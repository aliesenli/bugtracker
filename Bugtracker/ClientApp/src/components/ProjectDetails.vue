<template>
    <div>
        <b-card class="mt-3">
            <div class="card-title-shifted">
                <h5>Project-Details</h5>
                <b-row>
                    <b-col>
                        <a href="#" class="card-link">Card link</a>
                        <b-link href="#" class="card-link">Another link</b-link>     
                    </b-col>
                
                </b-row>
            </div>
            <div class="mt-4">
                <b-row align-h="start">
                    <b-col><span class="font-weight-bold">Name: </span>{{ projectName }}</b-col>
                    <b-col><span class="font-weight-bold">Description: </span>{{ projectDescription }}</b-col>
                </b-row>
            </div>
        </b-card>
        <hr>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'

    export default {
        name: 'ProjectDetail',
        methods: {

        },
        computed: {
            ...mapGetters([
                'projectName',
                'projectDescription'
            ]),      
        },
    }
</script>

<style scoped>
    a {
        color: #42b983;
    }
</style>


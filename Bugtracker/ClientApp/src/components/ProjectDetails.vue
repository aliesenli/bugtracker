<template>
    <div>
            <b-row>
                <b-col cols="12">
                    <div>
                        <b-button-group>
                        <b-button variant="outline-primary">
                            <b-icon icon="pencil"></b-icon> Edit
                        </b-button>
                        <b-button variant="outline-primary">
                            <b-icon icon="person-fill"></b-icon> Account
                        </b-button>
                        <b-button variant="outline-primary">
                            <b-icon icon="inbox-fill"></b-icon> Messages
                        </b-button>
                        </b-button-group>
                    </div>
                </b-col>
            </b-row>

             <b-row class="mt-4">
                <b-col cols="12" md="8">
                    <div>
                        <div class="mytextdiv">
                            <div class="mytexttitle">
                                Details
                            </div>
                            <div class="divider"></div>
                        </div>
                        <div class="p-2">
                            <b-row>
                                <b-col cols="6" md="6">
                                    <p>Name: {{ projectName }}</p>
                                    <p>Status: {{  }}</p>
                                </b-col>
                                <b-col cols="6" md="6">

                                </b-col>
                            </b-row>
                        </div>

                        <div class="mytextdiv">
                            <div class="mytexttitle">
                                Description
                            </div>
                            <div class="divider"></div>
                        </div>
                        <div class="p-2">
                            {{ projectDescription }}
                        </div>
                    </div>
                </b-col>
                
                <b-col cols="6" md="4">
                    <div>
                        <div class="mytextdiv">
                            <div class="mytexttitle">
                                Dates
                            </div>
                            <div class="divider"></div>
                        </div>
                        <div class="p-2">
                            <p>Created: {{  }}</p>
                            <p>Updated: {{ }}</p>
                        </div>
                        
                    </div>
                </b-col>
            </b-row>
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
.mytextdiv {
    display:flex;
    flex-direction:row;
    align-items: center;
}

.mytexttitle {
    flex-grow:0;
    font-weight: bold;
}

.divider {
    flex-grow:1;
    height: 1px;
    background-color: silver;
}
</style>


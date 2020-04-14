<template>
    <div>
        <b-row>
            <b-col cols="12">
                <div>

                    <b-button v-b-modal.modal-edit-footer-sm variant="outline-primary">
                        <b-icon icon="pencil"></b-icon> Edit Project
                    </b-button>

                    <b-modal id="modal-edit-footer-sm" size="lg" title="Edit Project" hide-footer>
                        <b-form @submit="onEditProject">
                            <b-form-group
                            class="mb-2"
                            label="Title"
                            label-for="input-title"
                            >
                                <b-form-input
                                id="input-title"
                                type="text"
                                required
                                v-model="name"
                                ></b-form-input>
                            </b-form-group>

                            <b-form-group
                            class="mb-2"
                            label="Description"
                            label-for="textarea"
                            >
                                <b-form-textarea
                                id="textarea"
                                type="text"
                                required
                                v-model="description"
                                
                                ></b-form-textarea>
                            </b-form-group>

                            <b-button type="submit" class="float-right mt-1" variant="primary">Update</b-button>
                        </b-form>
                    </b-modal>

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
                        <p>Created: {{ projectCreatedOn }}</p>
                        <p>Completion: {{ projectCompletion }}</p>
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
            onEditProject(e) {
                e.preventDefault();
                this.$store.dispatch('editProject', {
                    projectId: this.$route.params.projectId,
                    projectName: this.name,
                    projectDescription: this.description
                });
            },
        },
        computed: {
            ...mapGetters([
                'projectName',
                'projectDescription',
                'projectCreatedOn',
                'projectCompletion',
            ]),      
        },
        data() {
            return {
                name: "",
                description: "",
            }
        },
        watch: {
            projectName: function() {
                this.name = this.projectName
                this.description = this.projectDescription
            }
        }
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


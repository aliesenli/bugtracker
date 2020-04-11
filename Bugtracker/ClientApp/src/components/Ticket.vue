<template>
    <div class="main-content"> 
        <div class="main-content__body">
            <b-row>
                <b-col cols="12">
                    {{ getTicket.title }} / {{ getTicket.project}}
                </b-col>
            </b-row>
            <b-row class="mt-4">
                <b-col cols="12">
                    <div>
                        <b-button-group>
                        <b-button variant="outline-primary">
                            <b-icon icon="pencil"></b-icon> Edit
                        </b-button>
                        <b-button variant="outline-primary">
                            <b-icon icon="person-fill"></b-icon> Assign
                        </b-button>
                        <b-button variant="outline-primary">
                            <b-icon icon="inbox-fill"></b-icon> Comment
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
                                    <p>Priority: {{ getTicket.priority }}</p>
                                    <p>Status: {{ getTicket.status }}</p>
                                </b-col>
                                <b-col cols="6" md="6">
                                    <p>Created: {{ getTicket.createdOn }}</p>
                                    <p>Updated: {{ getTicket.updatedOn }}</p>
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
                            <p>{{ getTicket.description }}</p>
                        </div>

                        <b-tabs content-class="mt-3" lazy>
                            <b-tab title="History"><b-alert show>I'm lazy mounted!</b-alert></b-tab>
                            <b-tab title="Comments"><b-alert show>I'm lazy mounted too!</b-alert></b-tab>
                        </b-tabs>
                    </div>
                </b-col>
                
                <b-col cols="6" md="4">
                    <div>
                        <div class="mytextdiv">
                            <div class="mytexttitle">
                                People
                            </div>
                            <div class="divider"></div>
                        </div>
                        <div class="p-2">
                            <h6>Assigned to:</h6>
                            <b-avatar variant="primary"></b-avatar> {{ getTicket.assignee }}
                            <h6 class="mt-3">Submitter:</h6>
                            <b-avatar variant="secondary"></b-avatar> {{ getTicket.submitter }}
                        </div>
                        
                    </div>
                </b-col>
                
            </b-row>

        </div>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: 'Ticket',

    methods: {
        ...mapActions([
            'fetchTicket'
        ]),
    },

    computed: {
        ...mapGetters([
            'getTicket'
        ]),
    },
    
    created() {
        this.fetchTicket(this.$route.params.ticketId);
        console.log(this.getTicket)
    },

    data() {
        return {
            
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
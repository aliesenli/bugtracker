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
                            <b-button v-b-modal.modal-footer-sm variant="outline-primary" @click="setCurrentAssignee()">
                                <b-icon icon="pencil"></b-icon> Edit Ticket
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

            <div>
                <b-modal id="modal-footer-sm" size="lg" title="Edit Ticket" hide-footer>
                    <b-form @submit="onEditTicket">
                        <b-form-group
                        class="mb-2"
                        label="Title"
                        label-for="input-title"
                        >
                            <b-form-input
                            id="input-title"
                            type="text"
                            required
                            v-model="ticketTitle"
                            placeholder="Ticket Title"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group
                        class="mb-2"
                        label="Description"
                        label-for="textarea"
                        >
                            <b-form-textarea
                            required
                            id="textarea"
                            v-model="ticketDescription"
                            placeholder="Enter your text"
                            ></b-form-textarea>
                        </b-form-group>

                        <b-row>
                            <b-col cols="6">
                                <b-form-group
                                label="Priority"
                                label-for="priority-1"
                                >
                                    <b-form-select v-model="ticketPriority" class="mb-1" id="priority-1">
                                        <template v-slot:first>
                                            <b-form-select-option :value="getTicket.priority">{{ getTicket.priority }}</b-form-select-option>
                                        </template>
                                        <b-form-select-option v-show="getTicket.priority != 'High'" value=High>High</b-form-select-option>
                                        <b-form-select-option v-show="getTicket.priority != 'Medium'" value=Medium>Medium</b-form-select-option>
                                        <b-form-select-option v-show="getTicket.priority != 'Low'" value=Low>Low</b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                            <b-col cols="6">
                                <b-form-group
                                label="Status"
                                label-for="status-1"
                                >
                                    <b-form-select v-model="ticketStatus" class="mb-1" id="status-1">
                                        <template v-slot:first>
                                            <b-form-select-option :value="getTicket.status">{{getTicket.status}}</b-form-select-option>
                                        </template>
                                        <b-form-select-option v-show="getTicket.status != 'Closed'" value=Closed>Closed</b-form-select-option>
                                        <b-form-select-option v-show="getTicket.status != 'Open'" value=Open>Open</b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-form-group
                        label="Assign To"
                        label-for="assign-1"
                        >
                            <b-form-select v-model="ticketAssignee" :options="options" class="mb-1" id="assign-1">
                                <template v-slot:first>
                                    <b-form-select-option :value="currentAssignee.value">{{ currentAssignee.text }}</b-form-select-option>
                                </template>
                            </b-form-select>
                        </b-form-group>

                        <b-button type="submit" class="float-right mt-1" variant="primary">Submit</b-button>
                    </b-form>
                </b-modal>
            </div>

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
            'fetchTicket',
            'fetchStaffs'
        ]),

        setCurrentAssignee() {
            var currentAssignee = this.staffs.find(element => 
                element.text == this.getTicket.assignee)
    
            this.currentAssignee = currentAssignee;
        },

        onEditTicket(e) {
            e.preventDefault()
            this.$store.dispatch('editTicket', {
                ticketId: this.$route.params.ticketId,
                title: this.ticketTitle,
                description: this.ticketDescription,
                priority: this.ticketPriority,
                status: this.ticketStatus,
                assigneeId: this.ticketAssignee
            });
        }
    },

    computed: {
        ...mapGetters([
            'getTicket',
            'staffs'
        ]),
    },
    
    created() {
        this.fetchTicket(this.$route.params.ticketId);
        this.fetchStaffs();
    },

    data() {
        return {
            ticketId: this.$route.params.ticketId,

            ticketTitle: "",
            ticketDescription: "",
            ticketStatus: 0,
            ticketPriority: 0,
            ticketAssignee: "",

            currentPriority: "",
            currentAssignee: "",
            options: this.staffs
        }
    },
    watch: {
        getTicket: function() {
            this.ticketTitle = this.getTicket.title
            this.ticketDescription = this.getTicket.description
            this.ticketPriority = this.getTicket.priority
            this.ticketStatus = this.getTicket.status
        },
        staffs: function() {
            this.options = this.staffs.filter(e => e.text !== this.getTicket.assignee)
            this.ticketAssignee = this.staffs.find(element => 
                element.text == this.getTicket.assignee).value
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
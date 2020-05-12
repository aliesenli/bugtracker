<template>
    <div class="main-content" id="wrapper"> 
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
                            <b-button v-b-modal.modal-footer-sm variant="outline-primary">
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
                <b-modal id="modal-footer-sm" size="lg" title="Edit Ticket" ref="edit-ticket" hide-footer>
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
                            placeholder="Ticket Title"
                            ref="ticket_title"
                            :value="getTicket.title"
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
                            placeholder="Enter your text"
                            ref="ticket_description"
                            :value="getTicket.description"
                            ></b-form-textarea>
                        </b-form-group>

                        <b-row>
                            <b-col cols="6">
                                <b-form-group
                                label="Priority"
                                label-for="priority-1"
                                >
                                    <b-form-select :value="getTicket.priority" class="mb-1" id="priority-1" ref="ticket_priority">
                                        <b-form-select-option value=High>High</b-form-select-option>
                                        <b-form-select-option value=Medium>Medium</b-form-select-option>
                                        <b-form-select-option value=Low>Low</b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                            <b-col cols="6">
                                <b-form-group
                                label="Status"
                                label-for="status-1"
                                >
                                    <b-form-select :value="getTicket.status" class="mb-1" id="status-1" ref="ticket_status">
                                        <b-form-select-option value=Closed>Closed</b-form-select-option>
                                        <b-form-select-option value=Open>Open</b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-form-group
                        label="Assign To"
                        label-for="assign-1"
                        >
                            <b-form-select :value="getTicket.assigneeId" :options="staffs" class="mb-1" id="assign-1" ref="ticket_assigneeId">
                            </b-form-select>
                        </b-form-group>
                        
                        <b-button type="submit" class="float-right mt-1" variant="primary">Update</b-button>
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
                                <b-tab title="History">
                                    <b-alert show >
                                        <template v-if="getTicket.audits && getTicket.audits.length > 0">
                                            <div class="mt-2 mb-2" v-for="audit in getTicket.audits" v-bind:key="audit.id">
                                                <span class="audit-date">{{ audit.date }}</span>  
                                                {{ audit.property }} changed from 
                                                <span>{{ audit.oldValue }}</span> <b-icon icon="arrow-right"></b-icon>
                                                <span> {{ audit.newValue }}</span>
                                            </div>
                                        </template>
                                        <template v-else>
                                            Edit Ticket to see changes...
                                        </template>
                                    </b-alert>
                                </b-tab>
                            
                            <b-tab title="Comments"><b-alert show><template v-if="getTicket.comments > 0">todo: list comments</template><template v-else>No comments...</template></b-alert></b-tab>
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

        onEditTicket(e) {
            e.preventDefault()
            this.$store.dispatch('editTicket', {
                ticketId: this.$route.params.ticketId,
                title: this.$refs.ticket_title.localValue,
                description: this.$refs.ticket_description.localValue,
                priority: this.priorityToNumber(),
                status: this.statusToNumber(),
                assigneeId: this.$refs.ticket_assigneeId.localValue
            });

            this.$refs['edit-ticket'].hide()
        },

        priorityToNumber() {
            if(this.$refs.ticket_priority.localValue == 'Low') return 0;
            else if(this.$refs.ticket_priority.localValue == 'Medium') return 1;
            else return 2;
        },

        statusToNumber() {
            if(this.$refs.ticket_status.localValue == 'Open') return 0;
            else if(this.$refs.ticket_status.localValue == 'Closed') return 1;
        },
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
        }
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

.audit-date {
    border: 1px solid #ff0000c2;
    background-color: #ff000018;
    border-radius: 3px;
    padding: 3px;
}

.alert-info {
    background-color: rgba(40, 101, 255, 0.034);
}
</style>
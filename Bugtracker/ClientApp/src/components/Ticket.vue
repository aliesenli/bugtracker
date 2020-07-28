<template>
    <div>
        <b-row>
            <b-col cols="12">
                {{ getTicket.project}} / {{ getTicket.title }}
            </b-col>
        </b-row>
        <b-row class="mt-4">
            <b-col cols="12">
                <b-button-group>
                    <b-button v-b-modal.modal-footer-sm variant="outline-primary">
                        <b-icon icon="pencil"></b-icon> Edit Ticket
                    </b-button>
                    <b-button v-b-modal.modal-assign variant="outline-primary">
                        <b-icon icon="person-fill"></b-icon> Assign
                    </b-button>
                    <b-button v-b-modal.modal-comment variant="outline-primary">
                        <b-icon icon="inbox-fill"></b-icon> Comment
                    </b-button>
                </b-button-group>
            </b-col>
        </b-row>

        <b-modal id="modal-footer-sm" size="lg" title="Edit Ticket" ref="edit-ticket" hide-footer>
            <b-form @submit.prevent="onEditTicket">
                <b-form-group class="mb-2" label="Title" label-for="input-title">
                    <b-form-input id="input-title" type="text" required placeholder="Ticket Title" ref="ticket_title" :value="getTicket.title"></b-form-input>
                </b-form-group>

                <b-form-group class="mb-2" label="Description" label-for="textarea">
                    <b-form-textarea required id="textarea" placeholder="Enter your text" ref="ticket_description" :value="getTicket.description"></b-form-textarea>
                </b-form-group>

                <b-row>
                    <b-col cols="6">
                        <b-form-group label="Priority" label-for="priority">
                            <b-form-select :value="getTicket.priority" class="mb-1" id="priority" ref="ticket_priority">
                                <b-form-select-option value=High>High</b-form-select-option>
                                <b-form-select-option value=Medium>Medium</b-form-select-option>
                                <b-form-select-option value=Low>Low</b-form-select-option>
                            </b-form-select>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6">
                        <b-form-group label="Status" label-for="status">
                            <b-form-select :value="getTicket.status" class="mb-1" id="status" ref="ticket_status">
                                <b-form-select-option value=Closed>Closed</b-form-select-option>
                                <b-form-select-option value=Open>Open</b-form-select-option>
                            </b-form-select>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-button type="submit" class="float-right mt-1" variant="primary">Update</b-button>
            </b-form>
        </b-modal>

        <b-modal id="modal-assign" size="lg" title="Assign To" ref="assign-ticket" hide-footer>
            <b-form @submit.prevent="onAssignTicket">
                <b-form-group label="Assign To" label-for="assign-1">
                    <b-form-select :value="getTicket.assigneeId" :options="staffs" class="mb-1" id="assign-1" ref="ticket_assigneeId">
                    </b-form-select>
                </b-form-group>
                <b-button type="submit" class="float-right mt-1" variant="primary">Update</b-button>
            </b-form>
        </b-modal>

        <b-modal id="modal-comment" size="lg" title="Comment" ref="ticket-comment" hide-footer>
            <b-form @submit.prevent="onPostComment">
                <b-form-group label="Add new Comment to this Ticket" label-for="Comment">
                    <b-form-textarea required id="textarea" placeholder="Enter your text" ref="ticket_comment"></b-form-textarea>
                </b-form-group>    
                <b-button type="submit" class="float-right mt-1" variant="primary">Submit</b-button>
            </b-form>
        </b-modal>

        <b-row class="mt-4">
            <b-col cols="12" md="8">
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
                                        <b-row>
                                            <b-col sm="2" class="audit-date">{{ audit.date }}</b-col>
                                            <b-col> {{audit.property}} changed from {{audit.oldValue}}
                                                <b-icon icon="arrow-right"></b-icon> {{audit.newValue}}
                                            </b-col>
                                        </b-row>
                                    </div>
                                </template>
                                <template v-else>
                                    Empty history...
                                </template>
                            </b-alert>
                        </b-tab>
                        
                        <b-tab title="Comments">
                            <b-alert show>
                            <template v-if="getTicket.comments && getTicket.comments.length <= 0">No comments...</template>
                            <template v-else>
                                <div class="mt-2 mb-2" v-for="comment in getTicket.comments" v-bind:key="comment.id">
                                    <span class="text-danger">{{ comment.writer }}</span>:  
                                    <span>{{ comment.message }}</span>
                                </div>
                            </template>
                            </b-alert>
                        </b-tab>
                    </b-tabs>
            </b-col>

            <b-col sm cols="6">
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
            </b-col>
        </b-row>
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

        onEditTicket() {
            this.$store.dispatch('editTicket', {
                ticketId: this.$route.params.ticketId,
                title: this.$refs.ticket_title.localValue,
                description: this.$refs.ticket_description.localValue,
                priority: this.priorityToNumber(),
                status: this.statusToNumber(),
                assigneeId: this.getTicket.assigneeId
            });
            this.$refs['edit-ticket'].hide()
        },

        onAssignTicket() {
            this.$store.dispatch('editTicket', {
                ticketId: this.getTicket.id,
                title: this.getTicket.title,
                description: this.getTicket.description,
                priority: this.getTicket.priority,
                status: this.getTicket.status,
                assigneeId: this.$refs.ticket_assigneeId.localValue     
            });
            this.$refs['assign-ticket'].hide()
        },

        onPostComment() {
            this.$store.dispatch('postComment', {
                ticketId: this.getTicket.id,
                message: this.$refs.ticket_comment.localValue
            })
            this.$refs['ticket-comment'].hide()
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
    text-align: center;
    border: 1px solid #ff0000c2;
    background-color: #ff000018;
    border-radius: 3px;
    padding: 3px;
}

.alert-info {
    background-color: rgba(40, 101, 255, 0.034);
}
</style>
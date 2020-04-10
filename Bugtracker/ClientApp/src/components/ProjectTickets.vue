<template>
    <div class="hello mt-4">

        <div class="mb-3">
            <b-button v-b-modal.modal-footer-sm variant="outline-primary">
                <b-icon icon="inbox-fill"></b-icon> Create New Ticket
            </b-button>

            <b-modal id="modal-footer-sm" size="lg" title="Create New Ticket" hide-footer>
                <b-form @submit="onCreateTicket">
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

                    <b-form-group
                    label="Priority"
                    label-for="priority-1"
                    >
                        <b-form-select v-model="priority" class="mb-1" id="priority-1">
                            <template v-slot:first>
                                <b-form-select-option :value="null" disabled>-- Please select priority --</b-form-select-option>
                            </template>
                            <b-form-select-option value=2>High</b-form-select-option>
                            <b-form-select-option value=1>Medium</b-form-select-option>
                            <b-form-select-option value=0>Low</b-form-select-option>
                        </b-form-select>
                    </b-form-group>

                    <b-form-group
                    label="Assign To"
                    label-for="assign-1"
                    >
                        <b-form-select v-model="assigneeId" :options="options" class="mb-1" id="assign-1">
                            <template v-slot:first>
                                <b-form-select-option :value="null" disabled>-- Assign To --</b-form-select-option>
                            </template>
                        </b-form-select>
                    </b-form-group>

                    <b-button type="submit" class="float-right mt-1" variant="primary">Submit</b-button>
                </b-form>
            </b-modal>
        </div>

        <h5>Tickets for this project</h5>
        <div class="p-2">
            <b-row align-h="between" class="mb-2">
                <b-col sm="2" class="my-1">
                    <b-form-group
                    label="Per page"
                    label-cols-sm="12"
                    label-align-sm="left"
                    label-size="sm"
                    label-for="perPageSelect"
                    class="mb-0"
                    >
                    <b-form-select
                        v-model="perPage"
                        id="perPageSelect"
                        size="sm"
                        :options="pageOptions"
                    ></b-form-select>
                    </b-form-group>
                </b-col>

                <b-col sm="4" class="my-1">
                    <b-form-group
                    label="Filter"
                    label-cols-sm="12"
                    label-align-sm="left"
                    label-size="sm"
                    label-for="perPageSelect"
                    class="mb-0"
                    >
                        <b-input-group size="sm">
                            <b-form-input
                            v-model="filter"
                            type="search"
                            id="filterInput"
                            placeholder="Type to Search"
                            >
                            </b-form-input>
                                <b-input-group-append>
                                    <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
                                </b-input-group-append>
                        </b-input-group>
                    </b-form-group>
                </b-col>
            </b-row>

            <!-- Main table element -->
            <b-table
            show-empty
            stacked="sm"
            responsive="sm"
            sort-icon-left
            fixed
            striped
            :filter-ignored-fields="ignoreFilterFields"
            :busy="isBusy"
            :items="projectTickets"
            :fields="fields"
            :current-page="currentPage"
            :per-page="perPage"
            :filter="filter"
            :sort-by.sync="sortBy"
            :sort-desc.sync="sortDesc"
            :sort-direction="sortDirection"
            @filtered="onFiltered"
            >

                <template v-slot:cell(title)="row">
                    <b-icon icon="app-indicator"></b-icon> {{ row.item.title }}
                </template>/

                <template v-slot:table-busy>
                    <div class="text-center text-danger my-2">
                    <b-spinner class="align-middle"></b-spinner>
                    <strong>Loading...</strong>
                    </div>
                </template>

                <template v-slot:cell(actions)="row">
                    <b-button size="sm" @click="info(row.item)" class="mr-1">
                    Details
                    </b-button>                    
                </template>
            </b-table> 

            <b-row>
                <b-col sm="7" md="3" class="my-1">
                    <b-pagination
                        v-model="currentPage"
                        :total-rows="totalRows"
                        :per-page="perPage"
                        align="fill"
                        size="sm"
                        class="my-0"
                    ></b-pagination>
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
                'fetchProjectTickets',
                'createTicket',
                'createTicket2',
                'fetchStaffs'
            ]), 

            onCreateTicket(e) {
                e.preventDefault()
                this.$store.dispatch('createTicket', {
                    title: this.ticketTitle,
                    description: this.ticketDescription,
                    priority: this.priority,
                    assigneeId: this.assigneeId,
                    projectId: this.projectId
                })
                /*
                this.createTicket(
                    this.ticketTitle,
                    this.ticketDescription,
                    this.priority,
                    this.assignee,
                    //this.projectId
                );
                */
            },

            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = filteredItems.length
                this.currentPage = 1
            },

            info(item) {
                this.$router.push({ name: 'Ticket', params: { ticketId: item.id }})
            }

        },
        computed: {
            ...mapGetters([
                'projectTickets',
                'projectName',
                'projectDescription',
                'staffs'
            ]),
        },
        created() {
            this.fetchProjectTickets(this.$route.params.projectId);
            this.fetchStaffs();
        },
        data() {
            return {
                fields: [
                    { key: 'title', label: 'Ticket title', sortable: true, sortDirection: 'desc' },
                    { key: 'description', label: 'Description', sortable: false },
                    { key: 'priority', label: 'Priority', sortable: true, sortDirection: 'desc' },
                    { key: 'status', label: 'Status', sortable: true, sortDirection: 'desc' },
                    { key: 'actions', label: 'Actions' }
                ],
                isBusy: false,
                totalRows: 1,
                currentPage: 1,
                perPage: 5,
                pageOptions: [5, 10, 15],
                sortBy: '',
                sortDesc: false,
                sortDirection: 'asc',
                filter: null,
                ignoreFilterFields: ["id", "createdOn", "completion", "description", "submitter", "assignee", 'priority', "projectId"],

                options: this.staffs,

                ticketTitle: "",
                ticketDescription: "",
                priority: null,
                assigneeId: null,
                projectId: this.$route.params.projectId,
            }
        },
        watch: {
            projectTickets: function () {
               this.totalRows = this.projectTickets.length
            },
            staffs: function() {
                this.options = this.staffs
            }

        }
    }
</script>
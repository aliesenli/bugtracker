<template>
    <div class="hello">
        <div>
            <b-card
                header="Tickets for this Project"
                header-text-variant="white"
                header-tag="header"
                header-bg-variant="dark"
                border-variant="default"
            >
                <h3>Add Ticket</h3>
                <form @submit="onSubmit">
                    <input v-model="name" type="text" placeholder="Name">
                    <input v-model="prio" type="number" placeholder="Priority">
                    <input v-model="projectId" type="text" placeholder="Project id">
                    <input type="submit" value="Submit">
                </form>

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

                    <template v-slot:cell(priority)="data">
                        {{priorityText(data)}}
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
            </b-card>
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
                'createTicket'
            ]), 
            onSubmit(e) {
                e.preventDefault();
                this.createTicket(this.name, this.prio, this.projectId);
            },

            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = filteredItems.length
                this.currentPage = 1
            },

            priorityText(data){
                if (data.value == 0) return "Low"
                if (data.value == 1) return "Medium"
                return "High"
            },

            info(item) {
                this.$router.push({ name: 'Ticket', params: { ticketId: item.id }})
            }

        },
        computed: {
            ...mapGetters([
                'projectTickets',
                'projectName',
                'projectDescription'
            ]),
        },
        created() {
            this.fetchProjectTickets(this.$route.params.projectId);
        },
        data() {
            return {
                fields: [
                    { key: 'name', label: 'Ticket title', sortable: true, sortDirection: 'desc' },
                    { key: 'priority', label: 'Priority', sortable: true, sortDirection: 'desc' },
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

                name: '',
                prio: 1,
                projectId: '',
            }
        },
        watch: {
            projectTickets: function () {
                this.totalRows = this.projectTickets.length
            },
        }
    }
</script>


<template>
    <div>
        <h4>Your Tickets</h4>
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
            :items="userTickets"
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
                </template>

                <template v-slot:table-busy>
                    <div class="text-center text-danger my-2">
                    <b-spinner class="align-middle"></b-spinner>
                    <strong>Loading...</strong>
                    </div>
                </template>

                <template v-slot:cell(actions)="row" class="gully">
                    <b-dropdown size="lg"  variant="link" toggle-class="text-decoration-none" no-caret class="deckel">
                        <template v-slot:button-content>
                            <b-icon icon="grid3x2-gap"></b-icon><span class="sr-only">Search</span>
                        </template>
                        <b-dropdown-item @click="info(row.item)">Details</b-dropdown-item>
                        <b-dropdown-item href="#">Delete</b-dropdown-item>
                    </b-dropdown>           
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
    import jwt_decode from 'jwt-decode'

    export default {
        name: 'Home',
        methods: {
            ...mapActions([
                'fetchUserTickets',
            ]), 

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
                'userTickets',
            ]),
        },
        created() {
            const userId = jwt_decode(this.$store.state.auth.token).id
            this.fetchUserTickets(userId);
        },
        data() {
            return {
                fields: [
                    { key: 'title', label: 'Ticket title', sortable: true, sortDirection: 'desc' },
                    { key: 'description', label: 'Description', sortable: false },
                    { key: 'project', label: 'Project', sortable: true, sortDirection: 'desc'},
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
                ignoreFilterFields: ["id", "createdOn", "completion", "description", "submitter", "assignee", "projectId"],

                projectId: this.$route.params.projectId,
            }
        },
        watch: {
            userTickets: function () {
               this.totalRows = this.userTickets.length
            },

        }
    }
</script>

<style>
    .deckel .btn {
        padding: 0;
    }
</style>
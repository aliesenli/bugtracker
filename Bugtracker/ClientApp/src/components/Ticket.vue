<template>
    <div class="hello">
        <b-container fluid>
            <div class="mb-2">
                <b-card>
                    <div class="card-title-shifted">
                        <h5>Project-Details</h5>
                        <b-row>
                            <b-col>
                                      <a href="#" class="card-link">Card link</a>
                            <b-link href="#" class="card-link">Another link</b-link>     
                            </b-col>
                     
                        </b-row>
                    </div>
                    <b-card-text>
                    Some quick example text to build on the <em>card title</em> and make up the bulk of the card's
                    content.
                    </b-card-text>

                    <b-card-text>A second paragraph of text in the card.</b-card-text>
                </b-card>
                
                <hr>

                <b-card
                    header="Tickets for this Project"
                    header-text-variant="white"
                    header-tag="header"
                    header-bg-variant="dark"
                    border-variant="default"
                >

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
                        :items="allTickets"
                        :fields="fields"
                        :current-page="currentPage"
                        :per-page="perPage"
                        :filter="filter"
                        :filterIncludedFields="filterOn"
                        :sort-by.sync="sortBy"
                        :sort-desc.sync="sortDesc"
                        :sort-direction="sortDirection"
                        @filtered="onFiltered"
                    >
                        <template v-slot:table-busy>
                            <div class="text-center text-danger my-2">
                            <b-spinner class="align-middle"></b-spinner>
                            <strong>Loading...</strong>
                            </div>
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
        </b-container>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {
        name: 'Ticket',
        methods: {
            ...mapActions([
                'fetchTickets'
            ]),

            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = filteredItems.length
                this.currentPage = 1
            }
        },
        computed: {
            ...mapGetters([
                'allTickets',
                'pagination'
            ]),      
        },
        created() {
            this.fetchTickets();
        },
        data() {
            return {
                fields: [
                    { key: 'name', label: 'Ticket name', sortable: true, sortDirection: 'desc' },
                    { key: 'id', label: 'Ticket id', sortable: true },
                    { key: 'isActive', label: 'is Active', sortable: true },
                    { key: 'actions', label: 'Actions' }
                ],
                isBusy: true,
                totalRows: 1,
                currentPage: 1,
                perPage: 5,
                pageOptions: [5, 10, 15],
                sortBy: '',
                sortDesc: false,
                sortDirection: 'asc',
                filter: null,
                filterOn: [],
            }
        },
        watch: {
            allTickets: function () {
                this.totalRows = this.allTickets.length
                this.isBusy = !this.isBusy;
            },
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }
</style>

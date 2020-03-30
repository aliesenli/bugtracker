<template>
    <div>
        <h2>Projects</h2>
        <div class="p-3">
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
                :busy="isBusy"
                :items="allProjects"
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
    name: 'Project',

    methods: {
        ...mapActions([
            'fetchProjects'
        ]),
        onFiltered(filteredItems) {
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        info(item) {
            this.$router.push({ name: 'Project', params: { projectId: item.id }})
        }
    },

    computed: {
        ...mapGetters([
            'allProjects'
        ]),
    },
    
    created() {
        this.fetchProjects();
    },

    data() {
        return {
            fields: [
                { key: 'name', label: 'Project name', sortable: true, sortDirection: 'desc' },
                { key: 'description', label: 'Description', sortable: true },
                { key: 'createdOn', label: 'Created on', sortable: true },
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
            filterOn: []
        }
    }
}
</script>
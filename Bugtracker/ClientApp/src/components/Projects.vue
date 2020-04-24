<template>
    <div>
        <div>
            <b-button v-b-modal.modal-footer-sm variant="outline-primary">
                <b-icon icon="inbox-fill"></b-icon> Create New Project
            </b-button>

            <b-modal id="modal-footer-sm" size="lg" title="Create New Project" ref="new-project" hide-footer>
                <b-form @submit="onSubmit">
                    <b-form-group
                    class="mb-2"
                    id="input-group-1"
                    label="Project Name"
                    label-for="input-1"
                    >
                        <b-form-input
                        id="input-1"
                        type="text"
                        required
                        v-model="projectName"
                        placeholder="Project XYZ"
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
                        v-model="projectDescription"
                        placeholder="Enter your text"
                        ></b-form-textarea>
                    </b-form-group>

                    <b-form-group
                    label="Completion date"
                    label-for="datepicker"
                    class="mb-2"
                    >
                        <b-form-datepicker
                        id="datepicker"
                        v-model="completionDate"
                        ></b-form-datepicker>
                    </b-form-group>

                    <b-button type="submit" class="float-right mt-1" variant="primary">Submit</b-button>
                </b-form>
            </b-modal>
        </div>

        <h2 class="mt-3">Projects</h2>
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
                :filter-ignored-fields="ignoreFilterFields"
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
            'fetchProjects',
            'createProject'
        ]),
        onFiltered(filteredItems) {
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        info(item) {
            this.$router.push({ name: 'Project', params: { projectId: item.id }})
        },

        onSubmit(e) {
            e.preventDefault();
            this.createProject({
                name: this.projectName,
                description: this.projectDescription,
                completions: this.completionDate
            });
            
            this.$refs['new-project'].hide()
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
            filterOn: [],
            ignoreFilterFields: ["id", "createdOn", "completion", "tickets", "submitter", "assignee"],

            projectName: "",
            projectDescription: "",
            completionDate: "",

        }
    },
    watch: {
        allProjects: function () {
            this.totalRows = this.allProjects.length
        }
    }
}
</script>
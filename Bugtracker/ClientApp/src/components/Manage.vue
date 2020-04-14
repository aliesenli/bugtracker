<template>
  <div>
      <h3>Manage User Roles</h3>
    <b-row class="mt-3">
        <b-col cols="3">
          <h6>Select User</h6>
            <b-form-select v-model="staffSelected" :options="staffs" :select-size="6"></b-form-select>

            <div class="mt-3">
              <h6>Select the Role to assign</h6>
                <b-form-select v-model="roleSelected" :options="roles" class="mb-3">
                  <template v-slot:first>
                      <b-form-select-option :value="null" disabled>-- Please select a role --</b-form-select-option>
                  </template>
                </b-form-select>

            </div>
            <div>
                <b-button variant="primary" class="w-100">Submit</b-button>
            </div>
        </b-col>
<div class="vl"></div>
        <b-col cols="8">
            <div>
                <b-table
                :items="tableStaffs"
                :fields="fields"
                :sort-by.sync="sortBy"
                :sort-desc.sync="sortDesc"
                responsive="sm"
                ></b-table>
            </div>
        </b-col>
    </b-row>

  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
  export default {
    name: "Manage",
    methods: {
      ...mapActions([
        'fetchTableStaffs'
      ])
    },

    computed: {
        ...mapGetters([
            'staffs',
            'tableStaffs'
        ])
    },

    created() {
        this.$store.dispatch('fetchStaffs');
        this.fetchTableStaffs()
    },

    data() {
      return {
        staffSelected: null,
        roleSelected: null,
        roles: [
          { value: 'A', text: 'Admin' },
          { value: 'B', text: 'Submitter' },
          { value: 'C', text: 'Developer'}
        ],

        sortBy: 'age',
        sortDesc: false,
        fields: [
          { key: 'name', label: 'User' },
          { key: 'role', label: 'Current Role' },

        ],
      }
    },
  }
</script>

<style scoped>
  .vl {
    border-left: 1px solid rgb(199, 199, 199);
    height: 500px;
  }
</style>
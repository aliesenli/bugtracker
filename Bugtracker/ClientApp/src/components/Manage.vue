<template>
  <div>
    <h3>Manage User Roles</h3>
    <div class="p-2">
      <b-row class="mt-3">
          <b-col cols="12" md="4">
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
                  <b-button variant="primary" class="w-100" @click="assignUserRole">Submit</b-button>
              </div>
          </b-col>

          <b-col cols="12" md="7">
              <div>
                  <b-table class="mt-4"
                  :items="tableStaffs"
                  :fields="fields"
                  :sort-by.sync="sortBy"
                  :sort-desc.sync="sortDesc"
                  responsive="sm"
                  >

                  <template v-slot:cell(index)="data">
                    {{ data.index + 1 }}
                  </template>

                  <template v-slot:cell(role)="row">
                    <div v-for="role in row.item.role" :key="role.name">
                      {{ role }}
                    </div>
                  </template>
                  
                  </b-table>
              </div>
          </b-col>
      </b-row>
    </div>

  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
  export default {
    name: "Manage",
    methods: {
      ...mapActions([
        'fetchTableStaffs',
        'assignRole'
      ]),

      assignUserRole() {
        var payload = {
          user: this.staffSelected,
          role: this.roleSelected
        }
        this.assignRole(payload)
      }
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
          { value: 'Admin', text: 'Admin' },
          { value: 'Submitter', text: 'Submitter' },
          { value: 'Developer', text: 'Developer'}
        ],

        sortBy: 'age',
        sortDesc: false,
        fields: [
          'index',
          { key: 'name', label: 'User' },
          { key: 'role', label: 'Current Role(s)' },
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
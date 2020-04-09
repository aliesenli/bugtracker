<template>
    <div>
        <b-container>
            <div>
                <h1>Register</h1>
                <b-overlay :show="this.loading" rounded="sm">
                    <b-card title="Card with overlay" :aria-hidden="show ? 'true' : null">
                        <b-form @submit.prevent="register">
                            <b-form-group
                            id="input-group-1"
                            label="Email address:"
                            label-for="input-1"
                            >
                                <b-form-input
                                id="input-1"
                                v-model="email"
                                type="email"
                                required
                                placeholder="Enter email"
                                >
                                </b-form-input>
                            </b-form-group>

                            <b-form-group 
                            id="input-group-2" 
                            label="Your Password:" 
                            label-for="input-2"
                            >
                                <b-form-input
                                type="password"
                                id="input-2"
                                v-model="password"
                                required
                                placeholder="Enter name"
                                >
                                </b-form-input>
                            </b-form-group>
                            <b-button type="submit" variant="primary">Submit</b-button>
                        </b-form>
                    </b-card>
                </b-overlay>
            </div>
        </b-container>
    </div>
</template>

<script>
import { mapState } from 'vuex'

    export default {
        data(){
            return {
                email : "",
                password : "",
                is_admin : null,
                show: false
            }
        },

        computed: mapState({
            loading: state => state.auth.loading,
        }),

        methods: {
            register: function () {
                let data = {
                    email: this.email,
                    password: this.password,
                    //is_admin: this.is_admin
                }
                this.$store.dispatch('register', data)
               .then(() => this.$router.push('/'))
               .catch(err => console.log(err))
            }
        }
    }
</script>
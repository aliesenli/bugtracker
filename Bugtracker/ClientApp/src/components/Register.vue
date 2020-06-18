<template>
    <div>
        <div class="d-flex align-items-center min-vh-100" id="bg">
            <b-container class="bv-example-row">
                <b-row class="justify-content-md-center">
                    <b-col cols="12" md="5">
                        <b-overlay :show="this.loading" rounded="sm">
                            <b-card title="Create new Account" :aria-hidden="show ? 'true' : null">
                                <b-form @submit.prevent="register" class="mt-4">
                                    <b-form-input
                                    class="mt-2"
                                    v-model="email"
                                    type="email"
                                    required
                                    placeholder="Email"
                                    >
                                    </b-form-input>
                                    <b-form-input
                                    class="mt-2"
                                    type="password"
                                    v-model="password"
                                    required
                                    placeholder="Password"
                                    >
                                    </b-form-input>
                                    <b-button type="submit" variant="primary" class="w-100 mt-3">Register now</b-button>
                                </b-form>
                                <div class="mt-3 text-center"><b-link to="login">Already have an Account?</b-link></div>
                            </b-card>
                        </b-overlay>
                    </b-col>
                </b-row>
            </b-container>
        </div>
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
                }
                this.$store.dispatch('register', data)
               .then(() => this.$router.push('/'))
               .catch(err => console.log(err))
            },
            loginPage: function() {
                this.$route.push("/login")
            }
        }
    }
</script>

<style scoped>
    #bg {
        background-color: #062646;
    }
</style>
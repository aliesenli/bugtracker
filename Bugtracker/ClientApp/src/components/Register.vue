<template>
    <div>
        <div class="d-flex align-items-center min-vh-100" id="bg">
            <b-container class="bv-example-row">
                <b-row class="justify-content-md-center">
                    <b-col cols="12" md="5">
                        <div class="text-center">
                            <img src="../../public/logo.png" class="rounded" width="50">
                        </div>
                        <h2 class="text-light text-center mb-4">Create a new Account</h2>
                        <b-overlay :show="this.loading" rounded="sm">
                            <b-card :aria-hidden="show ? 'true' : null">

                                <b-form @submit.prevent="register">
                                    <b-form-group id="email-group" class="mt-2" label-for="email-input" label="Email">
                                        <b-form-input id="email-input" type="email" v-model="email" required></b-form-input>
                                    </b-form-group>

                                    <b-form-group id="password-group" class="mt-2" label-for="password-input" label="Password">
                                        <b-form-input id="password-input" type="password" v-model="password" required></b-form-input>
                                    </b-form-group>

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
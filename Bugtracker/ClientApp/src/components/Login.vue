<template>
    <div>
        <div class="d-flex align-items-center min-vh-100" id="bg">
            <b-container class="bv-example-row">
                <b-row class="justify-content-md-center">
                    <b-col cols="12" md="5">
                        <b-overlay :show="this.loading" rounded="sm">
                            <b-card title="Login" :aria-hidden="show ? 'true' : null">
                                <b-form @submit.prevent="login">
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
                                    <b-button type="submit" variant="primary" class="w-100 mt-3">Sign In</b-button>
                                </b-form>
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
    
    data() {
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
        login: function () {
            let email = this.email 
            let password = this.password
            this.$store.dispatch('login', { email, password })
            .then(() => this.$router.push('/'))
            .catch(err => console.log(err))
        }
    }
}
</script>

<style scoped>
    #bg {
        background-color: #062646;
    }
</style>


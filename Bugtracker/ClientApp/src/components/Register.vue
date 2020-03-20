<template>
    <div>
        <h4>Register</h4>
        <form @submit.prevent="register">

            <label for="email" >E-Mail Address</label>
            <div>
                <input id="email" type="email" v-model="email" required>
            </div>
             
            <label for="password">Password</label>
            <div>
                <input id="password" type="password" v-model="password" required>
            </div>

            <div>
                <button type="submit">Register</button>
            </div>
        </form>
    </div>
</template>

<script>
    export default {
        data(){
            return {
                email : "",
                password : "",
                is_admin : null
            }
        },
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
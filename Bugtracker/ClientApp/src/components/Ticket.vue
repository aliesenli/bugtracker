<template>
    <div class="hello">
        <h1>{{ ticket }}</h1>
        <button v-on:click="callApi()">Call Api</button>

        <div class="todos">
            <div v-for="todo in allTickets" :key="todo.id" class="todo">
                {{ todo.name }}
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    export default {
        name: 'Ticket',
        props: {
            ticket: String
        },
        computed: mapGetters(['allTickets'])
        /*
        methods: {
            callApi() {
                fetch("http://localhost:44340/api/tickets")
                    .then(response => response.json())
                    .then(data => {
                        console.log(data) // Prints result from `response.json()` in getRequest
                        this.ticket = data[0].name;
                    });
            }
        }
        */
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

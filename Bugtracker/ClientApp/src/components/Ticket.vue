<template>
    <div class="hello">
        <div class="todos">
            <table class="table table-dark">
                <thead>
                    <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">Created At</th>
                    <th scope="col">Updated At</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="todo in allTickets" :key="todo.id" class="todo">
                        <th scope="row">1</th>
                        <td>{{ todo.name }}</td>
                        <td>{{ todo.createdAt }}</td>
                        <td>{{ todo.updatedAt }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {
        name: 'Ticket',
        methods: {
            ...mapActions(['fetchTickets'])
        },
        computed: mapGetters(['allTickets']),
        created() {
            this.fetchTickets();
        }
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

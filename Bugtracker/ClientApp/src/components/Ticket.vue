<template>
    <div class="main-content"> 
        <div class="main-content__body">

            <b-row>
                <b-col cols="12" md="8">
                    <div>
                        <div>
                            <b-button-toolbar key-nav aria-label="Toolbar with button groups">
                                <b-button-group class="mx-1">
                                <b-button>&laquo;</b-button>
                                <b-button>&lsaquo;</b-button>
                                </b-button-group>
                                <b-button-group class="mx-1">
                                <b-button>Edit</b-button>
                                <b-button>Undo</b-button>
                                <b-button>Redo</b-button>
                                </b-button-group>
                                <b-button-group class="mx-1">
                                <b-button>&rsaquo;</b-button>
                                <b-button>&raquo;</b-button>
                                </b-button-group>
                            </b-button-toolbar>
                        </div>
                    </div>
                </b-col>
                <b-col cols="6" md="4">
                    <div>
                        
                    </div>
                </b-col>
            </b-row>

        </div>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: 'Ticket',

    methods: {
        ...mapActions([
            'fetchTicket'
        ]),
    },

    computed: {
        ...mapGetters([
            'getTicket'
        ]),
    },
    
    created() {
        this.fetchTicket(this.$route.params.ticketId);
        console.log(this.getTicket)
    },

    data() {
        return {
            
        }
    }
}
</script>
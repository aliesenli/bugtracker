<template>
    <div id="app" :class="spacingClass">
        <div id="wrapper" :class="wrapperClass" v-show="spacingClass">
            <MenuToggleBtn></MenuToggleBtn>

            <Menu></Menu>

            <ContentOverlay></ContentOverlay>
        </div>
        <router-view></router-view>
    </div>
</template>

<script>
import { mapState } from 'vuex'

import MenuToggleBtn from '@/components/MenuToggleBtn.vue'
import Menu from '@/components/Menu.vue'
import ContentOverlay from '@/components/ContentOverlay.vue'

export default {

    components: {
        MenuToggleBtn,
        Menu,
        ContentOverlay,
    },

    created() {
        window.bus.$on('menu/toggle', () => {
            window.setTimeout(() => {
                this.isOpenMobileMenu = !this.isOpenMobileMenu;
            }, 200);
        });

        window.bus.$on('menu/closeMobileMenu', () => {
            this.isOpenMobileMenu = false;
        });
    
        if(this.$route.path == "/login") {
            this.$store.dispatch("clear");
        }
    },

    data() {
        return {
            isOpenMobileMenu: false,
        };
    },

     computed: {
         wrapperClass() {
            return {
                'toggled': this.isOpenMobileMenu === true
            }
        },
        spacingClass() {
            return {
                'spacing': this.spacing
            }
        },
          ...mapState({
        spacing: state => state.menu.spacing,
        })
    },

    watch:{
        $route (){
            if (this.$route.path == "/login" || this.$route.path == "/register") {
                this.$store.dispatch('clear')
            } else {
                this.$store.dispatch('set')
            }
        }
    } 
    
}
</script>

<style>
    #nav {
        padding: 30px;
    }

    #nav a {
        font-weight: bold;
        color: #2c3e50;
    }

    #nav a.router-link-exact-active {
        color: #42b983;
    }
</style>


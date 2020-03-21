<template>
    <div id="appli">
        <div id="wrapper" :class="wrapperClass" v-show="!hideMenu">
            <MenuToggleBtn></MenuToggleBtn>

            <Menu></Menu>

            <ContentOverlay></ContentOverlay>
        </div>

        <router-view></router-view>
    </div>
</template>

<script>
// @ is an alias to /src
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

        if (this.$route.path == "/login" || this.$route.path == "/register") {
            this.hideMenu = true
            //document.getElementById("appli").classList.remove("applis")
        } else {
            this.hideMenu = false
            //document.getElementById("appli").classList.add("applis")
        }

        console.log(this.hideMenu);
    },

    data() {
        return {
            isOpenMobileMenu: false,
            hideMenu: false
        };
    },

    computed: {
        wrapperClass() {
            return {
                'toggled': this.isOpenMobileMenu === true,
            };
        }
    }


}
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }

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


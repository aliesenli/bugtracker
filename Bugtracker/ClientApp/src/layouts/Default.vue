<template>
    <div id="wrapper" :class="wrapperClass">
        <MenuToggleBtn/>
        <Menu/>
        <ContentOverlay/>
    </div>
</template>

<script>
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
    },

    computed: {
        wrapperClass() {
            return {
                'toggled': this.isOpenMobileMenu === true
            };
        }
    },

    data() {
        return {
            isOpenMobileMenu: false,
        };
    },
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
<template>
    <div id="app">
        <component :is="layout">
        </component>
        <router-view></router-view>
    </div>
</template>

<script>
const default_layout = "default";

export default {

    computed: {
        layout() {
            return (this.$route.meta.layout || default_layout) + '-layout';
        }
    },
}
</script>


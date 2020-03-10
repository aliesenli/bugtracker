<template>

  <div class="menu-container">

    <!-- root level itens -->
    <ul class="menu">

      <li class="menu__top">
        <router-link to="/" class="menu__logo">
          <img src="/icon-32.png" alt="icon">
        </router-link>
        <a
        href="#"
        @click.prevent="openProjectLink"
        class="menu__title"
        >
          Bugtracker
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('home')"
        :class="highlightSection('home')"
        >
            <i class="fa fa-home menu__icon" aria-hidden="true"></i>
            Home
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('projects')"
        :class="highlightSection('projects')"
        >
          <i class="fa fa-tag menu__icon" aria-hidden="true"></i>
          Projects
          <i class="fa fa-chevron-right menu__arrow-icon" aria-hidden="true"></i>
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('customers')"
        :class="highlightSection('customers')"
        >
          <i class="fa fa-users menu__icon" aria-hidden="true"></i>
          Customers
          <i class="fa fa-chevron-right menu__arrow-icon" aria-hidden="true"></i>
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('account')"
        :class="highlightSection('account')"
        >
          <i class="fa fa-user menu__icon" aria-hidden="true"></i>
          Account
          <i class="fa fa-chevron-right menu__arrow-icon" aria-hidden="true"></i>
        </a>
      </li>

    </ul>

    <!-- context menu: childs of root level itens -->
    <transition name="slide-fade">

      <div class="context-menu-container" v-show="showContextMenu">

        <ul class="context-menu">

          <li v-for="(item, index) in menuItems" :key="index">

            <h6 v-if="item.type === 'title'" class="context-menu__title">

              <i :class="item.icon" aria-hidden="true"></i>

              {{item.txt}}

              <a
              v-if="index === 0"
              @click.prevent="closeContextMenu"
              class="context-menu__btn-close"
              href="#"
              >
                <i class="fa fa-window-close" aria-hidden="true"></i>
              </a>

            </h6>

            <a
            v-else
            href="#"
            @click.prevent="openSection(item)"
            :class="subMenuClass(item.txt)"
            >
              {{item.txt}}
            </a>

          </li>

        </ul>

      </div>

    </transition>

  </div>

</template>

<script>
import menuData from './support/menu-data';
import kebabCase from 'lodash/kebabCase';

export default {
  name: 'Menu',

  data(){
    return {
      contextSection: '',

      menuItems: [],

      menuData: menuData,

      activeSubMenu: ''
    }
  },

  methods: {

    openProjectLink() {
      alert('You could open the project frontend in another tab here, so the logged admin could see changes made to the project ;)');
    },

    updateMenu(context) {
      this.contextSection = context;
      this.menuItems = this.menuData[context];

      if (context === 'home') {
        this.$router.push('/');
        window.bus.$emit('menu/closeMobileMenu');
      }
    },

    highlightSection(section) {
      return {
        'menu__link': true,
        'menu__link--active': section === this.contextSection,
      };
    },

    subMenuClass(subMenuName) {
      return {
        'context-menu__link': true,
        'context-menu__link--active': this.activeSubMenu === subMenuName,
      };
    },

    closeContextMenu() {
      this.contextSection = '';
      this.menuItems = [];
    },

    openSection(item) {
      this.activeSubMenu = item.txt;

      this.$router.push(this.getUrl(item));
      window.bus.$emit('menu/closeMobileMenu');
    },

    getUrl(item) {
      let sectionSlug = kebabCase(item.txt);

      return `${item.link}/${sectionSlug}`;
    }

  },

  computed: {
    showContextMenu() {
      return this.menuItems.length;
    },
  }

}
</script>

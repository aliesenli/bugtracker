export default {

  // home is a section without childs, set as an empty array
  home: [],

  projects: [

    {
      type: 'title',
      txt: 'Projects',
      //icon: 'fa fa-tag context-menu__title-icon',
      icon: 'fas fa-project-diagram context-menu__title-icon'
    },

    {
      type: 'link',
      txt: 'All Projects',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Add New Product',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Manage Categories',
      link: '/page',
    },

  ],

  customers: [

    {
      type: 'title',
      txt: 'Customers',
      icon: 'fa fa-users context-menu__title-icon',
    },

    {
      type: 'link',
      txt: 'List Customers',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'List Contacts',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'List Newsletters',
      link: '/page',
    },

  ],

  account: [

    {
      type: 'title',
      txt: 'My Account',
      icon: 'fa fa-user context-menu__title-icon',
    },

    {
      type: 'link',
      txt: 'Change Password',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Change Settings',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Logout',
      link: '/page',
    },

    {
      type: 'title',
      txt: 'Change Subscription',
      icon: 'fa fa-credit-card context-menu__title-icon',
    },

    {
      type: 'link',
      txt: 'Plans',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Payment Settings',
      link: '/page',
    },

    {
      type: 'link',
      txt: 'Payment History',
      link: '/page',
    },

  ],

};

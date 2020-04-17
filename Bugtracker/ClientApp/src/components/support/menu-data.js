export default {
  // home is a section without childs, set as an empty array
  home: [],

  projects: [
    {
      type: 'title',
      txt: 'Projects',
      icon: 'fas fa-project-diagram context-menu__title-icon'
    },

    {
      type: 'link',
      txt: 'Projects',
      link: '',
    },

  ],

  manage: [
    {
      type: 'title',
      txt: 'Manage',
      icon: 'fa fa-users context-menu__title-icon',
    },

    {
      type: 'link',
      txt: 'Manage Roles',
      link: '',
    }
  ],

  account: [
    {
      type: 'title',
      txt: 'My Account',
      icon: 'fa fa-user context-menu__title-icon',
    },

    {
      type: 'logout',
      txt: 'Logout',
    },
  ],
};

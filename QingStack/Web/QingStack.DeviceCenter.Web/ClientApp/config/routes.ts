export default [

  {
    path: '/authorization',
    layout: false,
    routes: [
      {
        name: 'login',
        path: '/authorization/login',
        component: './authorization/login',
      },
      {
        path: '/authorization/login-callback',
        component: './authorization/login-callback',
      },
      {
        path: '/authorization/logout',
        component: './authorization/logout',
      },
      {
        path: '/authorization/logout-callback',
        component: './authorization/logout-callback',
      },
      {
        component: './404',
      }
    ],
  },
  {
    path: '/user',
    layout: false,
    routes: [
      {
        path: '/user',
        routes: [
          {
            name: 'login',
            path: '/user/login',
            component: './user/Login',
          },
        ],
      },
      {
        component: './404',
      },
    ],
  },
  {
    path: '/welcome',
    name: 'welcome',
    icon: 'smile',
    component: './Welcome',
  },
  {
    path: '/admin',
    name: 'admin',
    icon: 'crown',
    access: 'canAdmin',
    component: './Admin',
    routes: [
      {
        path: '/admin/sub-page',
        name: 'sub-page',
        icon: 'smile',
        component: './Welcome',
      },
      {
        component: './404',
      },
    ],
  },
  {
    name: 'list.table-list',
    icon: 'table',
    path: '/list',
    component: './TableList',
  },
  {
    name: 'list.customer.table',
    path: '/customer',
    icon: 'AppleOutlined',
    component: './customer/index',
  },
  {
    name: 'list.product.table',
    path: '/products',
    icon: 'RadarChartOutlined',
    component: './products',
  },
  {
    path: '/',
    redirect: '/welcome',
  },
  {
    component: './404',
  },
];

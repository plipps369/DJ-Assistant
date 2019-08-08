import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/views/Home.vue';
import Dashboard from '@/views/Dashboard.vue';
import Login from '@/views/Login.vue';
import StoreFront from '@/views/StoreFront.vue';
import About from '@/views/About.vue';
import Account from '@/views/Account.vue';
import NewProduct from '@/views/NewProduct.vue';
import AllProducts from '@/views/AllProducts.vue';
import auth from "@/shared/auth";

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/account',
      name: 'account',
      component: Account
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: Dashboard
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/storefront',
      name: 'storefront',
      component: StoreFront
    },
    {
      path: '/about',
      name: 'about',
      component: About
    },
    {
      path: '/newproduct',
      name: 'newproduct',
      component: NewProduct
    },
    {
      path: '/allproducts',
      name: 'allproducts',
      component: AllProducts
    }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/register'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = auth.getUser();

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
});

export default router;
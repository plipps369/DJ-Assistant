import Vue from 'vue'
import Router from 'vue-router'
import auth from './auth'
import Home from './views/Home.vue'
import Login from './views/Login.vue'
import Register from './views/Register.vue'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/about',
      name: 'about',
      meta: {
        requiresAuth: false
      },
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);
  const user = auth.getUser();

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && !user) {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;

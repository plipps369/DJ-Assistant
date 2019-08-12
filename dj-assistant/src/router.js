import Vue from 'vue'
import Router from 'vue-router'
import auth from './auth'
import Home from './views/Home.vue'
import Login from './views/Login.vue'
import Register from './views/Register.vue'
import Dashboard from './views/Dashboard.vue'
import AddSong from './views/AddSong.vue'
import CreateParty from './views/CreateParty.vue'
import Party from './views/Party.vue'
import Guest from './views/Guest.vue'
import GuestParty from './views/GuestParty.vue'

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
      path: '/dashboard/',
      name: 'dashboard',
      component: Dashboard,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/party/:id',
      name: 'party',
      component: Party,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/guest-party/:partyName',
      name: 'guest-party',
      component: GuestParty,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/guest/',
      name: 'guest',
      component: Guest,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/add-song',
      name: 'add-song',
      component: AddSong,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/create-party',
      name: 'create-party',
      component: CreateParty,
      meta: {
        requiresAuth: false
      }
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
    { 
      path: '*', 
      redirect: '/' 
    }
  ]
});

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
<template>
  <div class="navHeader">
    <!-- <header>
      <div class="container-fluid">
        <div class="col-lg-12">
          <img id="logo" src="@/assets/dj-logo-blue.png" class="img-responsive" />
          <span id="header-text">DJ Jazzy Jeff</span>
        </div>
      </div>
    </header> -->

    <b-navbar toggleable="lg" type="dark" variant="primary" fixed="top">
      <img id="logo" src="@/assets/dj-logo-white.png" class="img-responsive" />
       <b-navbar-brand href="#">DJ App</b-navbar-brand>


      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item>
            <router-link :to="{name: 'home'}" class="nav-link">Home</router-link>
            </b-nav-item>
          <b-nav-item>
            <router-link :to="{name: 'dashboard'}" class="nav-link">Dashboard</router-link>
          </b-nav-item>
          <b-nav-item>
            <router-link :to="{name: 'login'}" class="nav-link">Login</router-link>
          </b-nav-item>
          <b-nav-item>
            <router-link :to="{name: 'register'}" class="nav-link">Register</router-link>
          </b-nav-item>
          <b-nav-item>
            <router-link :to="{name: 'guest'}" class="nav-link">Guest</router-link>
          </b-nav-item>
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown right>
            <!-- Using 'button-content' slot -->
            <template slot="button-content">
              <em>{{user.displayName}}</em>
            </template>
            
            <b-dropdown-item  v-on:click="logout">Sign Out</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <!-- <nav class="navbar sticky-top navbar-expand-lg navbar-dark bg-primary">
      <a class="navbar-brand" href="#">DJ App</a>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <router-link :to="{name: 'home'}" class="nav-link">
              Home
            </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'about'}" class="nav-link">
              About
              </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'login'}" class="nav-link">
              Login
              </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'register'}" class="nav-link">
              Register
              </router-link>
          </li>
        </ul>
      </div>
    </nav>-->
  </div>
</template>



<script>

import auth from "../auth";


export default {
  name: "nav-header",
  data() {
    return {
      user: {},
     };
  },
  methods: {
    logout() {
      auth.logout();
    },
    getUser() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ`, {
        method: "GET",
        headers: {
          // A Header with our authentication token.
          Authorization: "Bearer " + auth.getToken()
        }
      })
        .then(response => response.json())
        .then(json => {
          this.user = json;
        });
    },
  },
  created(){
    this.getUser();
  }
 
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Saira+Stencil+One&display=swap");

#logo {
  height: 3rem;
  margin-right: 1.5rem;
 
}
#header-text {
 
  font-size: 4rem;
  vertical-align: middle;
  color: #007bff;
  font-family: "Saira Stencil One", cursive;
}

.navHeader {
   padding-bottom: 100px;
}
</style>

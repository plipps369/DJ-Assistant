<template>
  <div class="navHeader">
    <b-navbar toggleable="lg" type="dark" variant="primary" fixed="top">
      <img id="logo" src="@/assets/dj-logo-white.png" class="img-responsive" />
      <h3>
        <b-navbar-brand id="name"><router-link :to="{name: 'home'}" class="navbar-brand">DJ InteRUPPter</router-link></b-navbar-brand>
      </h3>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <!-- <b-nav-item>
            <router-link :to="{name: 'home'}" class="nav-link">Home</router-link>
          </b-nav-item> -->
          <b-nav-item v-if="user">
            <router-link  :to="{name: 'dashboard'}" class="nav-link">Dashboard</router-link>
          </b-nav-item> 
          <b-nav-item v-if="!user">
            <router-link :to="{name: 'guest'}" class="nav-link">Guest</router-link>
          </b-nav-item>
          <b-nav-item v-if="!user">
            <router-link  :to="{name: 'login'}" class="nav-link">Login</router-link>
          </b-nav-item >
          <b-nav-item id="weird" v-if="user" v-on:click="logout">Logout
            </b-nav-item>
          <!-- <b-nav-item>
            <router-link :to="{name: 'register'}" class="nav-link">Register</router-link>
          </b-nav-item> -->
         
        </b-navbar-nav>

        
        
      </b-collapse>
    </b-navbar>
  </div>
</template>



<script>
import auth from "../auth";

export default {
  name: "nav-header",
  data() {
    return {
      user: {}
    };
  },
  methods: {
    logout() {
      auth.logout();
      this.$router.push({
        name: "login",
        query: { logout: true }
      });
    },
    getUser() {
      this.user = auth.getUser();
    }
    // getUser() {
    //   fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ`, {
    //     method: "GET",
    //     headers: {
    //       // A Header with our authentication token.
    //       Authorization: "Bearer " + auth.getToken()
    //     }
    //   })
    //     .then(response => response.json())
    //     .then(json => {
    //       this.user = json;
    //     });
    // }
  },
  created() {
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

#weird {
  padding-top: 7px;
}

#name {
  padding-top: 7px;
}

.navHeader {
  padding-bottom: 100px;
}
</style>

<template>
  <div id="login" class="text-center">
    <nav-header></nav-header>

    <img class="loginBackground" src="@/assets/Soundwave.jpg" fluid-grow alt="Fluid-grow image"/>
    
    <form class="form-signin" @submit.prevent="login">

      <div class="allInputFields" style="position: relative;">
      <h1 class="h1-reponsive white-text text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Please Sign In</strong></h1>
     
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div class="alert alert-success" role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="loginText">
      <label for="email" class="sr-only">Email</label>
      <input
        type="email"
        id="email"
        class="form-control"
        placeholder="Email"
        v-model="user.email"
        required
        autofocus
      />
      </div>
      <div class="loginText">
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      </div>
      </div>

      <router-link :to="{ name: 'register' }">Need an account?</router-link>
      
      <br>
    
      <button type="Submit" class="btn btn-outline-warning">Sign in</button>
    </form>
  </div>
</template>

<script>
import auth from '../auth';
import NavHeader from '@/components/NavHeader.vue'

export default {
  name: 'login',
  components: {
    NavHeader
  },
  data() {
    return {
      user: {
        email: '',
        password: '',
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ/login`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => {
          if (response.ok) {
            return response.text();
          } else {
            this.invalidCredentials = true;
          }
        })
        .then((token) => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, '');
            }
            auth.saveToken(token);
            this.$router.push('/');
          }
        })
        .catch((err) => console.error(err));
    },
  },
};
</script>

<style>
#login{
  margin-top:15vh;
  position: relative;
  z-index: 1;
}
.form-signin{
  margin-right: 25vw;
  margin-left: 25vw;
  margin-top: -500px;
}

.loginText{
   margin: 20px;
   text-align: center;
   position: relative;
}
.form-control{
  position: relative;
  z-index: 1000;
}
</style>

<template>
  <div id="login" class="text-center">
    <nav-header></nav-header>

    <form class="form-signin" @submit.prevent="login">
      <div class="allInputFields" style="position: relative;">
        <h1
          class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
          data-wow-delay="0.3s"
        >
          <strong>Please Sign In</strong>
        </h1>
        <div
          class="alert alert-danger"
          role="alert"
          v-if="invalidCredentials"
        >Invalid username and password!</div>
        <div
          class="alert alert-success"
          role="alert"
          v-if="this.$route.query.registration"
        >Thank you for registering, please sign in.</div>
        <div
          class="alert alert-success"
          role="alert"
          v-if="this.$route.query.logout"
        >You have been successfully logged out.</div>

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

      <div class="buttonLinks">
        <button type="Submit" id="myButton" class="btn btn-danger btn-lg btn-block">Sign In</button>
        <br />

        <router-link
          :to="{ name: 'register' }"
          tag="button"
          class="btn btn-dark btn-lg btn-block"
          id="registerPrompt"
        >Need an account?</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import auth from "../auth";
import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
const apiService = new APIService();

export default {
  name: "login",
  components: {
    NavHeader
  },
  data() {
    return {
      user: {
        email: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    async login() {
      this.error = "";
      try {
        let token = await apiService.login(this.user);
        auth.saveToken(token);
        this.$router.push({
          path: "/dashboard"
        });
      } catch (error) {
        this.error = error;
        this.invalidCredentials = true;
      }
    }
  }
};
</script>

<style>
#login {
  margin-top: 10vh;
  position: relative;
  z-index: 1;
}
.form-signin {
  margin-right: 15vw;
  margin-left: 15vw;
  margin-top: -7vh;
}

div .buttonLinks {
  display: block;
  padding-left: 4%;
  padding-right: 4%;
}

.loginText {
  margin: 20px;
  text-align: center;
  position: relative;
}
.form-control {
  position: relative;
  z-index: 1000;
}

h1 {
  color: white;
  text-shadow: 4px 2px 4px rgb(2, 170, 234), 0 0 25px rgb(0, 64, 87),
    0 0 5px red;
}
</style>

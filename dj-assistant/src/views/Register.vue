<template>
  <div id="register" class="text-center">
    <nav-header></nav-header>

    <form class="form-register" @submit.prevent="register">
      <div class="allInputFields" style="position: relative;">
        <h1
          class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
          data-wow-delay="0.3s"
        >
          <strong>Create Account</strong>
        </h1>
        <div
          class="alert alert-danger"
          role="alert"
          v-if="registrationErrors"
        >There were problems registering this user.</div>
        <div class="inputText">
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
        <div class="inputText">
          <label for="confirmEmail" class="sr-only">Confirm Email</label>
          <input
            type="email"
            id="confirmEmail"
            class="form-control"
            placeholder="Confirm Email"
            v-model="user.confirmEmail"
            required
          />
        </div>
        <div class="inputText">
          <label for="displayName" class="sr-only">Display Name</label>
          <input
            type="text"
            id="displayName"
            class="form-control"
            placeholder="Display Name"
            v-model="user.displayName"
            required
          />
        </div>
        <div class="inputText">
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
        <div class="inputText">
          <input
            type="password"
            id="confirmPassword"
            class="form-control"
            placeholder="Confirm Password"
            v-model="user.confirmPassword"
            required
          />
        </div>
      </div>
      <div class="registerButtonLinks">
        <button type="Submit" id="regButton" class="btn btn-danger btn-lg btn-block">Register</button>

        <br />

        <router-link
          :to="{ name: 'login' }"
          tag="button"
          class="btn btn-dark btn-lg btn-block"
          id="haveAccount"
        >Have an account?</router-link>
        <br />
      </div>
    </form>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";

export default {
  name: "register",
  components: {
    NavHeader
  },
  data() {
    return {
      user: {
        email: "",
        confirmEmail: "",
        displayName: "",
        password: "",
        confirmPassword: "",
        role: "user"
      },
      registrationErrors: false
    };
  },
  methods: {
    register() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ/register`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.user)
      })
        .then(response => {
          if (response.ok) {
            this.$router.push({
              path: "/login",
              query: { registration: true }
            });
          } else {
            this.registrationErrors = true;
          }
        })

        .then(err => console.error(err));
    }
  }
};
</script>

<style>
#register {
  margin-top: 68vh;
  position: relative;
  z-index: 1;
}

.form-register {
  margin-right: 15vw;
  margin-left: 15vw;
  margin-top: -500px;
}

.form-control {
  position: relative;
  z-index: 1000;
}

.inputText {
  margin-top: 20px;
  margin-bottom: 20px;
  text-align: center;
}

div .registerButtonLinks {
  display: block;
  padding-left: 4%;
  padding-right: 4%;
}
</style>

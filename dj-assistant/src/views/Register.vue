<template>
  <div id="register" class="text-center">
     <img class="registerBackground" src="@/assets/Soundwave.jpg" fluid-grow alt="Fluid-grow image"/>
    <nav-header></nav-header>
      <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="registrationErrors"
      >There were problems registering this user.</div>
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
      <label for="confirmEmail" class="sr-only">Confirm Email</label>
      <input
        type="email"
        id="confirmEmail"
        class="form-control"
        placeholder="Confirm Email"
        v-model="user.confirmEmail"
        required
      />
      <label for="displayName" class="sr-only">Display Name</label>
      <input
        type="text"
        id="displayName"
        class="form-control"
        placeholder="Display Name"
        v-model="user.displayName"
        required
      />
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
      <br>
      <button type="Submit" class="btn btn-outline-warning">Submit</button>
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
      fetch(`${process.env.VUE_APP_REMOTE_API}/register`, {
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
              query: { registration: "success" }
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
#register{
  margin-top:15vh;
  
}
.form-register{
  margin-right: 25vw;
  margin-left: 25vw;
  margin-top: -450px;
}

.registerBackground {
    background-image: url(/assets/Soundwave.jpg);
    background-position: center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    height: 100%;
    
}
</style>

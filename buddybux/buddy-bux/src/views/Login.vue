<!-- 
  The Login Page displays a register and login screen for the user to authenticate.
-->

<template>
  <div id="login">
    <section id="login-signup" v-bind:class="{ showSignupForm: !showLoginForm }">
      <form v-if="showLoginForm" v-on:submit.prevent="login">
        <div class="form-group">
            <label for="email">Username:</label>
            <input
            v-model.trim="loginForm.email"
            type="text"
            placeholder="Email"
            id="email"
            />
        </div>

        <div class="form-group">
            <label for="password">Password:</label>
            <input
            v-model.trim="loginForm.password"
            type="password"
            placeholder="Password"
            id="password"
            />
        </div>

        <div class="form-actions">
          <div>
            <a v-on:click="toggleForm">Create an Account</a>
          </div>
          <button type="submit" class="btn btn-primary">Login</button>          
        </div>
      </form>

      <form v-if="!showLoginForm" v-on:submit.prevent="signup">
        <div class="form-group">
            <label for="signUpEmail">Username:</label>
            <input
            v-model.trim="signupForm.email"
            type="text"
            placeholder="Email"
            id="signUpEmail"
            />
        </div>

        <div class="form-group">
            <label for="signupFormPassword">Password:</label>
            <input
            v-model.trim="signupForm.password"
            type="password"
            placeholder="Password"
            id="signupFormPassword"
            />
        </div>

        <div class="form-group">
            <label for="firstName">First Name:</label>
            <input
            v-model.trim="signupForm.firstName"
            type="text"
            placeholder="First Name"
            id="firstName"
            />
        </div>

        <div class="form-group">
            <label for="lastName">Last Name:</label>
            <input
            v-model.trim="signupForm.lastName"
            type="text"
            placeholder="Last Name"
            id="lastName"
            />
        </div>

        <div class="form-group">
            <label for="currencyName">Currency Name:</label>
            <input
            v-model.trim="signupForm.currencyName"
            type="text"
            placeholder="Currency Name"
            id="currencyName"
            />
        </div>

        <div class="form-actions">
          <div>
            <a v-on:click="toggleForm">Switch to Login</a>
          </div>
          <button type="submit" class="btn btn-primary">Register</button>          
        </div>
      </form>
    </section>
    <error-message v-bind:error="error"></error-message>
  </div>
</template>

<script>
import auth from "@/shared/auth";
import ErrorMessage from "@/components/ui/ErrorMessage.vue";
import {APIService} from '@/shared/APIService';
const apiService = new APIService();

export default {
  components: { ErrorMessage },
  data() {
    return {
      showLoginForm: true,
      error: "",
      /** Contents of the login form */
      loginForm: {
        email: "",
        password: ""
      },
      /** Contents of the sign up form */
      signupForm: {
        password: "",
        email: "",
        currencyName: "",
        firstName: "",
        lastName: ""
      }
    };
  },
  methods: {
    /**
     * Toggles the showLoginform
     * @function
     */
    toggleForm() {
      this.showLoginForm = !this.showLoginForm;
      this.error = "";
    },
    /**
     * Navigates the user to the home route.
     * @function
     */
    goDashboard() {
      this.$router.push("/dashboard");
    },
    /**
     * Logs the user in and then sends them to the dashboard.
     * NOTE: Uses async/await
     */
    async login() {
      this.error = "";
      try {
        let token = await apiService.login(this.loginForm);
        auth.saveToken(token);
        this.goDashboard();
      } catch (error) {
        this.error = error;
      }
    },
    /**
     * Signs the user up and then redirects them to the dashboard.
     */
    async signup() {
      this.error = "";
      try {
        let token = await apiService.register(this.signupForm);
        auth.saveToken(token);
        this.goDashboard();
      } catch (error) {
        console.error(error);
        this.error = "There was an error attempting to register";
      }
    }
  }
};
</script>

<style scoped>
form label {
    padding-right: 10px;
}

#login {
    padding-top: 20px;
}

#login button {
    margin-top: 10px;
}
</style>

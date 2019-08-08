<template>
  <div id="login" class="text-center">
    <nav-header></nav-header>

    
    
    <form class="form-signin" @submit.prevent="login">

      <div class="allInputFields" style="position: relative;">
      <h1 class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Please Sign In</strong></h1>
     
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

      
      
      <br>
    
      <button type="Submit" id="myButton" class="btn btn-outline-warning">SIGN IN</button>


      <router-link :to="{ name: 'register' }" id="registerPrompt"> Need an account?</router-link>
    </form>
  </div>
</template>

<script>
import auth from '../auth';
import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
const apiService = new APIService();

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
    async login() {
      this.error = "";
      try {
        let token = await apiService.login(this.user);
        auth.saveToken(token);
        this.$router.push({
              path: "/dashboard",
              
            });
      } catch (error) {
        this.error = error;
        this.invalidCredentials = true
      }
    }
  }
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

h1 {
color: white;
  text-shadow: 2px 2px 3px rgb(255, 0, 140), 0 0 25px salmon, 0 0 5px pink;
}

#myButton {
	-moz-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	-webkit-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	box-shadow:inset 0px 1px 0px 0px #fbafe3;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ff5bb0), color-stop(1, #ef027d));
	background:-moz-linear-gradient(top, #ff5bb0 5%, #ef027d 100%);
	background:-webkit-linear-gradient(top, #ff5bb0 5%, #ef027d 100%);
	background:-o-linear-gradient(top, #ff5bb0 5%, #ef027d 100%);
	background:-ms-linear-gradient(top, #ff5bb0 5%, #ef027d 100%);
	background:linear-gradient(to bottom, #ff5bb0 5%, #ef027d 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff5bb0', endColorstr='#ef027d',GradientType=0);
	background-color:#ff5bb0;
	-moz-border-radius:25px;
	-webkit-border-radius:25px;
	border-radius:25px;
	border:1px solid #e329b1;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:21px;
	font-weight:bold;
	padding:15px 26px;
  margin-right: 20px;
	text-decoration:none;
	text-shadow:0px 3px 45px #c70067;
}
#myButton:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ef027d), color-stop(1, #ff5bb0));
	background:-moz-linear-gradient(top, #ef027d 5%, #ff5bb0 100%);
	background:-webkit-linear-gradient(top, #ef027d 5%, #ff5bb0 100%);
	background:-o-linear-gradient(top, #ef027d 5%, #ff5bb0 100%);
	background:-ms-linear-gradient(top, #ef027d 5%, #ff5bb0 100%);
	background:linear-gradient(to bottom, #ef027d 5%, #ff5bb0 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ef027d', endColorstr='#ff5bb0',GradientType=0);
	background-color:#ef027d;
}
#myButton:active {
	position:relative;
	top:1px;
}

#registerPrompt {
-moz-box-shadow:inset 0px 1px 0px 0px #8a8087;
	-webkit-box-shadow:inset 0px 1px 0px 0px #8a8087;
	box-shadow:inset 0px 1px 0px 0px #8a8087;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #9e8090), color-stop(1, #241b20));
	background:-moz-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-webkit-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-o-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-ms-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:linear-gradient(to bottom, #9e8090 5%, #241b20 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#9e8090', endColorstr='#241b20',GradientType=0);
	background-color:#9e8090;
	-moz-border-radius:25px;
	-webkit-border-radius:25px;
	border-radius:25px;
	border:1px solid #12010d;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:12px;
	font-weight:bold;
	padding:21px 26px;
	text-decoration:none;
	text-shadow:0px 3px 45px #030002;
}
#registerPrompt {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #241b20), color-stop(1, #9e8090));
	background:-moz-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-webkit-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-o-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-ms-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:linear-gradient(to bottom, #241b20 5%, #9e8090 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#241b20', endColorstr='#9e8090',GradientType=0);
	background-color:#241b20;
}
#registerPrompt {
	position:relative;
	top:1px;
}
</style>

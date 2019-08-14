<template>
  <div id="register" class="text-center">
    <nav-header></nav-header>

      <form class="form-register" @submit.prevent="register">
       
        <div class="allInputFields" style="position: relative;">
          <h1 class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Create Account</strong></h1>
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
    <button type="Submit" id="regButton" class="btn btn-success btn-lg btn-block">Register</button>
      
   <br>
  
      <router-link :to="{ name: 'login' }" tag="button" class="btn btn-primary btn-lg btn-block" id="haveAccount">Have an account?</router-link>
      <br>
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
#register{
  margin-top: 500px;
  position: relative;
  z-index: 1;
}

.form-register{
  margin-right: 25vw;
  margin-left: 25vw;
  margin-top: -500px;
}

.form-control{
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

/* #regButton {
	-moz-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	-webkit-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	box-shadow:inset 0px 1px 0px 0px #fbafe3;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ff4c49), color-stop(1, #ef027d));
	background:-moz-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-webkit-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-o-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-ms-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:linear-gradient(to bottom, #ff5bb0 5%, #ff4c49 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff5bb0', endColorstr='#ef027d',GradientType=0);
	background-color: #ff4c49;
	-moz-border-radius:27px;
	-webkit-border-radius:27px;
	border-radius:27px;
	border:1px solid #ee1eb5;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:22px;
	font-weight:bold;
	padding:9px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #c70067;
  display: block;
  width: 100%;
  
}
#regButton:hover {
	background-color:#ff4c49;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ff4c49), color-stop(1, #ff5bb0));
	background:-moz-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-webkit-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-o-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-ms-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:linear-gradient(to bottom, #ff4c49 5%, #ff5bb0 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff4c49', endColorstr='#ff5bb0',GradientType=0);
	background-color:#ff4c49;
}
#regButton:active {
	
	top:1px;
}

#haveAccount {
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
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:12px;
	font-weight:bold;
	padding:9px 14px;
	text-decoration:none;
	text-shadow:0px 3px 45px #030002;
  display: block;
}
#haveAccount:hover {
background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #241b20), color-stop(1, #9e8090));
	background:-moz-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-webkit-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-o-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-ms-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:linear-gradient(to bottom, #241b20 5%, #9e8090 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#241b20', endColorstr='#9e8090',GradientType=0);
	background-color:#241b20;
}
#haveAccount:active {

	top:1px;
} */

</style>

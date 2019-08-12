<template>


 

<b-card-group deck>
  <div id="landing-page">
      <div class="djAbout">
       
          <b-card-text>Lorem ipsum dolor sit amet, mel mucius consetetur ut, 
              ad novum possit sea. Usu ex voluptatibus reprehendunt, mazim prodesset nec no. 
              Et tempor utroque constituam his, in mel erant ludus. Vidit percipit nam ea, 
              cum minim cotidieque ex, mei alterum nominavi postulant no. Ea vis nostro 
              apeirian evertitur, cu nibh brute nulla mea, quem putant similique eos ut. 
          </b-card-text>
      
      </div>

      <div class="loginAndRegister">
        
          
            With supporting text below as a natural lead-in to additional content.
         
        <div class="loginButton">
          <router-link to="/login" tag="button" class="btn btn-lg btn-primary" id="newLogIn">Log In</router-link>
        </div>
        <div class="registerButton">
          <router-link to="/register" tag="button" class="btn btn-lg btn-primary" id="newRegister">Register</router-link>
        </div>
       
      </div>
      </div>
</b-card-group>


  
</template>

<script>
export default {
    name: 'landing-page'
}
</script>

<style>
.djAbout {
  
  font-family: "Roboto Condensed", sans-serif;
  border-radius: 10px;
  float: left;
  padding: 25px;
  max-width: 28%;
  margin-left: 17%;
  margin-right: 10%;
  margin-top: 7%;
   max-height: 100%;
  background-color: hsla(0, 0%, 0%, 0.3);
  color: white;
}

.loginAndRegister {
  font-family: "Roboto Condensed", sans-serif;
  border-radius: 10px;
  float: right;
  padding: 25px;
  max-width: 28%;
  margin-top: 10%;
  margin-right: 17%;
  max-height: 100%;
  background-color: hsla(0, 0%, 0%, 0.3);
  color: white;
}

.buttons  {
   float: right;

   }

.registerButton {
    padding: 20px;
    
}

#landing-page {
  margin-top: 5%;
}

body {
   
    background-image: url('~@/assets/Soundwave.jpg') !important;
    background-position: center center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    width: auto;
    height: auto; 
}


#newLogIn {
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
#newLogIn:hover {
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
#newLogIn:active {
	
	top:1px;
}

#newRegister {
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
#newRegister:hover {
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
#newRegister:active {

	top:1px;
}
</style>

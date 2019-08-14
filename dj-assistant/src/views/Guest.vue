<template>
    <div class="guestPage">
        <nav-header></nav-header>
        <h1 id="guestPageTitle" class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Guest Page</strong></h1>
        <h3 class="h1-reponsive wow fadeInDown" data-wow-delay="0.3s">Please enter your party name:</h3>
        <div class="partyName">
        <input
        type="text"
        id="partyName"
        class="form-control"
        placeholder="Party Name"
        v-model="partyName"
        required
        autofocus
      />
      <br />
      <router-link id="findParty" :to="{ name: 'guest-party', params: { partyName: partyName } }" tag="button" class="btn btn-danger btn-lg btn-block">Find Party</router-link>
      </div>
       
    </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";

export default {
    name: "guest",
    components: {
        NavHeader
    },
    data() {
    return {
      partyName: '',
    };
  },
}
</script>

<style>

.guestPage {
    margin-top: 75px;
}
.partyName {
      margin: 20px;
   text-align: center;
   position: relative;
     margin-right: 25vw;
  margin-left: 25vw;
}

/* #findParty {
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
  width: 47%;
  text-align: center;
   margin-right: 25vw;
  margin-left: 26vw;
  
}
#findParty:hover {
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
#findParty:active {
	
	top:1px;
} */

h3 {
   color: white;
  text-shadow: 4px 4px 5px #ff4c49, 0 0 25px salmon, 0 0 5px pink;
}



</style>

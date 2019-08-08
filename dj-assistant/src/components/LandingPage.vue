<template>
<div id="landing-page">
    <div class="djAbout">
   
    <h1>About DJ App </h1>
        <p class="aboutTheDjApp">Lorem ipsum dolor sit amet, mel mucius consetetur ut, 
            ad novum possit sea. Usu ex voluptatibus reprehendunt, mazim prodesset nec no. 
            Et tempor utroque constituam his, in mel erant ludus. Vidit percipit nam ea, 
            cum minim cotidieque ex, mei alterum nominavi postulant no. Ea vis nostro 
            apeirian evertitur, cu nibh brute nulla mea, quem putant similique eos ut. </p>
    </div>

    <div class="buttons">
        <div class="loginButton">
          <router-link to="/login" tag="button" class=" w-40 btn  btn-primary">Log In</router-link>
        </div>
        <div class="registerButton">
          <router-link to="/register" tag="button" class="btn btn-lg btn-primary">Register</router-link>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: 'landing-page'
}
</script>

<style>
.djAbout {
  
  font-family: "Roboto Condensed", sans-serif;
  border-radius: 70px;
  float: left;
  border: 5px solid rgb(0, 110, 255);
  padding: 25px;
  max-width: 30%;

}


.buttons  {
   float: right;
   padding: 15px;
   width: 85%;
   margin-left: -50%;
   margin-top: 15em;
   margin-right: 8em;
   
   }

.registerButton {
    padding: 20px;
    
}


body {
   
    background-color: purple;
    background-image: "url('Soundwave.jpg')";
    
   /* background-position: center center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    width: 100%;
    height: auto; */
}

</style>

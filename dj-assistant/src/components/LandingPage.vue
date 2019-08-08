<template>


 

<b-card-group deck>
  <div id="landing-page">
      <div class="djAbout">
        <b-card bg-variant="dark" text-variant="white" header="About DJ App" class="text-center">
          <b-card-text>Lorem ipsum dolor sit amet, mel mucius consetetur ut, 
              ad novum possit sea. Usu ex voluptatibus reprehendunt, mazim prodesset nec no. 
              Et tempor utroque constituam his, in mel erant ludus. Vidit percipit nam ea, 
              cum minim cotidieque ex, mei alterum nominavi postulant no. Ea vis nostro 
              apeirian evertitur, cu nibh brute nulla mea, quem putant similique eos ut. 
          </b-card-text>
        </b-card>
      </div>

      <div class="loginAndRegister">
        <b-card bg-variant="dark" text-variant="white" title="Card Title">
          <b-card-text>
            With supporting text below as a natural lead-in to additional content.
          </b-card-text>
        <div class="loginButton">
          <router-link to="/login" tag="button" class="btn btn-lg btn-primary">Log In</router-link>
        </div>
        <div class="registerButton">
          <router-link to="/register" tag="button" class="btn btn-lg btn-primary">Register</router-link>
        </div>
        </b-card>
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
  border-radius: 70px;
  float: left;
  padding: 25px;
  max-width: 30%;
  margin-left: 20%;
  max-height: 40%;

}

.loginAndRegister {
  font-family: "Roboto Condensed", sans-serif;
  border-radius: 70px;
  float: right;
  padding: 25px;
  max-width: 30%;
  margin-right: 20%;
  max-height: 40%;

}

.buttons  {
   float: right;

   }

.registerButton {
    padding: 20px;
    
}


body {
   
    background-image: url('~@/assets/Soundwave.jpg') !important;
    background-position: center center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    width: 100%;
    height: auto; 
}

#app {
  vuetify: new Vuetify(),
}

</style>

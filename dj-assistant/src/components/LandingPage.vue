<template>
<div class="landing-page">
    <aside class="djAbout">
    <div>
    <h1>About DJ App </h1>
        <p class="aboutTheDjApp">Lorem ipsum dolor sit amet, mel mucius consetetur ut, 
            ad novum possit sea. Usu ex voluptatibus reprehendunt, mazim prodesset nec no. 
            Et tempor utroque constituam his, in mel erant ludus. Vidit percipit nam ea, 
            cum minim cotidieque ex, mei alterum nominavi postulant no. Ea vis nostro 
            apeirian evertitur, cu nibh brute nulla mea, quem putant similique eos ut. </p>
    </div>
    </aside>
    <div>
        <aside class="buttons">
          <router-link to="/Login" tag="button" class="btn btn-lg btn-primary">Log In</router-link>
          <router-link to="/Register" tag="button" class="btn btn-lg btn-primary">Register</router-link>
          </aside>
    </div>
    </div>
</template>

<script>
export default {
    name: 'landing-page'
}
</script>

<style>
.landing-page {
  width: 800px;
  background: #fff;
  margin: 100px auto;
  font-family: "Roboto Condensed", sans-serif;
  border-radius: 70px;
}

.djAbout {
    float: left;
    border: 1px black solid;
    padding: 5px;
    width: 50%;
}

.buttons  {
   float: right;
    border: 1px black solid;
    padding: 5px;
}
</style>

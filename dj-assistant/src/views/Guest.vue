<template>
    <div class="guestPage">
        <nav-header></nav-header>
        <h1 id="guestPageTitle" class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Guest Page</strong></h1>
        <div
        class="alert alert-danger song"
        role="alert"
        v-if="this.$route.query.partyFailed"
      >Party not found.</div>
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

h3 {
   color: white;
  text-shadow: 4px 4px 5px #ff4c49, 0 0 25px salmon, 0 0 5px pink;
}



</style>

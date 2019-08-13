<template>
    <div class="guest">
        <nav-header></nav-header>
        <h1>Guest Page</h1>
        <h3>Search for a party</h3>
        <input
        type="text"
        id="partyName"
        class="form-control"
        placeholder="Party Name"
        v-model="partyName"
        required
        autofocus
      />
       <router-link :to="{ name: 'guest-party', params: { partyName: partyName } }" tag="button" class="btn btn-lg btn-warning">Find Party</router-link>
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
      partyName: ''
    };
  },
}
</script>

<style>
.guest {
 background-image: url("~@/assets/guest_background.png") !important;
    background-position: center center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  width: auto;
  height: auto;
}



</style>

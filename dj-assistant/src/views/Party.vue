<template>
    <div>
        <div>
            <nav-header></nav-header>
        <h1>{{party.name}}</h1>
        <p>{{party.description}}</p>
        </div>
    </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
//const apiService = new APIService();
import auth from "../auth";

export default {
  name: "guestParty",
  data() {
    return {
      party: {}
    };
  },
  components: {
    NavHeader
  },
  methods: {},
  created() {
      const id = this.$route.params.id
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/party/${id}`, {
      method: "GET",
      headers: {
        // A Header with our authentication token.
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => response.json())
      .then(json => {
        this.party = json;
      });
  }
};
</script>

<style>

</style>

<template>
  <div id="dashboard" class="text-center">
    <nav-header></nav-header>

    <div id="dash-main" class="container">
      <div class="row">
        <h1
          class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
          data-wow-delay="0.3s"
        >
          <strong>THIS IS THE DASHBOARD YA'LL</strong>
        </h1>
        <h2>Party List</h2>
        <div v-for="party in parties" :key="party.id">{{party.name}} hello</div>
      </div>

      <aside class="buttons">
        <div class="addSong">
          <router-link to="/add-song" tag="button" class="btn btn-lg btn-warning">Add Song</router-link>
        </div>
        <div class="createParty">
          <router-link to="/create-party" tag="button" class="btn btn-lg btn-warning">Create Party</router-link>
        </div>
      </aside>
    </div>
  </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
//const apiService = new APIService();
import auth from "../auth";

export default {
  name: "dashboard",
  data() {
    return {
      parties: []
    };
  },
  components: {
    NavHeader
  },
  methods: {},
  created() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/party`, {
      method: "GET",
      headers: {
        // A Header with our authentication token.
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => response.json())
      .then(json => {
        this.parties = json;
      });
  }
};
</script>

<style>
#dash-main {
  margin-top: 100px;
}

/* .buttons  {
   float: right;
   padding: 15px;
   width: 55%;
   margin-left: -50%;
   margin-top: 15em;
   margin-right: 8em;
   } */

.createParty {
  padding: 20px;
}

.dashboardPage {
  height: 100%;
}

.registerBackground {
  background-image: url("/assets/Soundwave.jpg");
  background-position: center center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  width: 100%;
  height: auto;
}
</style>

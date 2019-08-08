<template>
  <div>
    <nav-header></nav-header>
    <div id="dashboard">
      <div id="dash-main" class="container">
        <h1 id="display-name" data-wow-delay="0.3s">
          <strong>Display Name</strong>
        </h1>
      </div>

      <div id="party-list">
        <h2>Party List</h2>
        <ul>
          <li v-for="party in parties" :key="party.id">{{party.name}}</li>
        </ul>
      </div>

      <div class="buttons">
        <div class="addSong">
          <router-link to="/add-song" tag="button" class="btn btn-lg btn-warning">Add Song</router-link>
        </div>

        <div class="createParty">
          <router-link to="/create-party" tag="button" class="btn btn-lg btn-warning">Create Party</router-link>
        </div>
      </div>
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

.registerBackground {
  background-image: url("~@/assets/dashboard.png") !important;
  background-position: center center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  width: 100%;
  height: auto;
}

#dash-main {
  grid-area: dash-main;
  display: flex;
  flex-direction: row;
  flex-flow: center;
  justify-content: center;
}

.party-list {
  display: flex;
  flex-direction: column;
  align-content: flex-start;
  grid-area: party-list;
}

#dashboard {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "dash-main dash-main"
    "party-list buttons"
    "party-list buttons";
}

.buttons {
  display: flex;
  flex-direction: column;
  align-content: flex-start;
  grid-area: buttons;
}

/* .createParty {
  padding: 20px;
} */
</style>

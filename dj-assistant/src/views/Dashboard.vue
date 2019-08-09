<template>
  <div>
    <nav-header></nav-header>
    <div id="dashboard">
      <div id="dash-main" class="container">
        <h1 id="display-name" data-wow-delay="0.3s">
          <strong>DJ Dashboard</strong>
        </h1>
      </div>

      <div id="party-list">
        <b-card bg-variant="dark" text-variant="white" title="Party List" class="text-center">
          <b-card-text>
              <ul>
                <li v-for="party in parties" :key="party.id">{{party.name}}</li>
              </ul>
          </b-card-text>
        </b-card>

      </div>

        <b-card bg-variant="dark" text-variant="white" >
            <div class="buttons">
              <div class="addSong">
                <router-link to="/add-song" tag="button" class="btn btn-lg btn-warning">Add Song</router-link>
              </div>

              <div class="createParty">
                <router-link to="/create-party" tag="button" class="btn btn-lg btn-warning">Create Party</router-link>
              </div>
            </div>
        </b-card>

      <!-- <div class="buttons">
        <div class="addSong">
          <router-link to="/add-song" tag="button" class="btn btn-lg btn-warning">Add Song</router-link>
        </div>

        <div class="createParty">
          <router-link to="/create-party" tag="button" class="btn btn-lg btn-warning">Create Party</router-link>
        </div>
      </div> -->
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
  align-items: center;
  grid-area: party-list;
}

#dashboard {
  display: grid;
  /* grid-gap: ; */

  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "dash-main dash-main"
    "party-list buttons"
    "party-list buttons"
    "party-list buttons";
}

.buttons {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  grid-area: buttons;
}

/* .createParty {
  padding: 20px;
} */
</style>

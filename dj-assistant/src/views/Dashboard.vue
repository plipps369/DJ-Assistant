<template>
  <div>
    <nav-header></nav-header>
    <div id="dashboard">
      <div id="dash-main">
        <h1 id="display-name">
          <strong>DJ Dashboard</strong>
        </h1>
      </div>

      <div>
        <b-card id="party-list" bg-variant="primary" text-variant="white" title="Party List" class="text-center">
              <router-link v-for="party in parties" :key="party.id" :to="{ name: 'party', params: { id: party.id } }" tag="button" class="btn btn-lg btn-block btn-light">{{party.name}}</router-link>
        </b-card>
      </div>

      <div>
        <b-card id="add-song" bg-variant="primary">
            <div>
              <router-link to="/add-song" tag="button" class="btn btn-lg btn-block btn-success">Add Song</router-link>
            </div>
        </b-card>

        <b-card id="create-party" bg-variant="primary">
            <div>
              <router-link to="/create-party" tag="button" class="btn btn-lg btn-block btn-danger">Create Party</router-link>
            </div>
        </b-card>
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

#dashboard {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "dash-main dash-main"
    "party-list buttons"
    "party-list buttons"
    "party-list buttons";
}

#dash-main {
  grid-area: dash-main;
  display: flex;
  flex-direction: row;
  flex-flow: center;
  justify-content: center;
}

#party-list {
  margin: 3vh;
}

#party-list > div {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
  grid-area: party-list;
}

#party-list > div > button {
  margin: 3vh;
  margin-left: 5vw;
  margin-right: 5vw;
}

#add-song {
  padding: 7vh;
  margin: 3vh;
}

#create-party {
  padding: 7vh;
  margin: 3vh;
}
</style>

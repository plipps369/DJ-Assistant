<template>
  <div>
    <nav-header></nav-header>
    <div id="dashboard">
      <div id="dash-main">
        <h1 id="display-name">
          <strong>{{user.displayName}}</strong>
        </h1>
        <br />
        <div
          class="alert alert-success"
          role="alert"
          v-if="this.$route.query.partyCreated"
        >Party successfully created.</div>
        <div></div>
      </div>

      <div id="buttons" class="container-fluid">
        <div class="row">
          <div class="col-sm-3"></div>
          <!-- <b-card id="add-song" bg-variant="primary"> -->
          <div class="col-sm-6">
            <router-link
              to="/add-song"
              id="button"
              tag="button"
              class="btn btn-md btn-danger"
            >Add Song</router-link>

            <!-- </b-card> -->

            <!-- <b-card id="create-party" bg-variant="primary"> -->

            <router-link
              to="/create-party"
              id="button"
              tag="button"
              class="btn btn-md btn-danger"
            >Create Party</router-link>

            <!-- </b-card> -->
            <!-- <b-card id="library" bg-variant="primary"> -->

            <router-link
              to="/library"
              id="button"
              tag="button"
              class="btn btn-md btn-danger"
            >View Library</router-link>
          </div>
          <div class="col-sm-3"></div>
        </div>
        <!-- </b-card> -->
      </div>

      <!-- <b-card
        id="party-list"
        bg-variant="primary"
        text-variant="white"
        title="Party List"
        class="text-center"
      >-->
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-3"></div>
          <div class="col-sm-6">
            <router-link
              v-for="party in parties"
              :key="party.id"
              :to="{ name: 'party', params: { id: party.id } }"
              tag="button"
              class="btn btn-lg btn-block btn-light"
            >
              <b>{{party.name}}:</b>
              {{party.description}}
            </router-link>
          </div>
          <div class="col-sm-3"></div>
        </div>
      </div>
      <!-- </b-card> -->
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
      user: {},
      parties: []
    };
  },
  components: {
    NavHeader
  },
  methods: {
    getUser() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ`, {
        method: "GET",
        headers: {
          // A Header with our authentication token.
          Authorization: "Bearer " + auth.getToken()
        }
      })
        .then(response => response.json())
        .then(json => {
          this.user = json;
        });
    },
    getParties() {
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
  },
  created() {
    this.getUser();
    this.getParties();
  }
};
</script>

<style>
#button {
  margin-right: 2px;
  margin-bottom: 5%;
  padding: 1%;
}
</style>

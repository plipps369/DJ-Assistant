<template>
  <div>
    <nav-header></nav-header>
    <div id="dash-main" class="container">
      <div class="row">
        <div class="col-lg-8">
      <h1>THIS IS THE DASHBOARD YA'LL</h1>
      <ul>
        <!-- <li v-for="party in parties" :key="party.id">{{party.name}}</li> -->
      </ul>
      </div>
      <aside class="col-lg-4">
        <router-link to="/add-song" tag="button" class="btn btn-lg btn-warning">Add Song</router-link>
      </aside>
      </div>
    </div>
  </div>
</template>

<script>
import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
const apiService = new APIService();

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
  methods: {
    getPartiesByDJid(DJid) {
      apiService
        .getParties(DJid)
        .then(json => {
          this.parties = json;
        })
        .catch(error => {
          window.console.log("Error:", error);
        });
    }
  }
};
</script>

<style>
#dash-main {
  margin-top: 15vh;

}
</style>

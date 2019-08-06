<template>
  <div>
    <nav-header></nav-header>
    <div id="dash-main">
      <h1>THIS IS THE DASHBOARD YA'LL</h1>
      <ul>
        <!-- <li v-for="party in parties" :key="party.id">{{party.name}}</li> -->
      </ul>
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

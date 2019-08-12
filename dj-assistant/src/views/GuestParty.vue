<template>
    <div>
        <div>
            <nav-header></nav-header>
            <h1>Songlist</h1>
            <p v-for="song in songs" :key="song.id">{{song.title}} by: {{song.artist}}</p>
        </div>
    </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
//const apiService = new APIService();
//import auth from "../auth";

export default {
  name: "party",
  data() {
    return {
      songs: []
    };
  },
  components: {
    NavHeader
  },
  methods: {},
  created() {
      const partyName = this.$route.params.partyName
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/guest/${partyName}`, {
      method: "GET",
      headers: {
        Accept: "application/json",
          "Content-Type": "application/json"
      }
    })
      .then(response => response.json())
      .then(json => {
        this.songs = json;
      });
  }
};
</script>

<style>

</style>
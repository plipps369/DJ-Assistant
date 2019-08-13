<template>
    <div>
        <div>
            <nav-header></nav-header>
        <h1>{{party.name}}</h1>
        <p>{{party.description}}</p>
        <ul>
          <li v-for="song in songs" :key="song.id">{{song.title}} by {{song.artist}}</li>
        </ul>
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
      party: {},
      songs: []
    };
  },
  components: {
    NavHeader
  },
  methods: {
    getParty() {
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
  
    },
    getSongs() {
      const partyId = this.$route.params.id
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/party/partysongs/${partyId}`, {
      method: "GET",
      headers: {
        // A Header with our authentication token.
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => response.json())
      .then(json => {
        this.songs = json;
      });
    }
  },
  created() {
      this.getParty();
      this.getSongs();
  }
};
</script>

<style>

</style>

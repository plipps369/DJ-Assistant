<template>
    <div>
        <div>
            <nav-header></nav-header>
        <h1>{{party.name}}</h1>
        <p>{{party.description}}</p>
        <ul>
          <div v-for="song in songs" :key="song.id" v-bind:class="{ 'played': song.played == true }">{{song.title}} by {{song.artist}}
             <button v-on:click="markSongPlayed(song.id)">Mark as Played</button>
          </div>
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
      songs: [],
      markErrors: false,
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
    },
    markPlayedLocally(partySongId) {
      this.songs.forEach(song => {
        if(song.id == partySongId){
          song.played = true;
        }
      });
    },
    markSongPlayed(partySongId) {
      this.markPlayedLocally(partySongId);
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/party/MarkedAsPlayed/${partySongId}`, {
        method: "POST",
        headers: {
          Authorization: "Bearer " + auth.getToken(),
           "Content-Type": "application/json"
        },
        //body: JSON.stringify(this.partySongId)
      })
        .then(response => {
          if (response.ok) {
            return response.text();
            
          } else {
            this.markErrors = true;
          }
        })

        .catch(err => console.error(err));
    
    }
  },
  created() {
      this.getParty();
      this.getSongs();
  }
};
</script>

<style>
.played {
  background-color: black;
}
</style>

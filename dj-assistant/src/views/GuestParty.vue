<template>
  <div>
    <div>
      <nav-header></nav-header>
      <h1>{{songRequest.partyName}}</h1>
      <ul>Next 5 Songs:
        <li v-for="song in next5Songs" :key="song.id">{{song.title}} by {{song.artist}}</li>
        </ul>
        <ul>Last 5 Songs:
        <li v-for="song in last5Songs" :key="song.id">{{song.title}} by {{song.artist}}</li>
        </ul>
      <h3>Request a Song</h3>
      <form @submit.prevent="requestSong">
          <div class="form-group">
        <select class="form-control" v-model="songRequest.songId">
          Song Choices:
          <option v-for="song in songs" :key="song.id" :value="song.id">{{song.title}} by: {{song.artist}}</option>
        </select>
        </div>
        <button type="Submit" class="btn btn-outline-warning" id="songSubmit">Submit Song</button>
      </form>
    </div>
  </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
//const apiService = new APIService();

export default {
  name: "guest-party",
  data() {
    return {
      songs: [],
      next5Songs: [],
      last5Songs: [],
      songRequest: {
        songId: null,
        partyName: this.$route.params.partyName
      },
      requestSongErrors: false
    };
  },
  components: {
    NavHeader
  },
  methods: {
      requestSong() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/guest`, {
        method: "POST",
        headers: {
          Accept: "application/json",
        "Content-Type": "application/json"
        },
        body: JSON.stringify(this.songRequest)
      })
        .then(response => {
          if (response.ok) {
            
            this.$router.push({
              path: "/",
            });
          } else {
            this.requestSongErrors = true;
          }
        })

        .catch(err => console.error(err));
    },
    getSongsForRequest() {
    const partyName = this.$route.params.partyName;
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
    },
    getNext5Songs() {
const partyName = this.$route.params.partyName;
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/guest/nextFive/${partyName}`, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    })
      .then(response => response.json())
      .then(json => {
        this.next5Songs = json;
      });
    },
    getLast5Songs() {
const partyName = this.$route.params.partyName;
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/guest/lastFive/${partyName}`, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    })
      .then(response => response.json())
      .then(json => {
        this.last5Songs = json;
      });
    },
  },
  created() {
    this.getSongsForRequest();
    this.getNext5Songs();
    this.getLast5Songs();
  }
};
</script>

<style>
</style>
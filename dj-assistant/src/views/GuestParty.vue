<template>
  <div>
    <div>
      <nav-header></nav-header>
      <h1>{{songRequest.partyName}}</h1>
      <div
        class="alert alert-success song"
        role="alert"
        v-if="this.$route.query.songRequested"
      >Song requested.</div>
      <div class="song">
        <h3>Next 5 Songs:</h3>
        <p v-for="song in next5Songs" :key="song.id">{{song.title}} by {{song.artist}}</p>
      </div>

      <div class="song">
        <h3>Last 5 Songs:</h3>
        <p v-for="song in last5Songs" :key="song.id">{{song.title}} by {{song.artist}}</p>
      </div>
    </div>
    <h3>Request a Song</h3>
    <form @submit.prevent="requestSong" id="form">
      <div class="form-group">
        <select class="form-control" v-model="songRequest.songId">
          Song Choices:
          <option
            v-for="song in songs"
            :key="song.id"
            :value="song.id"
          >{{song.title}} by: {{song.artist}}</option>
        </select>
      </div>
      <button type="Submit" class="btn btn-danger btn-lg btn-block" id="songSubmit">Submit Song</button>
    </form>
  </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
import { clearInterval } from "timers";
//const apiService = new APIService();

export default {
  name: "guest-party",
  data() {
    return {
      songs: [],
      next5Songs: [],
      last5Songs: [],
      timer1: "",
      timer2: "",
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
              name: "guest-party",
              params: { partyName: this.$route.params.partyName },
              query: { songRequested: true }
            });
          } else {
            this.requestSongErrors = true;
          }
        })

        .catch(err => console.error(err));

      this.songRequest.songId = null;

      this.getNext5Songs();
      this.getLast5Songs();
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
        .then(response => {
          if (response.ok) {
            return response.json();
          } else {
            this.$router.push({
              path: "/guest/",
              query: { partyFailed: true }
            });
          }
        })
        .then(json => {
          this.songs = json;
        });
    },
    getNext5Songs() {
      const partyName = this.$route.params.partyName;
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/api/guest/nextFive/${partyName}`,
        {
          method: "GET",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          }
        }
      )
        .then(response => response.json())
        .then(json => {
          this.next5Songs = json;
        });
    },
    getLast5Songs() {
      const partyName = this.$route.params.partyName;
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/api/guest/lastFive/${partyName}`,
        {
          method: "GET",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          }
        }
      )
        .then(response => response.json())
        .then(json => {
          this.last5Songs = json;
        });
    }
  },
  created() {
    this.getSongsForRequest();
    this.getNext5Songs();
    this.getLast5Songs();
    this.timer1 = setInterval(this.getNext5Songs, 30000);
    this.timer2 = setInterval(this.getLast5Songs, 30000);
  },
  beforeDestroy() {
    clearInterval(this.timer1);
    clearInterval(this.timer2);
  }
};
</script>

<style>
.song {
  margin: 5vw;
  text-align: center;
  position: relative;
  margin-right: 10vw;
  margin-left: 10vw;
  background-color: white;
}
#form {
  margin: 5vw;
  text-align: center;
  position: relative;
  margin-right: 10vw;
  margin-left: 10vw;
}
</style>
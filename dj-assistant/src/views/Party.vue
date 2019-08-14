<template>
  <div>
    <div>
      <nav-header></nav-header>
      <h1
        class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
        data-wow-delay="0.3s"
      >
        <strong>Party: {{party.name}}</strong>
      </h1>
      <br />
      <p class="partyDesc">Description: {{party.description}}</p>
      <br />
      <div class="cardOfSongs">
        <div class="card" style="width: 28rem;">
        <ul class="list-group list-group-flush">
          <div class="list-group-item" id="songDetail" v-for="song in songs" :key="song.id" v-bind:class="{ 'played': song.played == true }">{{song.title}} by {{song.artist}}
             <button id="playedButton" class="btn btn-danger btn-lg" v-on:click="markSongPlayed(song.id)">Played</button>
          </div>
        </ul>
        </div>
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
  name: "guestParty",
  computed: {},
  data() {
    return {
      party: {},
      songs: [],
      markErrors: false,
      timer: ""
    };
  },
  components: {
    NavHeader
  },
  methods: {
    getParty() {
      const id = this.$route.params.id;
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
      const partyId = this.$route.params.id;
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/api/party/partysongs/${partyId}`,
        {
          method: "GET",
          headers: {
            // A Header with our authentication token.
            Authorization: "Bearer " + auth.getToken()
          }
        }
      )
        .then(response => response.json())
        .then(json => {
          this.songs = json;
        });
    },
    markPlayedLocally(partySongId) {
      this.songs.forEach(song => {
        if (song.id == partySongId) {
          song.played = true;
        }
      });
    },
    markSongPlayed(partySongId) {
      this.markPlayedLocally(partySongId);
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/api/party/MarkedAsPlayed/${partySongId}`,
        {
          method: "POST",
          headers: {
            Authorization: "Bearer " + auth.getToken(),
            "Content-Type": "application/json"
          }
          //body: JSON.stringify(this.partySongId)
        }
      )
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
    this.timer = setInterval(this.getSongs, 30000);
  },
  beforeDestroy() {
    clearInterval(this.timer);
  }
};
</script>

<style>
.played {
  text-decoration: line-through;
}

.partyDesc {
  color: white;
}

#songDetail {
  color: black;
}

.cardOfSongs {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  max-width: 100%;
  float: center;
}

#playedButton {
  float: right;
}
  

</style>

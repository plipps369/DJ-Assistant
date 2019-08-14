<template>
    <div>
        <div>
            <nav-header></nav-header>
        <h1 class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Party: {{party.name}}</strong></h1>
        <br />
        <p class="partyDesc">Description: {{party.description}}</p>
        <br/>
        <div class="cardOfSongs">
        <div class="card" style="width: 28rem;">
        <ul class="list-group list-group-flush">
          <div class="list-group-item" id="songDetail" v-for="song in songs" :key="song.id" v-bind:class="{ 'played': song.played == true }">{{song.title}} by {{song.artist}}
             <button id="playedButton" v-on:click="markSongPlayed(song.id)">Played</button>
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
  -moz-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	-webkit-box-shadow:inset 0px 1px 0px 0px #fbafe3;
	box-shadow:inset 0px 1px 0px 0px #fbafe3;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ff4c49), color-stop(1, #ef027d));
	background:-moz-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-webkit-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-o-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:-ms-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
	background:linear-gradient(to bottom, #ff5bb0 5%, #ff4c49 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff5bb0', endColorstr='#ef027d',GradientType=0);
	background-color: #ff4c49;
	-moz-border-radius:27px;
	-webkit-border-radius:27px;
	border-radius:2px;
	border:1px solid #ee1eb5;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:10px;
	font-weight:bold;
	padding:9px 12px;
	text-decoration:none;
	text-shadow:0px 1px 0px #c70067;
  display: block;

  
}
#playedButton:hover {
	background-color:#ff4c49;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ff4c49), color-stop(1, #ff5bb0));
	background:-moz-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-webkit-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-o-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:-ms-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
	background:linear-gradient(to bottom, #ff4c49 5%, #ff5bb0 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff4c49', endColorstr='#ff5bb0',GradientType=0);
	background-color:#ff4c49;
}
#playedButton:active {
	
	top:1px;
}

</style>

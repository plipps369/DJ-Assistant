<template>
  <div id="create-main" class="text-center">
    <nav-header></nav-header>
    <form class="form-register" @submit.prevent="newParty">
      <h1 class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>New Party</strong></h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="newPartyErrors"
      >There were problems creating this party</div>
      
      <div class="inputText">
      <label for="name" class="sr-only">Name</label>
      <input
        type="text"
        id="name"
        class="form-control"
        placeholder="Name"
        v-model="party.name"
        required
        autofocus
      />
      </div>
      <div class="inputText">
      <label for="description" class="sr-only">Description</label>
      <input
        type="text"
        id="description"
        class="form-control"
        placeholder="Description"
        v-model="party.description"
      />
  </div>
  <div class="form-group">
             <select class="form-control" v-model="party.GenresId" multiple>Genre(s)
        <option v-for="genre in genres" :key="genre.id" :value="genre.id">{{genre.name}}</option>
      </select> 
</div>
      <br />
            <button type="Submit" class="btn btn-danger btn-lg btn-block" id="partySubmit">Create Party</button>
<router-link to="/dashboard">
      <button type="Cancel" class="btn btn-dark btn-lg btn-block" id="cancelPartyCreate">Cancel Add</button>
</router-link>
    </form>
  </div>
</template>

<script>
//import { APIService } from "@/APIService";
import NavHeader from "@/components/NavHeader.vue";
//const apiService = new APIService();
import auth from "../auth";

export default {
  name: "create-party",
  components: {
    NavHeader
  },
  data() {
    return {
      genres: [],
      party: {
        name: "",
        description: "",
        GenresId: []
      },
      newPartyErrors: false
    };
  },
  methods: {
    newParty() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/party`, {
        method: "POST",
        headers: {
           Authorization: "Bearer " + auth.getToken(),
           "Content-Type": "application/json"
        },
        body: JSON.stringify(this.party)
      })
        .then(response => {
          if (response.ok) {
            this.$router.push({
              path: "/dashboard",
              query: { partyCreated: true }
              });
            return response.text();
            
          } else {
            this.newPartyErrors = true;
          }
        })

        .catch(err => console.error(err));
    },
    getGenres() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/genre`, {
      method: "GET",
      headers: {
        // A Header with our authentication token.
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => response.json())
      .then(json => {
        this.genres = json;
      });
  }
  },
  created() {
    this.getGenres();
  }
};
</script>

<style>
#create-main {
  margin-top: 550px;
}

/* #partySubmit {
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
	border-radius:27px;
	border:1px solid #ee1eb5;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:22px;
	font-weight:bold;
	padding:9px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #c70067;
  display: block;
  width: 100%;
  margin-bottom: 20px;
  
}
#partySubmit:hover {
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
#partySubmit:active {
	
	top:1px;
}

#cancelPartyCreate {
	-moz-box-shadow:inset 0px 1px 0px 0px #8a8087;
	-webkit-box-shadow:inset 0px 1px 0px 0px #8a8087;
	box-shadow:inset 0px 1px 0px 0px #8a8087;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #9e8090), color-stop(1, #241b20));
	background:-moz-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-webkit-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-o-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:-ms-linear-gradient(top, #9e8090 5%, #241b20 100%);
	background:linear-gradient(to bottom, #9e8090 5%, #241b20 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#9e8090', endColorstr='#241b20',GradientType=0);
	background-color:#9e8090;
	-moz-border-radius:25px;
	-webkit-border-radius:25px;
	border-radius:25px;
	border:1px solid #12010d;
	cursor:pointer;
	color:#ffffff;
	font-family:Trebuchet MS;
	font-size:12px;
	font-weight:bold;
	padding:9px 14px;
	text-decoration:none;
	text-shadow:0px 3px 45px #030002;
  display: block;
  width: 100%;
}
#cancelPartyCreate:hover {
background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #241b20), color-stop(1, #9e8090));
	background:-moz-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-webkit-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-o-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-ms-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:linear-gradient(to bottom, #241b20 5%, #9e8090 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#241b20', endColorstr='#9e8090',GradientType=0);
	background-color:#241b20;
}
#cancelPartyCreate:active {

	top:1px;
} */
</style>

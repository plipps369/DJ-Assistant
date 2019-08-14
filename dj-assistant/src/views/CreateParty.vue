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

#partySubmit {
 
  margin-bottom: 4%;
}

</style>

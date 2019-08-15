<template>
  <div>
    <nav-header></nav-header>
    <h1
      class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
      data-wow-delay="0.3s"
    >
      <strong>LIBRARY</strong>
    </h1>
    <br />
    <router-link to="/add-song" id="button" tag="button" class="btn btn-md btn-danger">Add Song</router-link>
    <h3>All Songs:</h3>
    <div class="libraryCard">
      <div class="card" style="width: 28rem;">
        <ul class="list-group list-group-flush">
          <li v-for="song in songs" :key="song.id">{{song.title}} by {{song.artist}}</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";
import auth from "../auth";

export default {
  name: "library",
  data() {
    return {
      songs: []
    };
  },
  components: {
    NavHeader
  },
  methods: {
    getAllSongs() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ/AllSongs`, {
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
    this.getAllSongs();
  }
};
</script>

<style>
.libraryCard {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  max-width: 100%;
  float: center;
}

ul {
  list-style-type: none;
}
</style>
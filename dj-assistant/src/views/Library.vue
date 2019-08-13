<template>
    <div>
        <nav-header></nav-header>
        <h1>Library</h1>
        <h3>All Songs:</h3>
        <ul>
            <li v-for="song in songs" :key="song.id">{{song.title}} by {{song.artist}}</li>
        </ul>
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
  
    },
    },
    created() {
        this.getAllSongs();
    }
}
</script>

<style>

</style>
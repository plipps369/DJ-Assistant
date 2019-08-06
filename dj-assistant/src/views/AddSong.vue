<template>
  <div id="add-main" class="text-center">
    <nav-header></nav-header>
    <form class="form-register" @submit.prevent="addSong">
      <h1 class="h3 mb-3 font-weight-normal">New Song</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="addSongErrors"
      >There were problems adding this song</div>
      <label for="title" class="sr-only">Title</label>
      <input
        type="text"
        id="title"
        class="form-control"
        placeholder="Title"
        v-model="song.title"
        required
        autofocus
      />
      <label for="artist" class="sr-only">Artist</label>
      <input
        type="text"
        id="artist"
        class="form-control"
        placeholder="Artist"
        v-model="song.artist"
        required
      />
      <label for="length" class="sr-only">Length</label>
      <input
        type="text"
        id="length"
        class="form-control"
        placeholder="Length"
        v-model="song.length"
      />
      <label for="radio" class="sr-only">Explicit?</label>
      <input type="radio" id="true" value="true" v-model="song.explicit" />
      <label for="true">Yes</label>
      
      <input type="radio" id="false" value="false" v-model="song.explicit" />
      <label for="false">No</label>
      <br>
      <button type="Submit" class="btn btn-outline-warning">Submit Song</button>
    </form>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";

export default {
  name: "song",
  components: {
    NavHeader
  },
  data() {
    return {
      song: {
        title: "",
        artist: "",
        length: 0,
        explicit: false,
        genresId: []
      },
      addSongErrors: false
    };
  },
  methods: {
    addSong() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/addSong`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.song),
      })
        .then((response) => {
          if (response.ok) {
            return response.text();
          } else {
            this.addSongErrors = true;
          }
        })
        .then((token) => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, '');
            }
            auth.saveToken(token);
            this.$router.push('/');
          }
        })
        .catch((err) => console.error(err));
    },
  },
};
</script>

<style>
#add-main {
  margin-top: 90vh;
}
</style>

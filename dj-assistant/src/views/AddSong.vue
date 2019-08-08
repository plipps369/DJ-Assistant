<template>
  <div id="add-main" class="text-center">
    <nav-header></nav-header>
    <form class="form-register" @submit.prevent="addSong">
      <h1 class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown" data-wow-delay="0.3s"><strong>Add A New Song</strong></h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="addSongErrors"
      >There were problems adding this song</div>
      <div class="inputText">
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
      </div>
      <div class="inputText">
      <label for="artist" class="sr-only">Artist</label>
      <input
        type="text"
        id="artist"
        class="form-control"
        placeholder="Artist"
        v-model="song.artist"
        required
      />
       </div>
      <div class="inputText">
      <label for="length" >Length</label>
      <input
        type="text"
        id="length"
        class="form-control"
        placeholder="Length"
        v-model="song.length"
      />
       </div>
      
      <label for="radio">Explicit?</label>
      <br>
      <input class="form-check-input" type="radio" id="true" value="true" v-model="song.explicit" />
      <label class="form-check-label" for="true">Yes</label>
      <br>
      <input class="form-check-input" type="radio" id="false" value="false" v-model="song.explicit" />
      <label class="form-check-label" for="false">No</label>
      <br>
      <br>
      <span>Genre(s) - Hold ctrl to make multiple selections</span>
      <br>
      <select class="form-control" v-model="song.genres" multiple>Genre(s)
        <option v-for="object in genres" :key="object" :value="object.id">{{object.name}}</option>
      </select>
      <br>
      <button type="Submit" class="btn btn-outline-warning">Submit Song</button>
    </form>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";
import auth from "../auth";

export default {
  name: "add-song",
  components: {
    NavHeader
  },
  data() {
    return {
      genres: [
        {
          name: "Rock",
          id: 1
        },
        {
          name: "Pop",
          id: 2
        },
        {
          name: "Country",
          id: 3
        },
        {
          name: "Rap",
          id: 4
        }
        ],
      song: {
        title: "",
        artist: "",
        length: 0,
        explicit: false,
        genres: []
      },
      addSongErrors: false
    };
  },

  methods: {
    addSong() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/song/NewSong`, {
        method: "POST",
        headers: {
          Authorization: "Bearer " + auth.getToken(),
           "Content-Type": "application/json"
        },
        body: JSON.stringify(this.song)
      })
        .then(response => {
          if (response.ok) {
            return response.text();
          } else {
            this.addSongErrors = true;
          }
        })

        .catch(err => console.error(err));
    }
  }
};
</script>

<style>
#add-main {
  margin-top: 80vh;
}
</style>

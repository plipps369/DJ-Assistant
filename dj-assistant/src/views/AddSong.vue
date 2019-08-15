<template>
  <div id="add-main" class="text-center">
    <nav-header></nav-header>
    <form class="form-register" @submit.prevent="addSong">
      <h1
        class="h1-reponsive text-uppercase font-weight-bold mb-0 pt-md-5 pt-5 wow fadeInDown"
        data-wow-delay="0.3s"
      >
        <strong>Add A New Song</strong>
      </h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="addSongErrors"
      >There were problems adding this song</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.songAdded"
      >Song added to library successfully.</div>
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

      <div class="radioButtons">
        <label for="radio">Explicit?</label>
        <div id="trueButton">
          <input
            class="form-radio inline-block"
            type="radio"
            id="true"
            value="true"
            v-model="song.explicit"
          />
          <label class="form-check-label inline-block" for="true">Yes</label>
        </div>
        <div id="falseButton">
          <input
            class="form-radio inline-block"
            type="radio"
            id="false"
            value="false"
            v-model="song.explicit"
          />
          <label class="form-check-label inline-block" for="false">No</label>
        </div>
      </div>
      <br />

      <b-card header-bg-variant="dark" text-variant="black">
        <b-card-text>Genre(s) - hold ctrl to make multiple selections</b-card-text>
        <!-- <div class="form-group">
  <label class="typo__label">Simple select / dropdown</label>
  <multiselect class="form-control" v-model="song.GenresId" :options="genres" :multiple="true" :close-on-select="false" :clear-on-select="false" :preserve-search="true" placeholder="Pick some" label="name" track-by="name" :preselect-first="false">
    <template slot="selection" slot-scope="{ values, search, isOpen }"><span class="multiselect__single" v-if="song.GenresId.length &amp;&amp; !isOpen">{{ song.GenresId.length }} options selected</span></template>
  </multiselect>
  <pre class="language-json"><code>{{ value  }}</code></pre>
        </div>-->
        <div class="form-group">
          <select class="form-control" v-model="song.GenresId" multiple>
            Genre(s)
            <option v-for="object in genres" :key="object.id" :value="object.id">{{object.name}}</option>
          </select>
        </div>
      </b-card>
      <br />
      <button type="Submit" class="btn btn-danger btn-lg btn-block" id="songSubmit">Submit Song</button>
      <router-link to="/dashboard">
        <button type="Cancel" class="btn btn-dark btn-lg btn-block" id="cancelRequest">Cancel Add</button>
      </router-link>
    </form>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";
import auth from "../auth";
//import Multiselect from 'vue-multiselect';

export default {
  name: "add-song",
  components: {
    NavHeader
    //Multiselect
  },
  data() {
    return {
      user: {},
      genres: [],
      song: {
        title: "",
        artist: "",
        length: 0,
        explicit: false,
        GenresId: []
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
            this.$router.push({
              path: "/add-song",
              query: { songAdded: true }
            });
          } else {
            this.addSongErrors = true;
          }
        })

        .catch(err => console.error(err));
      this.song.title = "";
      this.song.artist = "";
      this.song.length = 0;
      this.song.explicit = false;
      this.song.GenresId = [];
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
#add-main {
  margin-top: 55%;
}

.form-radio {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  display: inline-block;
  position: relative;
  background-color: #f1f1f1;
  color: #666;
  top: 10px;
  height: 30px;
  width: 30px;
  border: 0;
  border-radius: 50px;
  cursor: pointer;
  margin-right: 7px;
  outline: none;
}
.form-radio:checked::before {
  position: absolute;
  font: 13px/1 "Open Sans", sans-serif;
  left: 11px;
  top: 7px;
  content: "\02143";
  transform: rotate(40deg);
}
.form-radio:hover {
  background-color: #f7f7f7;
}
.form-radio:checked {
  background-color: #f1f1f1;
}
label {
  font: 15px/1.7 "Open Sans", sans-serif;
  color: #333;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  cursor: pointer;
}

.radioButtons {
  border: 1px solid #b0c5da;
  background: white;
  border-radius: 5px;
  padding-bottom: 10px;
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  justify-self: center;
}

.trueButton {
  margin-right: 80px;
}

label {
  padding-top: 20px;
}

form {
  align-content: center;
}

#songSubmit {
  margin-bottom: 4%;
}
</style>

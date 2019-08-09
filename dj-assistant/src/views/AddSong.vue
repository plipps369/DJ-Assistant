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
      <div class="radioButtons">
        <label for="radio">Explicit?</label>
          <div id="trueButton">
            <input class="form-radio inline-block" type="radio" id="true" value="true"  v-model="song.explicit" />
            <label class="form-check-label inline-block" for="true">Yes</label>
          </div>
          <div id="falseButton">
            <input class="form-radio inline-block" type="radio" id="false" value="false" v-model="song.explicit" />
            <label class="form-check-label inline-block" for="false">No</label>
          </div>
      </div>
      <br>
      <br>
      <span>Genre(s) - Hold ctrl to make multiple selections</span>
      <br>
      <select class="form-control" v-model="song.GenresId" multiple>Genre(s)
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
            return response.text();
          } else {
            this.addSongErrors = true;
          }
        })

        .catch(err => console.error(err));
    }
  },
  created() {
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
};
</script>

<style>
#add-main {
  margin-top: 70vh;
}

.form-radio
{
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
.form-radio:checked::before
{
     position: absolute;
     font: 13px/1 'Open Sans', sans-serif;
     left: 11px;
     top: 7px;
     content: '\02143';
     transform: rotate(40deg);
}
.form-radio:hover
{
     background-color: #f7f7f7;
}
.form-radio:checked
{
     background-color: #f1f1f1;
}
label
{
     font: 15px/1.7 'Open Sans', sans-serif;
     color: #333;
     -webkit-font-smoothing: antialiased;
     -moz-osx-font-smoothing: grayscale;
     cursor: pointer;
     
} 

.radioButtons {
 
  display: flex;
  flex-direction: row;
  justify-content: space-around;
}

.trueButton {
  margin-right: 80px;
}

label {
  padding-top: 20px;
}
</style>

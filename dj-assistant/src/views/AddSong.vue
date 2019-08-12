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
      <!--<div class="inputText">
      <label for="length" >Length</label>
      <input
        type="text"
        id="length"
        class="form-control"
        placeholder="Length"
        v-model="song.length"
      />
       </div>-->
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
   

       <b-card header-bg-variant="dark" text-variant="black">
          <b-card-text>
            Genre(s) - hold ctrl to make multiple selections
          </b-card-text>
          <!-- <div class="form-group">
  <label class="typo__label">Simple select / dropdown</label>
  <multiselect class="form-control" v-model="song.GenresId" :options="genres" :multiple="true" :close-on-select="false" :clear-on-select="false" :preserve-search="true" placeholder="Pick some" label="name" track-by="name" :preselect-first="false">
    <template slot="selection" slot-scope="{ values, search, isOpen }"><span class="multiselect__single" v-if="song.GenresId.length &amp;&amp; !isOpen">{{ song.GenresId.length }} options selected</span></template>
  </multiselect>
  <pre class="language-json"><code>{{ value  }}</code></pre>
</div> -->
<div class="form-group">
             <select class="form-control" v-model="song.GenresId" multiple>Genre(s)
        <option v-for="object in genres" :key="object.id" :value="object.id">{{object.name}}</option>
      </select> 
</div>
      </b-card>
      <br>
      <button type="Submit" class="btn btn-outline-warning" id="songSubmit">Submit Song</button>
      <router-link to="/dashboard">
      <button type="Cancel" class="btn btn-outline-warning" id="cancelRequest">Cancel Add</button>
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
    NavHeader,
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
            // return response.text();
            this.$router.push({
              path: "/dashboard",
            });
          } else {
            this.addSongErrors = true;
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
#add-main {
  margin-top: 465px;
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
#songSubmit:hover {
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
#songSubmit:active {
	
	top:1px;
}

#cancelRequest {
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
#cancelRequest:hover {
background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #241b20), color-stop(1, #9e8090));
	background:-moz-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-webkit-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-o-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:-ms-linear-gradient(top, #241b20 5%, #9e8090 100%);
	background:linear-gradient(to bottom, #241b20 5%, #9e8090 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#241b20', endColorstr='#9e8090',GradientType=0);
	background-color:#241b20;
}
#cancelRequest:active {

	top:1px;
}

</style>

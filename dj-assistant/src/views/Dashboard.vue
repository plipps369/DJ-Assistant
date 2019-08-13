<template>
  <div>
    <nav-header></nav-header>
    <div id="dashboard">
      <div id="dash-main">
        <h1 id="display-name">
          <strong>{{user.displayName}}</strong>
        </h1>
      </div>

      <div>
        <b-card
          id="party-list"
          bg-variant="primary"
          text-variant="white"
          title="Party List"
          class="text-center"
        >
          <router-link
            v-for="party in parties"
            :key="party.id"
            :to="{ name: 'party', params: { id: party.id } }"
            tag="button"
            class="btn btn-lg btn-block btn-light"
          >{{party.name}}</router-link>
        </b-card>
      </div>

      <div id="buttons">
        <b-card id="add-song" bg-variant="primary">
          <div>
            <router-link to="/add-song" tag="button" class="btn btn-lg btn-block">Add Song</router-link>
          </div>
        </b-card>

        <b-card id="create-party" bg-variant="primary">
          <div>
            <router-link to="/create-party" tag="button" class="btn btn-lg btn-block">Create Party</router-link>
          </div>
        </b-card>
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
  name: "dashboard",
  data() {
    return {
      user: {},
      parties: []
    };
  },
  components: {
    NavHeader
  },
  methods: {
    getUser() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/DJ`, {
        method: "GET",
        headers: {
          // A Header with our authentication token.
          Authorization: "Bearer " + auth.getToken()
        }
      })
        .then(response => response.json())
        .then(json => {
          this.user = json;
        });
    },
    getParties() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/party`, {
        method: "GET",
        headers: {
          // A Header with our authentication token.
          Authorization: "Bearer " + auth.getToken()
        }
      })
        .then(response => response.json())
        .then(json => {
          this.parties = json;
        });
    }
  },
  created() {
    this.getUser();
    this.getParties();
  }
};
</script>

<style>
.registerBackground {
  background-image: url("~@/assets/dashboard.png") !important;
  background-position: center center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  width: 100%;
  height: auto;
}

#dashboard {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "dash-main dash-main"
    "party-list buttons"
    "party-list buttons"
    "party-list buttons";
    
}

#dash-main {
  grid-area: dash-main;
  display: flex;
  flex-direction: row;
  flex-flow: center;
  justify-content: center;
  
}

#add-song {
  background-image: linear-gradient(black,black,rgb(15, 182, 248));
}


#create-party {
  background-image: linear-gradient(black,black,rgb(15, 182, 248));
}
#party-list {
  margin: 3vh;
  background-image: linear-gradient(black,black,rgb(15, 182, 248));
}



#party-list > div {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
  grid-area: party-list;
}

#party-list > div > button {
  margin: 3vh;
  margin-left: 5vw;
  margin-right: 5vw;
  
}

#add-song {
  padding: 7vh;
  margin: 3vh;
}

#create-party {
  padding: 7vh;
  margin: 3vh;
}

#add-song > div > div {
  -moz-box-shadow: inset 0px 1px 0px 0px #fbafe3;
  -webkit-box-shadow: inset 0px 1px 0px 0px #fbafe3;
  box-shadow: inset 0px 1px 0px 0px #fbafe3;
  background: -webkit-gradient(
    linear,
    left top,
    left bottom,
    color-stop(0.05, #ff4c49),
    color-stop(1, #ef027d)
  );
  background: -moz-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -webkit-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -o-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -ms-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: linear-gradient(to bottom, #ff5bb0 5%, #ff4c49 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff5bb0', endColorstr='#ef027d',GradientType=0);
  background-color: #ff4c49;
  -moz-border-radius: 27px;
  -webkit-border-radius: 27px;
  border-radius: 27px;
  border: 1px solid #ee1eb5;
  cursor: pointer;
  color: #ffffff;
  font-family: Trebuchet MS;
  font-size: 22px;
  font-weight: bold;
  padding: 9px 24px;
  text-decoration: none;
  text-shadow: 0px 1px 0px #c70067;
  display: block;
  width: 100%;
}
#add-song > div > div:hover {
  background-color: #ff4c49;
  background: -webkit-gradient(
    linear,
    left top,
    left bottom,
    color-stop(0.05, #ff4c49),
    color-stop(1, #ff5bb0)
  );
  background: -moz-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -webkit-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -o-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -ms-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: linear-gradient(to bottom, #ff4c49 5%, #ff5bb0 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff4c49', endColorstr='#ff5bb0',GradientType=0);
  background-color: #ff4c49;
}
#add-song > div > div:active {
  top: 1px;
}

#create-party > div > div {
  -moz-box-shadow: inset 0px 1px 0px 0px #fbafe3;
  -webkit-box-shadow: inset 0px 1px 0px 0px #fbafe3;
  box-shadow: inset 0px 1px 0px 0px #fbafe3;
  background: -webkit-gradient(
    linear,
    left top,
    left bottom,
    color-stop(0.05, #ff4c49),
    color-stop(1, #ef027d)
  );
  background: -moz-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -webkit-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -o-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: -ms-linear-gradient(top, #ff5bb0 5%, #ff4c49 100%);
  background: linear-gradient(to bottom, #ff5bb0 5%, #ff4c49 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff5bb0', endColorstr='#ef027d',GradientType=0);
  background-color: #ff4c49;
  -moz-border-radius: 27px;
  -webkit-border-radius: 27px;
  border-radius: 27px;
  border: 1px solid #ee1eb5;
  cursor: pointer;
  color: #ffffff;
  font-family: Trebuchet MS;
  font-size: 22px;
  font-weight: bold;
  padding: 9px 24px;
  text-decoration: none;
  text-shadow: 0px 1px 0px #c70067;
  display: block;
  width: 100%;
}
#create-party > div > div:hover {
  background-color: #ff4c49;
  background: -webkit-gradient(
    linear,
    left top,
    left bottom,
    color-stop(0.05, #ff4c49),
    color-stop(1, #ff5bb0)
  );
  background: -moz-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -webkit-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -o-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: -ms-linear-gradient(top, #ff4c49 5%, #ff5bb0 100%);
  background: linear-gradient(to bottom, #ff4c49 5%, #ff5bb0 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff4c49', endColorstr='#ff5bb0',GradientType=0);
  background-color: #ff4c49;
}
#create-party > div > div:active {
  top: 1px;
}
</style>

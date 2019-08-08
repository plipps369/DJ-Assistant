<template>
  <div>
    <h1>This is the storefront page</h1>
    <form v-on:submit.prevent="getProducts">
      <button type="submit" class="btn btn-primary">Login</button>
      <div>{{storeFront}}</div>
    </form>
  </div>
</template>



<script>
import auth from "@/shared/auth.js";
import { APIService } from "@/shared/APIService";
const apiService = new APIService();

export default {
  name: "storefront",
  data() {
    return {
      products: [],
      storeFront: {}
    };
  },
  methods: {},
  created() {
    this.storeFront = apiService
      .getStore(auth.getUser().sub)
      .then(data => {
        this.storeFront = data;
      })
      .catch(error => {
        window.console.log("Error:", error);
      });

    //  fetch(`${process.env.VUE_APP_REMOTE_API}/storefront/mystore`, {
    //       method: "GET",
    //       headers: {
    //         // A Header with our authentication token.
    //         Authorization: "Bearer " + auth.getToken()
    //       }
    //   }).then(response => response.json())
    //     .then(json => {
    //       this.StoreFront = json;
    //     });
    // }
  }
};
</script>
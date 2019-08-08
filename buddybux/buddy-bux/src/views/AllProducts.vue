<template>
  <div>
    <h1>This is the All Products page</h1>
    <div v-for="product in products" :key="product.id"> {{product}}</div>
  </div>
  
</template>



<script>
import auth from "@/shared/auth.js";

export default {
  name: 'storefront',
  data() {
    return {
      products: [],
      storeFront: {}
    };
  },
  methods: {
  },
  created() {
     fetch(`${process.env.VUE_APP_REMOTE_API}/storefront/products`, {
          method: "GET",
          headers: {
            // A Header with our authentication token.
            Authorization: "Bearer " + auth.getToken()
          }
      }).then(response => response.json())
        .then(json => {
          this.products = json;
        });
    }
  }

</script>
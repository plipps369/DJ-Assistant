<template>
  <div id="app">
    
    <router-link :to="{ name: 'home', params: {}}">Home</router-link> |
    <router-link :to="{ name: 'dashboard', params: {}}">Dashboard</router-link> |
    <router-link :to="{ name: 'account', params: {}}">Account</router-link> |
    <router-link :to="{ name: 'storefront', params: {}}">Store Front</router-link> |
    <router-link :to="{ name: 'login', params: {}}">Login</router-link> |
    <router-link :to="{ name: 'about', params: {}}">About</router-link> |
    <router-link :to="{ name: 'newproduct', params: {}}">New Product </router-link>|
    <router-link :to="{ name: 'allproducts', params: {}}">All Product </router-link>|
    <a href="/logout" v-on:click.prevent="logout">Logout</a>
    <router-view/>
    
  </div>
</template>

<script>
// import {APIService} from '@/APIService';
//import {globals} from '@/main.js';
// const apiService = new APIService();
import auth from "@/shared/auth.js";
export default {
  name: 'app',
  methods: {
    /**
     * Logs the user out and takes them to the login page
     * @function
     */
    logout() {
      auth.destroyToken();
      this.$router.push("/login");
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>

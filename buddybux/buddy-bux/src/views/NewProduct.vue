<!-- 
  The Login Page displays a register and login screen for the user to authenticate.
-->

<template>
  <div id="NewProduct">
    <form v-on:submit.prevent="addProduct">
      <div class="form-group">
        <label for="name">Product Name:</label>
        <input v-model.trim="newProduct.name" type="text" placeholder="Product Name" id="name" />
      </div>

      <div class="form-group">
        <span>Charitable:</span>
        <input type="radio" id="True" value="true" v-model="newProduct.IsCharitable" />
        <label for="IsCharitable">true</label>
        <input type="radio" id="false" value="false" v-model="newProduct.IsCharitable" />
        <label for="IsCharitable">False</label>
      </div>
 
      <div class="form-actions">
        <button type="submit" class="btn btn-primary">Login</button>
      </div>
    </form>

    <error-message v-bind:error="error"></error-message>
  </div>
</template>

<script>
import auth from "@/shared/auth";
import ErrorMessage from "@/components/ui/ErrorMessage.vue";

export default {
  components: { ErrorMessage },
  data() {
    return {
      error: "",
      /** Contents of the newProduct form */
      newProduct: {
        name: "",
        IsCharitable: true
      }
    };
  },
  methods: {
    /**
     * Navigates the user to the home route.
     * @function
     */
    goDashboard() {
      this.$router.push("/dashboard");
    },

    /**
     * Signs the user up and then redirects them to the dashboard.
     */
    async addProduct() {
      this.error = "";

      try {
        const url = `${process.env.VUE_APP_REMOTE_API}/StoreFront/addproduct`;
        const response = await fetch(url, {
          method: "POST",
          headers: {
            Authorization: "Bearer " + auth.getToken(),
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.newProduct)
        });

        // We definitely need the response if success or not.
        const data = await response.json();

        if (response.status === 400) {
          this.error = data.message;
        } else {
          auth.saveToken(data);
          this.goDashboard();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error attempting to register new product";
      }
    }

  },
  created() {
      if(!auth.getToken()){
               this.$router.push("/login");
      }
    }
};
</script>

<style scoped>
form label {
  padding-right: 10px;
}

#login {
  padding-top: 20px;
}

#login button {
  margin-top: 10px;
}
</style>

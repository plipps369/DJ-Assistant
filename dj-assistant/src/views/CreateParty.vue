<template>
  <div id="create-main" class="text-center">
    <nav-header></nav-header>
    <form class="form-register" @submit.prevent="newParty">
      <h1 class="h3 mb-3 font-weight-normal">New Party</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="newPartyErrors"
      >There were problems creating this party</div>
      <label for="name" class="sr-only">Name</label>
      <input
        type="text"
        id="name"
        class="form-control"
        placeholder="Name"
        v-model="party.name"
        required
        autofocus
      />
      <label for="description" class="sr-only">Description</label>
      <input
        type="text"
        id="description"
        class="form-control"
        placeholder="Description"
        v-model="party.description"
      />

      <br />
      <button type="Submit" class="btn btn-outline-warning">Create Party</button>
    </form>
  </div>
</template>

<script>
import NavHeader from "@/components/NavHeader.vue";

export default {
  name: "create-party",
  components: {
    NavHeader
  },
  data() {
    return {
      party: {
        name: "",
        description: ""
      },
      newPartyErrors: false
    };
  },
  methods: {
    newParty() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/party/NewParty`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.party)
      })
        .then(response => {
          if (response.ok) {
            return response.text();
          } else {
            this.newPartyErrors = true;
          }
        })

        .catch(err => console.error(err));
    }
  }
};
</script>

<style>
#create-main {
  margin-top: 120vh;
}
</style>

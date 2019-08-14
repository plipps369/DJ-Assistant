<template>
  <div class="home">
    <nav-header></nav-header>
    <landing-page> </landing-page>
  </div>
</template>

<script>

// @ is an alias to /src
import NavHeader from '@/components/NavHeader.vue'
import LandingPage from '@/components/LandingPage.vue';
import auth from '../auth';

export default {
  name: 'home',
  data() {
    return {
     
    }
  },
  computed: {
    user: function () {
      return auth.getUser();
    }
  },
  components: {
    NavHeader,
    LandingPage
  },
}
</script>
 
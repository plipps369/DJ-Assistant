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

export default {
  name: 'home',
  
  components: {
    NavHeader,
    LandingPage
  },
}
</script>
 
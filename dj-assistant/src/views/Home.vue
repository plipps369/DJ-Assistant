<template>
  <div class="home">
    <nav-header></nav-header>
    <img alt="Vue logo" src="../assets/logo.png">
  </div>
</template>

<script>
// @ is an alias to /src
import NavHeader from '@/components/NavHeader.vue'

export default {
  name: 'home',
  components: {
    NavHeader
  }
}
</script>

<template>
    <div>
        <nav-header></nav-header>
        <div id="dash-main">
        <h1>THIS IS THE DASHBOARD YA'LL</h1>
        </div>
    </div>
</template>

<script>
import NavHeader from '@/components/NavHeader.vue'

export default {
  name: 'dashboard',
  components: {
    NavHeader
  },
  
}
</script>

<style>
#dash-main{
    margin-top:15vh;
}
</style>

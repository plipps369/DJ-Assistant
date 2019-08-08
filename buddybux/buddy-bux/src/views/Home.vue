<template>
  <div>
    <h1>This is a home page</h1>
  </div>
</template>

<script>
import auth from "@/shared/auth";

export default {
  name: 'home',
  user: auth.getUser()
}
</script>
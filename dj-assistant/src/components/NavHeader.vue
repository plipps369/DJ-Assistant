<template>
  <div>
    <header>
      <div class="container-fluid">
            <div class="col-lg-12">
                <img id="logo" src="@/assets/dj-logo.png" class="img-responsive">
                <span id="header-text">DJ Bluntz</span>
            </div>
        </div>
    </header>
    <nav class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark">
      <a class="navbar-brand" href="#">DJ App</a>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <router-link :to="{name: 'home'}" class="nav-link">
              Home
            </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'about'}" class="nav-link">
              About
              </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'login'}" class="nav-link">
              Login
              </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'register'}" class="nav-link">
              Register
              </router-link>
          </li>
        </ul>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "nav-header"
};
</script>

<style>
nav {
  position: sticky;
  overflow: hidden;
  top: 0;
  width: 100%; 
  z-index: 1000;
}
#logo { 
  height: 5rem;
}
#header-text {
  font-size: 3rem;
  vertical-align: middle;
}
</style>

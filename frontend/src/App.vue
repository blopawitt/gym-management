<template>
  <div id="app">
    <AppNavBar />
    <router-view />
  </div>
</template>

<script>
import AppNavBar from "./components/AppNavbar.vue";

export default {
  name: "App",
  components: {
    AppNavBar,
  },
};
</script>

<style>
/* Estilos Globais */
</style>

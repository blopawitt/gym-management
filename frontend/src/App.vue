<template>
  <div id="app">
    <AppMenubar />
    <router-view></router-view>
    <!-- Use router-view to display the routed components -->
  </div>
</template>

<script>
import AppMenubar from "@/components/AppNavbar.vue";

export default {
  name: "App",
  components: {
    AppMenubar,
  },
};
</script>

<style>
/* Global styles */
body {
  font-family: Arial, sans-serif;
  margin: 0;
  padding: 0;
  background-color: #f4f4f4;
}

#app {
  display: flex;
  flex-direction: column;
}

.main-content {
  padding: 20px;
  width: 100%;
}

h1 {
  text-align: center;
  color: #333;
}

h2 {
  color: #555;
}
</style>

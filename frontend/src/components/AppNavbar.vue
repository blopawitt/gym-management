<template>
  <nav class="bg-gray-800 p-4">
    <div class="container mx-auto flex items-center justify-between relative">
      <!-- Left-aligned navigation links -->
      <div class="flex items-center space-x-4 mr-auto">
        <a href="/" class="text-white text-lg font-semibold">Home</a>
        <a href="/members" class="text-gray-300 hover:text-white">Members</a>
        <a href="/trainers" class="text-gray-300 hover:text-white">Trainers</a>
        <a href="/classes" class="text-gray-300 hover:text-white">Classes</a>
      </div>
      <!-- Centered title -->
      <div class="flex items-center space-x-4">
        <i class="fas fa-dumbbell text-white space-x-2"></i>
        <h1 class="text-white text-lg font-semibold">Gym Management</h1>
      </div>
      <!-- Right-aligned search form -->
      <div class="flex items-center space-x-4 ml-auto">
        <input
          type="text"
          placeholder="Search"
          class="px-4 py-2 rounded-md bg-gray-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
        >
          Search
        </button>
        <button
          @click="logout"
          class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600"
        >
          Logout
        </button>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "AppNavbar",
  methods: {
    logout() {
      localStorage.removeItem("token");
      this.$router.push("/login");
    },
  },
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

<template>
  <nav class="bg-white p-4 shadow-md">
    <div class="container mx-auto flex items-center justify-between relative">
      <!-- Left-aligned navigation links -->
      <div class="flex items-center space-x-4 mr-auto">
        <a href="/" class="text-gray-800 text-lg font-semibold">Home</a>
        <a href="/members" class="text-gray-600 hover:text-gray-800">Members</a>
        <a href="/trainers" class="text-gray-600 hover:text-gray-800"
          >Trainers</a
        >
        <a href="/classes" class="text-gray-600 hover:text-gray-800">Classes</a>
      </div>
      <!-- Centered title -->
      <div class="flex items-center space-x-4">
        <i class="fas fa-dumbbell text-gray-800 space-x-2"></i>
        <h1 class="text-gray-800 text-lg font-semibold">Gym Management</h1>
      </div>
      <!-- Right-aligned search form -->
      <div class="flex items-center space-x-4 ml-auto">
        <input
          type="text"
          placeholder="Search"
          class="px-4 py-2 rounded-md bg-gray-200 text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-300"
        />
        <button
          class="bg-blue-300 text-white px-4 py-2 rounded-md hover:bg-blue-400"
        >
          Search
        </button>
        <button
          @click="logout"
          class="bg-red-300 text-white px-4 py-2 rounded-md hover:bg-red-400"
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

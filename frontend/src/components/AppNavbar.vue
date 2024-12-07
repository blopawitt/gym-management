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
      <div class="absolute left-1/2 transform -translate-x-1/2">
        <h1 class="text-white text-lg font-semibold">
          Gym Management Dashboard
        </h1>
      </div>
      <!-- Right-aligned search form -->
      <div class="flex items-center space-x-4">
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
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "AppNavbar",
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

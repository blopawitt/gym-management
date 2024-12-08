<template>
  <nav class="bg-white p-4 shadow-md">
    <div class="w-full flex items-center justify-between relative">
      <!-- Left-aligned navigation links -->
      <div class="flex items-center space-x-4">
        <a href="/" class="text-gray-800 text-lg font-semibold">Home</a>
        <a href="/members" class="text-gray-600 hover:text-gray-800">Members</a>
        <a href="/trainers" class="text-gray-600 hover:text-gray-800"
          >Trainers</a
        >
        <a href="/classes" class="text-gray-600 hover:text-gray-800">Classes</a>
        <a href="/subscriptions" class="text-gray-600 hover:text-gray-800"
          >Subscriptions</a
        >
      </div>
      <!-- Centered title -->
      <div
        class="absolute left-1/2 transform -translate-x-1/2 flex items-center space-x-2"
      >
        <i class="fas fa-dumbbell text-gray-800"></i>
        <h1 class="text-gray-800 text-lg font-semibold">Gym Management</h1>
      </div>
      <!-- Right-aligned search form and user icon -->
      <div class="flex items-center space-x-4">
        <input
          type="text"
          placeholder="Search"
          class="px-4 py-2 rounded-md bg-gray-200 text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-300"
          @keyup.enter="search"
        />
        <div class="relative">
          <img
            v-if="userPhoto"
            :src="userPhoto"
            alt="Admin Photo"
            class="w-8 h-8 rounded-full cursor-pointer"
            @click="toggleDropdown"
          />
          <div
            v-else
            class="w-8 h-8 rounded-full bg-gray-300 flex items-center justify-center cursor-pointer"
            @click="toggleDropdown"
          >
            <i class="fas fa-user text-gray-600"></i>
          </div>
          <div
            v-if="dropdownOpen"
            class="absolute right-0 mt-2 w-48 bg-white border rounded-md shadow-lg"
            style="left: 50%; transform: translateX(-50%)"
          >
            <a
              href="#"
              @click="logout"
              class="block px-4 py-2 text-gray-800 hover:bg-gray-100"
              >Logout</a
            >
            <!-- Add more dropdown options here -->
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "AppNavbar",
  data() {
    return {
      dropdownOpen: false,
      userPhoto: null, // Placeholder for user photo URL
    };
  },
  methods: {
    logout() {
      localStorage.removeItem("token");
      this.$router.push("/login");
      this.dropdownOpen = false; // Close the dropdown
    },
    search() {
      // Implement search functionality here
    },
    toggleDropdown() {
      this.dropdownOpen = !this.dropdownOpen;
    },
  },
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

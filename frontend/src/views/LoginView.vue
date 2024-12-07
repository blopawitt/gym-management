<template>
  <div class="container mx-auto p-4">
    <h1 class="text-2xl font-bold mb-4">Login</h1>
    <form @submit.prevent="login">
      <div class="mb-4">
        <label class="block text-gray-700">Username</label>
        <input
          v-model="username"
          type="text"
          class="w-full px-4 py-2 border rounded-md"
          required
        />
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Password</label>
        <input
          v-model="password"
          type="password"
          class="w-full px-4 py-2 border rounded-md"
          required
        />
      </div>
      <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md">
        Login
      </button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import axios from "../axios-instance"; // Import the configured Axios instance

export default defineComponent({
  name: "LoginView",
  setup() {
    const username = ref("");
    const password = ref("");

    const login = async () => {
      try {
        const response = await axios.post("/auth/login", {
          username: username.value,
          password: password.value,
        });
        localStorage.setItem("token", response.data.token);
        // Redirect to the classes page or another protected page
      } catch (error) {
        console.error("Error logging in:", error);
      }
    };

    return {
      username,
      password,
      login,
    };
  },
});
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

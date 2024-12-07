<template>
  <div class="min-h-screen flex items-start justify-center bg-gray-100 pt-16">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md mr-24">
      <form @submit.prevent="login">
        <div class="mb-4">
          <label
            class="block text-gray-700 text-sm font-bold mb-2"
            for="username"
          >
            Username
          </label>
          <input
            v-model="username"
            type="text"
            id="username"
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            required
          />
        </div>
        <div class="mb-6">
          <label
            class="block text-gray-700 text-sm font-bold mb-2"
            for="password"
          >
            Password
          </label>
          <input
            v-model="password"
            type="password"
            id="password"
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
            required
          />
        </div>
        <div class="flex items-center justify-center">
          <button
            type="submit"
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          >
            Sign In
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import { useRouter } from "vue-router";
import axios from "axios";

axios.defaults.baseURL = "https://localhost:7032";

export default {
  name: "LoginView",
  setup() {
    const username = ref("");
    const password = ref("");
    const router = useRouter();

    const login = async () => {
      try {
        const response = await axios.post("/api/auth/login", {
          username: username.value,
          password: password.value,
        });
        localStorage.setItem("token", response.data.token);
        router.push("/");
      } catch (error) {
        console.error("Login failed:", error);
      }
    };

    return {
      username,
      password,
      login,
    };
  },
};
</script>

<style scoped>
/* Adicione qualquer estilo específico do componente aqui */
</style>

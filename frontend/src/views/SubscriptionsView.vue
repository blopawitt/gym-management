<template>
  <div class="container mx-auto p-4">
    <p class="text-center font-bold mt-2">Current prices:</p>
    <div class="bg-gray-100 p-4 rounded-md text-center">
      <p><strong>Monthly:</strong> ${{ latestPrices.monthly }}</p>
      <p><strong>Trimestral:</strong> ${{ latestPrices.trimestral }}</p>
      <p><strong>Semi-annual:</strong> ${{ latestPrices.semiAnnual }}</p>
      <p><strong>Annual:</strong> ${{ latestPrices.annual }}</p>
    </div>
    <form @submit.prevent="savePrices" class="text-center">
      <div class="mb-4 mt-2">
        <label class="block text-gray-700">Monthly</label>
        <div class="relative w-48 mx-auto" style="width: 300px">
          <span
            class="absolute inset-y-0 left-0 pl-3 flex items-center text-gray-500"
            >$</span
          >
          <input
            v-model="prices.monthly"
            type="number"
            class="w-full px-4 py-2 border rounded-md pl-8"
          />
        </div>
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Trimestral</label>
        <div class="relative w-48 mx-auto" style="width: 300px">
          <span
            class="absolute inset-y-0 left-0 pl-3 flex items-center text-gray-500"
            >$</span
          >
          <input
            v-model="prices.trimestral"
            type="number"
            class="w-full px-4 py-2 border rounded-md pl-8"
          />
        </div>
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Semi-annual</label>
        <div class="relative w-48 mx-auto" style="width: 300px">
          <span
            class="absolute inset-y-0 left-0 pl-3 flex items-center text-gray-500"
            >$</span
          >
          <input
            v-model="prices.semiAnnual"
            type="number"
            class="w-full px-4 py-2 border rounded-md pl-8"
          />
        </div>
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Annual</label>
        <div class="relative w-48 mx-auto" style="width: 300px">
          <span
            class="absolute inset-y-0 left-0 pl-3 flex items-center text-gray-500"
            >$</span
          >
          <input
            v-model="prices.annual"
            type="number"
            class="w-full px-4 py-2 border rounded-md pl-8"
          />
        </div>
      </div>
      <button
        type="submit"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-400"
      >
        Save
      </button>
    </form>
    <p v-if="successMessage" class="text-green-500 mt-4">
      {{ successMessage }}
    </p>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "SubscriptionsView",
  data() {
    return {
      prices: {
        monthly: 0,
        trimestral: 0,
        semiAnnual: 0,
        annual: 0,
      },
      latestPrices: {
        monthly: 0,
        trimestral: 0,
        semiAnnual: 0,
        annual: 0,
      },
      successMessage: "", // Add a success message property
    };
  },
  async created() {
    try {
      const response = await axios.get("/api/prices/latest");
      this.latestPrices = response.data;
    } catch (error) {
      console.error("Error fetching latest prices:", error);
    }
  },
  methods: {
    async savePrices() {
      try {
        const response = await axios.post("/api/prices", this.prices);
        console.log("Prices saved successfully:", response.data);
        this.successMessage = "Prices saved successfully!"; // Set success message
      } catch (error) {
        console.error("Error saving prices:", error);
      }
    },
  },
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

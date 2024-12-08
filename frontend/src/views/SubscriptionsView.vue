<template>
  <div class="container mx-auto p-4">
    <form @submit.prevent="savePrices">
      <div class="mb-4 mt-2">
        <label class="block text-gray-700">Monthly</label>
        <input
          v-model="prices.monthly"
          type="number"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Trimestral</label>
        <input
          v-model="prices.trimestral"
          type="number"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Semi-annual</label>
        <input
          v-model="prices.semiAnnual"
          type="number"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">Annual</label>
        <input
          v-model="prices.annual"
          type="number"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md">
        Save
      </button>
    </form>
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
    };
  },
  methods: {
    savePrices() {
      axios
        .put("/api/prices/1", this.prices) // Assuming the ID is 1 for the unique price entry
        .then((response) => {
          console.log("Prices updated successfully:", response.data);
        })
        .catch((error) => {
          console.error("Error updating prices:", error);
        });
    },
  },
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

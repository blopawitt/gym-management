<template>
  <div class="flex flex-col md:flex-row w-full">
    <!-- Incomes Table -->
    <div class="w-full md:w-1/2 p-4">
      <h2 class="text-xl font-semibold mb-4">Incomes</h2>
      <table class="min-w-full bg-white">
        <thead>
          <tr>
            <th class="py-2 px-4 border-b text-left">Date</th>
            <th class="py-2 px-4 border-b text-left">Description</th>
            <th class="py-2 px-4 border-b text-left">Amount</th>
            <th class="py-2 px-4 border-b text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="income in incomes" :key="income.id">
            <td class="py-2 px-4 border-b">
              <input
                v-if="income.editing"
                v-model="income.date"
                type="date"
                class="border p-1"
              />
              <span v-else>{{ formatDate(income.date) }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <input
                v-if="income.editing"
                v-model="income.description"
                type="text"
                class="border p-1"
              />
              <span v-else>{{ income.description }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <input
                v-if="income.editing"
                v-model="income.amount"
                type="number"
                class="border p-1"
              />
              <span v-else>{{ income.amount }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <button
                v-if="income.editing"
                @click="saveIncome(income)"
                class="bg-blue-500 text-white w-full h-full px-2 py-1"
              >
                Save
              </button>
              <button
                v-else
                @click="income.editing = true"
                class="bg-yellow-500 text-white w-full h-full px-2 py-1"
              >
                Edit
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <!-- Expenses Table -->
    <div class="w-full md:w-1/2 p-4">
      <h2 class="text-xl font-semibold mb-4">Expenses</h2>
      <table class="min-w-full bg-white">
        <thead>
          <tr>
            <th class="py-2 px-4 border-b text-left">Date</th>
            <th class="py-2 px-4 border-b text-left">Description</th>
            <th class="py-2 px-4 border-b text-left">Amount</th>
            <th class="py-2 px-4 border-b text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="expense in expenses" :key="expense.id">
            <td class="py-2 px-4 border-b">
              <input
                v-if="expense.editing"
                v-model="expense.date"
                type="date"
                class="border p-1"
              />
              <span v-else>{{ formatDate(expense.date) }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <input
                v-if="expense.editing"
                v-model="expense.description"
                type="text"
                class="border p-1"
              />
              <span v-else>{{ expense.description }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <input
                v-if="expense.editing"
                v-model="expense.amount"
                type="number"
                class="border p-1"
              />
              <span v-else>{{ expense.amount }}</span>
            </td>
            <td class="py-2 px-4 border-b">
              <button
                v-if="expense.editing"
                @click="saveExpense(expense)"
                class="bg-blue-500 text-white w-full h-full px-2 py-1"
              >
                Save
              </button>
              <button
                v-else
                @click="expense.editing = true"
                class="bg-yellow-500 text-white w-full h-full px-2 py-1"
              >
                Edit
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "MoneyView",
  data() {
    return {
      incomes: [], // Placeholder for incomes data
      expenses: [], // Placeholder for expenses data
    };
  },
  created() {
    this.fetchIncomes();
    this.fetchExpenses();
  },
  methods: {
    async fetchIncomes() {
      try {
        const response = await axios.get("/api/incomes");
        this.incomes = response.data.map((income) => ({
          ...income,
          editing: false,
        }));
      } catch (error) {
        console.error("Error fetching incomes:", error);
      }
    },
    async fetchExpenses() {
      try {
        const response = await axios.get("/api/expenses");
        this.expenses = response.data.map((expense) => ({
          ...expense,
          editing: false,
        }));
      } catch (error) {
        console.error("Error fetching expenses:", error);
      }
    },
    async saveIncome(income) {
      try {
        await axios.put(`/api/incomes/${income.id}`, income);
        income.editing = false;
      } catch (error) {
        console.error("Error saving income:", error);
      }
    },
    async saveExpense(expense) {
      try {
        await axios.put(`/api/expenses/${expense.id}`, expense);
        expense.editing = false;
      } catch (error) {
        console.error("Error saving expense:", error);
      }
    },
    formatDate(dateString) {
      const options = { year: "numeric", month: "short", day: "numeric" };
      return new Date(dateString).toLocaleDateString(undefined, options);
    },
  },
};
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

<script lang="ts">
import { defineComponent } from "vue";
import FullCalendar from "@fullcalendar/vue3";
import dayGridPlugin from "@fullcalendar/daygrid";
import axios from "../axios-instance"; // Import the configured Axios instance

interface Expense {
  description: string;
  amount: number;
  date: string;
}

interface Income {
  description: string;
  amount: number;
  date: string;
}

export default defineComponent({
  name: "HomeView",
  components: {
    FullCalendar,
  },
  data() {
    return {
      income: 0,
      outcome: 0,
      balance: 0,
      newClass: {
        name: "",
        description: "",
        startTime: "",
        endTime: "",
        trainerId: 0,
      },
      newExpense: {
        description: "",
        amount: 0,
        date: "",
      },
      newIncome: {
        description: "",
        amount: 0,
        date: "",
      },
      calendarOptions: {
        plugins: [dayGridPlugin],
        initialView: "dayGridMonth",
        weekends: true,
        events: [],
      },
      additionalExpenses: [] as Expense[],
      additionalIncomes: [] as Income[],
    };
  },
  methods: {
    fetchData() {
      // Placeholder for fetching data logic
    },
    fetchIncomeOutcome() {
      console.log("Fetching income and outcome");
      this.fetchTrainerSalaries();
    },
    fetchTrainerSalaries() {
      axios
        .get("/trainers")
        .then((response) => {
          const totalSalary = response.data.reduce(
            (sum: number, trainer: { salary: number }) => sum + trainer.salary,
            0
          );
          const totalExpenses = this.additionalExpenses.reduce(
            (sum, expense) => sum + expense.amount,
            0
          );
          this.outcome = totalSalary + totalExpenses; // Sum annual salaries and additional expenses
        })
        .catch((error) => {
          console.error("Error fetching trainer salaries:", error);
        });
    },
    fetchClasses() {
      axios
        .get("/classes")
        .then((response) => {
          const events = response.data.map(
            (classItem: {
              name: string;
              startTime: string;
              endTime: string;
            }) => ({
              title: classItem.name,
              start: classItem.startTime,
              end: classItem.endTime,
            })
          );
          this.calendarOptions.events = events;
        })
        .catch((error) => {
          console.error("Error fetching classes:", error);
        });
    },
    addClass() {
      // Placeholder for adding a new class logic
    },
    addExpense() {
      // Ensure the date is in the correct format
      this.newExpense.date = new Date(this.newExpense.date).toISOString();

      axios
        .post("/expenses", this.newExpense)
        .then((response) => {
          this.additionalExpenses.push(response.data);
          this.newExpense.description = "";
          this.newExpense.amount = 0;
          this.newExpense.date = "";
          this.fetchIncomeOutcome(); // Recalculate outcome after adding an expense
        })
        .catch((error) => {
          console.error("Error adding expense:", error);
        });
    },
    addIncome() {
      // Ensure the date is in the correct format
      this.newIncome.date = new Date(this.newIncome.date).toISOString();

      axios
        .post("/incomes", this.newIncome)
        .then((response) => {
          this.additionalIncomes.push(response.data);
          this.newIncome.description = "";
          this.newIncome.amount = 0;
          this.newIncome.date = "";
          this.fetchIncomeOutcome(); // Recalculate income after adding an income
        })
        .catch((error) => {
          console.error("Error adding income:", error);
        });
    },
    fetchTotalIncome() {
      axios
        .get("/incomes/total-income")
        .then((response) => {
          this.income = response.data;
        })
        .catch((error) => {
          console.error("Error fetching total income:", error);
        });
    },
  },
  mounted() {
    this.fetchData();
    this.fetchIncomeOutcome();
    this.fetchClasses();
    this.fetchTotalIncome(); // Fetch total income on mount
  },
});
</script>

<template>
  <div class="home flex">
    <div class="dashboard flex-1 p-4 mt-16">
      <div class="flex">
        <div class="card bg-white shadow-md rounded-md p-4 mb-4 flex-1">
          <h2 class="text-xl font-bold mb-2">Balance</h2>
          <p
            :class="{
              'text-red-500': income - outcome < 0,
              'text-green-500': income - outcome > 0,
            }"
          >
            $ {{ income - outcome }}
          </p>
        </div>
        <div class="card bg-white shadow-md rounded-md p-4 mb-4 flex-1 ml-4">
          <h2 class="text-xl font-bold mb-2">Total Income</h2>
          <p>$ {{ income }}</p>
        </div>
        <div class="card bg-white shadow-md rounded-md p-4 mb-4 flex-1 ml-4">
          <h2 class="text-xl font-bold mb-2">Total Outcome</h2>
          <p>$ {{ outcome }}</p>
        </div>
      </div>
      <div class="card bg-white shadow-md rounded-md p-4 mb-4">
        <h2 class="text-xl font-bold mb-2">Additional Expenses</h2>
        <form @submit.prevent="addExpense">
          <div class="mb-4">
            <label class="block text-gray-700">Description</label>
            <input
              v-model="newExpense.description"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Amount</label>
            <input
              v-model="newExpense.amount"
              type="number"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Date</label>
            <input
              v-model="newExpense.date"
              type="date"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="flex justify-end">
            <button
              type="submit"
              class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-400"
            >
              Add Expense
            </button>
          </div>
        </form>
        <ul>
          <li v-for="expense in additionalExpenses" :key="expense.description">
            {{ expense.description }}: {{ expense.amount }} ({{ expense.date }})
          </li>
        </ul>
      </div>
      <div class="card bg-white shadow-md rounded-md p-4 mb-4">
        <h2 class="text-xl font-bold mb-2">Additional Incomes</h2>
        <form @submit.prevent="addIncome">
          <div class="mb-4">
            <label class="block text-gray-700">Description</label>
            <input
              v-model="newIncome.description"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Amount</label>
            <input
              v-model="newIncome.amount"
              type="number"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Date</label>
            <input
              v-model="newIncome.date"
              type="date"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="flex justify-end">
            <button
              type="submit"
              class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-400"
            >
              Add Income
            </button>
          </div>
        </form>
        <ul>
          <li v-for="income in additionalIncomes" :key="income.description">
            {{ income.description }}: {{ income.amount }} ({{ income.date }})
          </li>
        </ul>
      </div>
    </div>
    <div class="sidebar flex-1 p-4">
      <FullCalendar :options="calendarOptions" />
      <div class="bg-white p-6 rounded-md mt-4">
        <h2 class="text-xl font-bold mb-4">Add New Class</h2>
        <form @submit.prevent="addClass">
          <div class="mb-4">
            <label class="block text-gray-700">Name</label>
            <input
              v-model="newClass.name"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Description</label>
            <input
              v-model="newClass.description"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Start Time</label>
            <input
              v-model="newClass.startTime"
              type="datetime-local"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">End Time</label>
            <input
              v-model="newClass.endTime"
              type="datetime-local"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Trainer ID</label>
            <input
              v-model="newClass.trainerId"
              type="number"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="flex justify-end">
            <button
              type="submit"
              class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-400"
            >
              Add Class
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
.home {
  display: flex;
}

.dashboard,
.sidebar {
  flex: 1;
  padding: 20px;
}

.card {
  background-color: white;
  padding: 20px;
  margin-bottom: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.text-red-500 {
  color: red;
}

.text-green-500 {
  color: green;
}
</style>

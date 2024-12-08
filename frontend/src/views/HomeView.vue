<script lang="ts">
import { defineComponent } from "vue";
import FullCalendar from "@fullcalendar/vue3";
import dayGridPlugin from "@fullcalendar/daygrid";
import axios from "../axios-instance"; // Import the configured Axios instance

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
      dateRange: {
        startDate: "",
        endDate: "",
      },
      newClass: {
        name: "",
        description: "",
        startTime: "",
        endTime: "",
        trainerId: 0,
      },
      calendarOptions: {
        plugins: [dayGridPlugin],
        initialView: "dayGridMonth",
        weekends: true,
        events: [],
      },
    };
  },
  methods: {
    fetchData() {
      // Placeholder for fetching data logic
    },
    fetchIncomeOutcome() {
      // Placeholder for fetching income and outcome based on date range
      console.log(
        "Fetching income and outcome for date range:",
        this.dateRange
      );
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
  },
  watch: {
    dateRange: {
      handler() {
        this.fetchIncomeOutcome();
      },
      deep: true,
    },
  },
  mounted() {
    this.fetchData();
    this.fetchIncomeOutcome();
    this.fetchClasses();
  },
});
</script>

<template>
  <div class="home flex">
    <div class="dashboard flex-1 p-4 mt-10">
      <div class="mb-4">
        <label class="block text-gray-700">Start Date</label>
        <input
          v-model="dateRange.startDate"
          type="date"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <div class="mb-4">
        <label class="block text-gray-700">End Date</label>
        <input
          v-model="dateRange.endDate"
          type="date"
          class="w-full px-4 py-2 border rounded-md"
        />
      </div>
      <div class="card bg-white shadow-md rounded-md p-4 mb-4">
        <h2 class="text-xl font-bold mb-2">Balance</h2>
        <p>{{ income - outcome }}</p>
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
              class="bg-blue-500 text-white px-4 py-2 rounded-md"
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
</style>

<template>
  <div class="container mx-auto">
    <div class="mb-4 mt-2">
      <button
        @click="openModal"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
      >
        Add Class
      </button>
    </div>
    <ul class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <li
        v-for="classItem in classes"
        :key="classItem.id"
        class="flex flex-col justify-between items-start p-4 bg-white shadow-md rounded-md"
      >
        <div>
          <h3 class="text-xl font-bold mb-2">{{ classItem.name }}</h3>
          <p class="text-gray-700 mb-2">{{ classItem.description }}</p>
          <p class="text-gray-500 mb-1">
            Start: {{ new Date(classItem.startTime).toLocaleString() }}
          </p>
          <p class="text-gray-500 mb-1">
            End: {{ new Date(classItem.endTime).toLocaleString() }}
          </p>
          <p class="text-gray-500 mb-1">
            Trainer ID: {{ classItem.trainerId }}
          </p>
          <p class="text-gray-500 mb-1">
            Trainer: {{ classItem.trainer ? classItem.trainer.name : "N/A" }}
          </p>
        </div>
        <button
          @click="removeClass(classItem.id)"
          class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600 mt-4 self-end"
        >
          Remove
        </button>
      </li>
    </ul>

    <!-- Modal -->
    <div
      v-if="isModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md">
        <h2 class="text-xl font-bold mb-4">Add New Class</h2>
        <form @submit.prevent="submitForm">
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
              type="button"
              @click="closeModal"
              class="bg-gray-500 text-white px-4 py-2 rounded-md mr-2"
            >
              Cancel
            </button>
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

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import axios from "../axios-instance"; // Import the configured Axios instance

export default defineComponent({
  name: "ClassesView",
  setup() {
    interface ClassItem {
      id: number;
      name: string;
      description: string;
      startTime: string;
      endTime: string;
      trainerId: number;
      trainer: {
        name: string;
      } | null;
    }

    const classes = ref<ClassItem[]>([]);
    const isModalOpen = ref(false);
    const newClass = ref({
      name: "",
      description: "",
      startTime: "",
      endTime: "",
      trainerId: 0,
    });

    const fetchClasses = async () => {
      try {
        const response = await axios.get("/classes");
        const classItems = response.data;
        for (const classItem of classItems) {
          classItem.trainer = await fetchTrainer(classItem.trainerId);
        }
        classes.value = classItems;
      } catch (error) {
        console.error("Error fetching classes:", error);
      }
    };

    const fetchTrainer = async (trainerId: number) => {
      try {
        const response = await axios.get(`/trainers/${trainerId}`);
        return response.data;
      } catch (error) {
        console.error("Error fetching trainer:", error);
        return null;
      }
    };

    const addClass = async () => {
      try {
        const response = await axios.post("/classes", newClass.value);
        const addedClass = response.data;
        addedClass.trainer = await fetchTrainer(addedClass.trainerId);
        classes.value.push(addedClass);
        closeModal();
      } catch (error) {
        console.error("Error adding class:", error);
      }
    };

    const removeClass = async (id: number) => {
      try {
        await axios.delete(`/classes/${id}`);
        classes.value = classes.value.filter(
          (classItem) => classItem.id !== id
        );
      } catch (error) {
        console.error("Error removing class:", error);
      }
    };

    const openModal = () => {
      isModalOpen.value = true;
    };

    const closeModal = () => {
      isModalOpen.value = false;
    };

    const submitForm = () => {
      addClass();
    };

    onMounted(fetchClasses);

    return {
      classes,
      isModalOpen,
      newClass,
      openModal,
      closeModal,
      submitForm,
      addClass,
      removeClass,
      fetchTrainer,
    };
  },
});
</script>

<style scoped>
/* Add any component-specific styles here */
</style>

<template>
  <div class="container mx-auto">
    <div class="mt-2">
      <button
        @click="openModal"
        class="bg-blue-500 text-white px-4 py-2 rounded-md mb-4"
      >
        Add Trainer
      </button>
    </div>
    <table class="min-w-full bg-white">
      <thead>
        <tr>
          <th class="py-2 px-4 border-b text-left">ID</th>
          <th class="py-2 px-4 border-b text-left">Name</th>
          <th class="py-2 px-4 border-b text-left">Email</th>
          <th class="py-2 px-4 border-b text-left">Phone</th>
          <th class="py-2 px-4 border-b text-left">Address</th>
          <th class="py-2 px-4 border-b text-left">Joining Date</th>
          <th class="py-2 px-4 border-b text-left">Salary</th>
          <th class="py-2 px-4 border-b text-left">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="trainer in trainers" :key="trainer.id">
          <td class="py-2 px-4 border-b text-left">{{ trainer.id }}</td>
          <td class="py-2 px-4 border-b text-left">{{ trainer.name }}</td>
          <td class="py-2 px-4 border-b text-left">{{ trainer.email }}</td>
          <td class="py-2 px-4 border-b text-left">{{ trainer.phone }}</td>
          <td class="py-2 px-4 border-b text-left">{{ trainer.address }}</td>
          <td class="py-2 px-4 border-b text-left">
            {{ new Date(trainer.joiningDate).toLocaleDateString() }}
          </td>
          <td class="py-2 px-4 border-b">{{ trainer.salary }}</td>
          <td class="py-2 px-4 border-b">
            <div class="flex space-x-2">
              <button
                @click="editTrainer(trainer)"
                class="flex-1 bg-yellow-500 text-white px-4 py-2 rounded-md"
              >
                Edit
              </button>
              <button
                @click="deleteTrainer(trainer.id)"
                class="flex-1 bg-red-500 text-white px-4 py-2 rounded-md"
              >
                Delete
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <div
      v-if="isModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md w-3/4">
        <h2 class="text-xl font-bold mb-4">
          {{ isEditing ? "Edit Trainer" : "Add Trainer" }}
        </h2>
        <form @submit.prevent="isEditing ? updateTrainer() : addTrainer()">
          <div class="mb-4">
            <label class="block text-gray-700">Name</label>
            <input
              v-model="trainerForm.name"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Email</label>
            <input
              v-model="trainerForm.email"
              type="email"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Phone</label>
            <input
              v-model="trainerForm.phone"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Address</label>
            <input
              v-model="trainerForm.address"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Joining Date</label>
            <input
              v-model="trainerForm.joiningDate"
              type="date"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Salary</label>
            <input
              v-model="trainerForm.salary"
              type="number"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="flex justify-end">
            <button
              @click="closeModal"
              type="button"
              class="bg-gray-500 text-white px-4 py-2 rounded-md mr-2"
            >
              Cancel
            </button>
            <button
              type="submit"
              class="bg-blue-500 text-white px-4 py-2 rounded-md"
            >
              {{ isEditing ? "Update" : "Add" }}
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
  name: "TrainersView",
  setup() {
    interface Trainer {
      id: number;
      name: string;
      email: string;
      phone: string;
      address: string;
      joiningDate: string;
      salary: number;
    }

    const trainers = ref<Trainer[]>([]);
    const isModalOpen = ref(false);
    const isEditing = ref(false);
    const trainerForm = ref<Trainer>({
      id: 0,
      name: "",
      email: "",
      phone: "",
      address: "",
      joiningDate: "",
      salary: 0,
    });

    const fetchTrainers = async () => {
      const response = await axios.get("/trainers");
      trainers.value = response.data;
    };

    const openModal = () => {
      isModalOpen.value = true;
      isEditing.value = false;
      trainerForm.value = {
        id: 0,
        name: "",
        email: "",
        phone: "",
        address: "",
        joiningDate: "",
        salary: 0,
      };
    };

    const closeModal = () => {
      isModalOpen.value = false;
    };

    const addTrainer = async () => {
      await axios.post("/trainers", trainerForm.value);
      await fetchTrainers();
      closeModal();
    };

    const editTrainer = (trainer: Trainer) => {
      isModalOpen.value = true;
      isEditing.value = true;
      trainerForm.value = { ...trainer };
    };

    const updateTrainer = async () => {
      await axios.put(
        `/api/trainers/${trainerForm.value.id}`,
        trainerForm.value
      );
      await fetchTrainers();
      closeModal();
    };

    const deleteTrainer = async (id: number) => {
      await axios.delete(`/api/trainers/${id}`);
      await fetchTrainers();
    };

    onMounted(fetchTrainers);

    return {
      trainers,
      isModalOpen,
      isEditing,
      trainerForm,
      openModal,
      closeModal,
      addTrainer,
      editTrainer,
      updateTrainer,
      deleteTrainer,
    };
  },
});
</script>

<style scoped>
/* Add any scoped styles here */
</style>

<template>
  <div class="w-full mx-auto">
    <div class="mb-4 mt-2 ml-2">
      <button
        @click="openModal"
        class="bg-blue-500 text-white px-4 py-2 rounded-md mb-4"
      >
        Add Trainer
      </button>
      <button
        @click="openExportModal"
        class="bg-green-500 text-white px-4 py-2 rounded-md mb-4 ml-2"
      >
        Export
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
          <td class="py-2 px-4 border-b">${{ trainer.salary }}</td>
          <td class="py-2 px-4 border-b">
            <div class="flex space-x-2">
              <button
                @click="editTrainer(trainer)"
                class="flex-1 bg-yellow-500 text-white px-4 py-2 rounded-md"
              >
                Edit
              </button>
              <button
                @click="confirmDeleteTrainer(trainer.id)"
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

    <!-- Export Modal -->
    <div
      v-if="isExportModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md w-1/2">
        <h2 class="text-xl font-bold mb-4">Export Trainers</h2>
        <p>Do you want to export all the current trainers?</p>
        <div class="flex justify-end mt-4">
          <button
            type="button"
            @click="closeExportModal"
            class="bg-gray-500 text-white px-4 py-2 rounded-md mr-2 hover:bg-gray-400"
          >
            Cancel
          </button>
          <button
            type="button"
            @click="exportTrainers"
            class="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-400"
          >
            OK
          </button>
        </div>
      </div>
    </div>

    <!-- Delete Confirmation Modal -->
    <div
      v-if="isDeleteModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md w-1/2">
        <h2 class="text-xl font-bold mb-4">Delete Trainer</h2>
        <p>Are you sure you want to delete this trainer?</p>
        <div class="flex justify-end mt-4">
          <button
            type="button"
            @click="closeDeleteModal"
            class="bg-gray-500 text-white px-4 py-2 rounded-md mr-2 hover:bg-gray-400"
          >
            Cancel
          </button>
          <button
            type="button"
            @click="deleteTrainer"
            class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-400"
          >
            Delete
          </button>
        </div>
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

    const isDeleteModalOpen = ref(false);
    const trainerIdToDelete = ref<number | null>(null);

    const confirmDeleteTrainer = (id: number) => {
      trainerIdToDelete.value = id;
      isDeleteModalOpen.value = true;
    };

    const closeDeleteModal = () => {
      isDeleteModalOpen.value = false;
      trainerIdToDelete.value = null;
    };

    const deleteTrainer = async () => {
      if (trainerIdToDelete.value !== null) {
        await axios.delete(`/trainers/${trainerIdToDelete.value}`);
        await fetchTrainers();
        closeDeleteModal();
      }
    };

    const isExportModalOpen = ref(false);

    const openExportModal = () => {
      isExportModalOpen.value = true;
    };

    const closeExportModal = () => {
      isExportModalOpen.value = false;
    };

    const exportTrainers = () => {
      const csvContent =
        "data:text/csv;charset=utf-8," +
        trainers.value
          .map(
            (trainer) =>
              `${trainer.id},${trainer.name},${trainer.email},${trainer.phone},${trainer.address},${trainer.joiningDate},${trainer.salary}`
          )
          .join("\n");

      const encodedUri = encodeURI(csvContent);
      const link = document.createElement("a");
      link.setAttribute("href", encodedUri);
      link.setAttribute("download", "trainers.csv");
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      closeExportModal();
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
      confirmDeleteTrainer,
      closeDeleteModal,
      deleteTrainer,
      isDeleteModalOpen,
      isExportModalOpen,
      openExportModal,
      closeExportModal,
      exportTrainers,
    };
  },
});
</script>

<style scoped>
/* Add any scoped styles here */
</style>

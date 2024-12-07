<template>
  <div class="container mx-auto">
    <h1 class="text-2xl font-bold mt-2">Members</h1>
    <div class="mb-4">
      <button
        @click="openModal"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
      >
        Add Member
      </button>
    </div>
    <table class="min-w-full bg-white">
      <thead>
        <tr>
          <th class="py-2 px-4 border-b text-left">Name</th>
          <th class="py-2 px-4 border-b text-left">Email</th>
          <th class="py-2 px-4 border-b text-left">Phone</th>
          <th class="py-2 px-4 border-b text-left">Address</th>
          <th class="py-2 px-4 border-b text-left">Membership Start Date</th>
          <th class="py-2 px-4 border-b text-left">Membership End Date</th>
          <th class="py-2 px-4 border-b text-left">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="member in members" :key="member.id">
          <td class="py-2 px-4 border-b text-left">{{ member.name }}</td>
          <td class="py-2 px-4 border-b text-left">{{ member.email }}</td>
          <td class="py-2 px-4 border-b text-left">{{ member.phone }}</td>
          <td class="py-2 px-4 border-b text-left">{{ member.address }}</td>
          <td class="py-2 px-4 border-b text-left">
            {{ new Date(member.membershipStartDate).toLocaleDateString() }}
          </td>
          <td class="py-2 px-4 border-b text-left">
            {{ new Date(member.membershipEndDate).toLocaleDateString() }}
          </td>
          <td class="py-2 px-4 border-b text-left">
            <button
              @click="removeMember(member.id)"
              class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600"
            >
              Remove
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal -->
    <div
      v-if="isModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md">
        <h2 class="text-xl font-bold mb-4">Add New Member</h2>
        <form @submit.prevent="submitForm">
          <div class="mb-4">
            <label class="block text-gray-700">Name</label>
            <input
              v-model="newMember.name"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Email</label>
            <input
              v-model="newMember.email"
              type="email"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Phone</label>
            <input
              v-model="newMember.phone"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Address</label>
            <input
              v-model="newMember.address"
              type="text"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Membership Start Date</label>
            <input
              v-model="newMember.membershipStartDate"
              type="date"
              class="w-full px-4 py-2 border rounded-md"
              required
            />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Membership End Date</label>
            <input
              v-model="newMember.membershipEndDate"
              type="date"
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
              Add Member
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
  name: "MembersView",
  setup() {
    interface Member {
      id: number;
      name: string;
      email: string;
      phone: string;
      address: string;
      membershipStartDate: string;
      membershipEndDate: string;
    }
    const members = ref<Member[]>([]);
    const isModalOpen = ref(false);
    const newMember = ref({
      name: "",
      email: "",
      phone: "",
      address: "",
      membershipStartDate: "",
      membershipEndDate: "",
    });
    const fetchMembers = async () => {
      try {
        const response = await axios.get("/members");
        members.value = response.data;
      } catch (error) {
        console.error("Error fetching members:", error);
      }
    };
    const addMember = async () => {
      try {
        const response = await axios.post("/members", newMember.value);
        members.value.push(response.data);
        closeModal();
      } catch (error) {
        console.error("Error adding member:", error);
      }
    };
    const removeMember = async (id: number) => {
      try {
        await axios.delete(`/members/${id}`);
        members.value = members.value.filter((member) => member.id !== id);
      } catch (error) {
        console.error("Error removing member:", error);
      }
    };
    const openModal = () => {
      isModalOpen.value = true;
    };

    const closeModal = () => {
      isModalOpen.value = false;
    };

    const submitForm = () => {
      addMember();
    };

    onMounted(fetchMembers);

    return {
      members,
      isModalOpen,
      newMember,
      openModal,
      closeModal,
      submitForm,
      addMember,
      removeMember,
    };
  },
});
</script>

<template>
  <div class="w-full mx-auto">
    <div class="mb-4 mt-2 ml-2 flex items-center space-x-2">
      <button
        @click="openModal"
        class="bg-blue-500 text-white px-4 py-2 rounded-md"
      >
        Add Member
      </button>
      <button
        @click="openExportModal"
        class="bg-green-500 text-white px-4 py-2 rounded-md"
      >
        Export
      </button>
      <div class="flex-1 flex justify-start">
        <input
          v-model="searchQuery"
          type="text"
          placeholder="Search..."
          class="w-1/4 px-4 py-2 border rounded-md"
        />
      </div>
    </div>
    <table class="min-w-full bg-white">
      <thead>
        <tr>
          <th class="py-2 px-4 border-b text-left">ID</th>
          <th class="py-2 px-4 border-b text-left">Name</th>
          <th class="py-2 px-4 border-b text-left">Email</th>
          <th class="py-2 px-4 border-b text-left">Phone</th>
          <th class="py-2 px-4 border-b text-left">Address</th>
          <th class="py-2 px-4 border-b text-left">Membership Start Date</th>
          <th class="py-2 px-4 border-b text-left">Membership End Date</th>
          <th class="py-2 px-4 border-b text-left">Subscription Plan</th>
          <th class="py-2 px-4 border-b text-left">Payment Status</th>
          <th class="py-2 px-4 border-b text-left">Status</th>
          <th class="py-2 px-4 border-b text-left">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="member in filteredMembers"
          :key="member.id"
          :class="{ 'line-through opacity-50': !member.isActive }"
        >
          <td class="py-2 px-4 border-b text-left">{{ member.id }}</td>
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
            {{ member.subscriptionPlan }}
          </td>
          <td
            class="py-2 px-4 border-b text-left"
            :class="{
              'text-green-500': member.paymentStatus === 'Up to date',
              'text-yellow-500': member.paymentStatus === 'Pending',
              'text-red-500': member.paymentStatus === 'Expired',
            }"
          >
            {{ member.paymentStatus }}
          </td>
          <td class="py-2 px-4 border-b text-left">
            {{ member.isActive ? "Active" : "Deactive" }}
          </td>
          <td class="py-2 px-4 border-b">
            <div class="flex space-x-2">
              <button
                @click="editMember(member)"
                class="flex-1 bg-yellow-500 text-white px-4 py-2 rounded-md"
              >
                Edit
              </button>
              <button
                @click="confirmDeleteMember(member.id)"
                class="flex-1 bg-red-500 text-white px-4 py-2 rounded-md"
              >
                Delete
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal -->
    <div
      v-if="isModalOpen"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50"
    >
      <div class="bg-white p-6 rounded-md w-3/4">
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
            <label class="block text-gray-700">Subscription Plan</label>
            <select
              v-model="newMember.subscriptionPlan"
              class="w-full px-4 py-2 border rounded-md bg-white"
              required
            >
              <option value="Monthly">Monthly</option>
              <option value="Trimestral">Trimestral</option>
              <option value="Semi-annual">Semi-annual</option>
              <option value="Annual">Annual</option>
            </select>
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
          <div class="mb-4">
            <label class="block text-gray-700">Payment Status</label>
            <select
              v-model="newMember.paymentStatus"
              class="w-full px-4 py-2 border rounded-md bg-white"
              required
            >
              <option value="Em dia">Em dia</option>
              <option value="Pendente">Pendente</option>
              <option value="Expirado">Expirado</option>
            </select>
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Status</label>
            <select
              v-model="newMember.isActive"
              class="w-full px-4 py-2 border rounded-md bg-white"
              required
            >
              <option :value="true">Active</option>
              <option :value="false">Deactive</option>
            </select>
          </div>
          <div class="flex justify-end">
            <button
              type="button"
              @click="closeModal"
              class="bg-gray-500 text-white px-4 py-2 rounded-md mr-2 hover:bg-gray-400"
            >
              Cancel
            </button>
            <button
              type="submit"
              class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-400"
            >
              Add Member
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
        <h2 class="text-xl font-bold mb-4">Export Users</h2>
        <p>Do you want to export all the current users?</p>
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
            @click="exportUsers"
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
        <h2 class="text-xl font-bold mb-4">Delete Member</h2>
        <p>Are you sure you want to delete this member?</p>
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
            @click="deleteMember"
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
import { defineComponent, ref, computed, onMounted } from "vue";
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
      subscriptionPlan: string;
      membershipStartDate: string;
      membershipEndDate: string;
      paymentStatus: string;
      isActive: boolean;
    }
    const members = ref<Member[]>([]);
    const isModalOpen = ref(false);
    const isExportModalOpen = ref(false);
    const isDeleteModalOpen = ref(false);
    const memberIdToDelete = ref<number | null>(null);
    const newMember = ref({
      id: 0,
      name: "",
      email: "",
      phone: "",
      address: "",
      subscriptionPlan: "Monthly",
      membershipStartDate: "",
      membershipEndDate: "",
      paymentStatus: "Pendente",
      isActive: true,
    });
    const searchQuery = ref("");
    const filteredMembers = computed(() => {
      return members.value.filter((member) => {
        return Object.values(member).some((value) =>
          String(value).toLowerCase().includes(searchQuery.value.toLowerCase())
        );
      });
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

    const openExportModal = () => {
      isExportModalOpen.value = true;
    };

    const closeExportModal = () => {
      isExportModalOpen.value = false;
    };

    const exportUsers = () => {
      const csvContent =
        "data:text/csv;charset=utf-8," +
        members.value
          .map(
            (member) =>
              `${member.id},${member.name},${member.email},${member.phone},${member.address},${member.subscriptionPlan},${member.membershipStartDate},${member.membershipEndDate},${member.paymentStatus},${member.isActive}`
          )
          .join("\n");

      const encodedUri = encodeURI(csvContent);
      const link = document.createElement("a");
      link.setAttribute("href", encodedUri);
      link.setAttribute("download", "members.csv");
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      closeExportModal();
    };

    const submitForm = () => {
      addMember();
    };

    const confirmDeleteMember = (id: number) => {
      memberIdToDelete.value = id;
      isDeleteModalOpen.value = true;
    };

    const closeDeleteModal = () => {
      isDeleteModalOpen.value = false;
      memberIdToDelete.value = null;
    };

    const deleteMember = async () => {
      if (memberIdToDelete.value !== null) {
        await removeMember(memberIdToDelete.value);
        closeDeleteModal();
      }
    };

    const editMember = (member: Member) => {
      openModal();
      Object.assign(newMember.value, member);
    };

    onMounted(fetchMembers);

    return {
      members,
      isModalOpen,
      isExportModalOpen,
      isDeleteModalOpen,
      newMember,
      openModal,
      closeModal,
      openExportModal,
      closeExportModal,
      submitForm,
      addMember,
      removeMember,
      exportUsers,
      confirmDeleteMember,
      closeDeleteModal,
      deleteMember,
      editMember,
      searchQuery,
      filteredMembers,
    };
  },
});
</script>

<template> <!--TODO: Missing UI -->
  <div class="flex flex-col gap-2 h-full items-center justify-center">
    <form
      class="ring-1 ring-gray-300 shadow-lg rounded-lg p-5 flex flex-col gap-4"
      @submit.prevent="register"
    >
      <div class="flex flex-col">
        <label for="name" class="mr-2 flex flex-row gap-1"
          ><p class="text-red-600">*</p>
          Username</label
        >
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="username"
          name="name"
          type="text"
          placeholder="johndoe"
          required
        />
      </div>

      <div class="flex flex-col">
        <label for="password" class="mr-2 flex flex-row gap-1"
          ><p class="text-red-600">*</p>
          Password</label
        >
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="password"
          name="password"
          type="password"
          placeholder="●●●●●●●●"
          required
        />
      </div>

      <div class="flex flex-col">
        <label for="email" class="mr-2 flex flex-row gap-1"
          ><p class="text-red-600">*</p>
          Email</label
        >
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="email"
          name="email"
          type="email"
          placeholder="john.doe@company.com"
          required
        />
      </div>

      <div class="flex flex-col">
        <label for="phone" class="mr-2">Phone Number</label>
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="phone"
          name="phone"
          placeholder="+96612345678"
        />
      </div>

      <div class="flex justify-center mt-4">
        <button
          type="submit"
          name="button"
          class="bg-secondary/50 hover:bg-tertiary hover:text-white active:scale-110 transition-all duration-100 w-fit py-1 px-4 shadow-md ring-1 ring-gray-400 rounded-lg"
        >
          Register
        </button>
      </div>
    </form>
    <div class="text-gray-700">
      Already have an account? Login
      <router-link class="underline text-blue-500" :to="{ name: 'LoginView' }"
        >here</router-link
      >
    </div>
  </div>
</template>

<script>
import { createToaster } from "@meforma/vue-toaster";

export default {
  name: "RegisterView",
  data() {
    return {
      username: "",
      password: "",
      email: "",
      phone: "",
    };
  },
  methods: {
    register() {
      // FIXME: Check the backend's possible errors list before sending the request for a snappier UX.
      this.$store
        .dispatch("register", {
          username: this.username,
          password: this.password,
          email: this.email,
          phone: this.phone,
        })
        .then(() => {
          this.$router.push({ name: "DashboardView" });
        })
        .catch((error) => {
          const toaster = createToaster({
            position: "top",
            duration: 2000,
            type: "error",
          });
          toaster.show(error.message);
        });
    },
  },
};
</script>

<style></style>

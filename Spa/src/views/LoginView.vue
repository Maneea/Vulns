<template> <!--TODO: Missing UI -->
  <div class="flex flex-col gap-2 h-full items-center justify-center">
    <form
      class="ring-1 ring-gray-300 bg-primary shadow-lg rounded-lg p-5 flex flex-col gap-4"
      @submit.prevent="login"
    >
      <div>
        <label for="name" class="mr-2 flex flex-row gap-1">Username</label>
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="username"
          name="name"
          type="text"
          placeholder="johndoe"
          required
        />
      </div>

      <div>
        <label for="password" class="mr-2 flex flex-row gap-1">Password</label>
        <input
          class="px-2 py-1 rounded-md ring-1 ring-gray-300"
          v-model="password"
          name="password"
          type="password"
          placeholder="●●●●●●●●"
          required
        />
      </div>

      <div class="flex justify-center mt-4">
        <button
          type="submit"
          name="button"
          class="bg-secondary/50 hover:bg-tertiary hover:text-white active:scale-110 transition-all duration-100 w-fit py-1 px-4 shadow-md ring-1 ring-gray-400 rounded-lg"
        >
          Login
        </button>
      </div>
    </form>
    <div class="text-gray-700">
        Don't have an account? Create one <router-link class="underline text-blue-500" :to='{name: "RegisterView"}'>here</router-link>
    </div>
  </div>
</template>

<script>
import { createToaster } from "@meforma/vue-toaster";
export default {
  name: "LoginView",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    login() {
      this.$store
        .dispatch("login", {
          username: this.username,
          password: this.password,
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

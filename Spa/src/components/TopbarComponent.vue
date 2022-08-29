<template>
  <div class="flex flex-row w-screen shadow-md z-10 fixed">
    <div class="pl-32 w-full h-[70px] bg-gradient-to-r from-primary via-secondary to-secondary">
      <div class="flex flex-row justify-end gap-32 h-full items-center">
        <div class="w-2/3">
          <SearchbarComponent />
        </div>

        <div
          class="pr-8 pl-8 flex flex-row justify-center items-center gap-3 h-full transition-all"
        >
          <div>
            <div class="text-xl">{{ userName }}</div>
            <div class="text-sm text-gray-500">{{ userRole }}</div>
          </div>

          <div>
            <img
              class="my-1 w-12 h-12 shadow-2x"
              src="@/assets/profile-icon.png"
              alt="Bonnie image"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="mb-[70px]"></div>
</template>

<script>
import SearchbarComponent from "./SearchbarComponent.vue";
import jwt_decode from "jwt-decode";

export default {
  name: "TopbarComponent",
  components: {
    SearchbarComponent,
  },
  data() {
    return {
      userInput: "",
    };
  },
  computed: {
    userName() {
      // Optimistic code. No validation or error handling
      const user = localStorage.getItem("user");
      const userData = jwt_decode(JSON.parse(user).token);
      return userData[
        Object.keys(userData).filter((key) => key.endsWith("name"))
      ];
    },
    userRole() {
      // Optimistic code. No validation or error handling
      const user = localStorage.getItem("user");
      const userData = jwt_decode(JSON.parse(user).token);
      return userData[
        Object.keys(userData).filter((key) => key.endsWith("role"))
      ].includes("Admin")
        ? "Admin"
        : "User";
    },
  },
};
</script>

<style></style>

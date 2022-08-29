<template> <!--TODO: Missing UI -->
<div class="flex flex-col items-center">
  <div class="text-2xl mt-4">Landing Page</div>
      <div class="flex flex-row justify-center w-full h-full">
        <div>
          <router-link :to="{ name: 'DashboardView' }">
            <button
              class="m-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200"
            >
              <span
                class="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white rounded-md group-hover:bg-opacity-0"
              >
                Dashboard
              </span>
            </button>
          </router-link>
  
          <router-link :to="{ name: 'LoginView' }">
            <button
              :class="'m-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 ' + ((this.$store.state.user !== null) ? 'hidden' : 'block')"
            >
              <span
                class="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white rounded-md group-hover:bg-opacity-0"
              >
                Login
              </span>
            </button>
          </router-link>
  
          <router-link :to="{ name: 'RegisterView' }">
            <button
              :class="'m-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 ' + ((this.$store.state.user !== null) ? 'hidden' : 'block')"
            >
              <span
                class="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white rounded-md group-hover:bg-opacity-0"
              >
                Register
              </span>
            </button>
          </router-link>

          <button
          @click="logout"
              :class="'m-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-red-500 to-red-500 group-hover:from-red-500 group-hover:to-red-500 hover:text-white focus:ring-4 focus:outline-none focus:ring-red-200 ' + ((this.$store.state.user !== null) ? 'block' : 'hidden')"
            >
              <span
                class="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white rounded-md group-hover:bg-opacity-0"
              >
                Logout
              </span>
            </button>
  
        </div>
      </div>
</div>
</template>

<script>
export default {
  name: "LandingView",
  methods : {
    logout() {
      this.$store.dispatch('logout')
    }
  }
};
</script>

<style>

</style>

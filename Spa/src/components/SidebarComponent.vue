<template>
  <div class="bg-red-500 w-20"></div>
  <!-- This line is to preserve the layout and should not be viewed by the user, since the display of the sidebar is `fixed` -->
  <div
    class="shadow-md group fixed bg-gradient-to-b from-primary via-secondary to-secondary left-0 top-0 w-20 border-r-2 h-full hover:w-72 transition-all duration-300 z-[10] flex flex-col"
    @mouseover="extendedSidebar"
    @mouseout="compressedSidebar"
  >
    <router-link
      :to="{ name: 'LandingView' }"
      class="flex flex-row w-full content-center items-center"
    >
      <div class="logo">
        <img src="@/assets/logo.png" />
      </div>

      <div class="scale-x-0 w-0 group-hover:scale-x-100 duration-300">
        <p
          class="text-3xl mt-2 text-tertiary opacity-0 -translate-x-5 group-hover:opacity-100 group-hover:translate-x-0 duration-150 transition-all font-mono font-bold"
        >
          VulnView
        </p>
      </div>
    </router-link>

    <div class="">
      <div class="seperator"></div>
    </div>

    <router-link :to="{ name: 'DashboardView' }" class="router-option bg-transparent">
      <div class="selected"></div>
      <view-dashboard-outline-icon :size="32" />
      <p>Dashboard</p>
    </router-link>

    <router-link :to="{ name: 'SearchView' }" class="router-option bg-transparent">
      <div class="selected"></div>
      <magnify-icon :size="32" />
      <p>Search</p>
    </router-link>

    <router-link :to="{ name: 'StatisticsView' }" class="router-option bg-transparent">
      <div class="selected"></div>
      <chart-line-icon :size="32" />
      <p>Statistics</p>
    </router-link>

    <div class="flex flex-col justify-end h-full w-full mb-4">
      <div class="router-option bg-transparent" @click="logout">
        <logout-variant-icon :size="32" />
        <p>Logout</p>
      </div>
    </div>
  </div>
</template>

<script>
import ViewDashboardOutlineIcon from "@/../node_modules/vue-material-design-icons/ViewDashboardOutline.vue";
import MagnifyIcon from "@/../node_modules/vue-material-design-icons/Magnify.vue";
import ChartLineIcon from "@/../node_modules/vue-material-design-icons/ChartLine.vue";
import EyeOutlineIcon from "@/../node_modules/vue-material-design-icons/EyeOutline.vue";
import LogoutVariantIcon from "@/../node_modules/vue-material-design-icons/LogoutVariant.vue";
import CogOutlineIcon from "@/../node_modules/vue-material-design-icons/CogOutline.vue";

export default {
  name: "SidebarComponent",
  components: {
    ViewDashboardOutlineIcon,
    MagnifyIcon,
    ChartLineIcon,
    EyeOutlineIcon,
    LogoutVariantIcon,
    CogOutlineIcon,
  },
  methods: {
    extendedSidebar() {
      // May be used later
    },
    compressedSidebar() {
      // May be used later
    },
    logout() {
      this.$store.dispatch("logout");
    },
  },
};
</script>

<style>
.logo {
  @apply w-12 mt-2 mb-[8px] ml-[15px] mr-3;
}

.seperator {
  @apply m-2 mt-0.5 w-5/6 h-0.5 bg-gray-300 mb-4 group-hover:w-11/12 transition-all;
}

.router-option {
  @apply bg-white w-full flex flex-row items-center pl-[22px] gap-4 py-4 hover:bg-quaternary/75 hover:text-white active:bg-quaternary p-2 cursor-pointer;
}

.router-option:hover p {
  @apply text-white;
}

.router-option p {
  @apply text-[18px] text-[#303030] w-0 scale-0 group-hover:w-fit group-hover:scale-100 duration-300 transition-transform font-light select-none;
}

.selected {
  @apply absolute left-0 w-1 h-12 rounded-l rounded-full bg-tertiary transition-all scale-y-0 -translate-x-4 duration-300 opacity-0;
}

.router-link-active .selected {
  @apply visible scale-y-100 translate-x-0 opacity-100;
}
</style>

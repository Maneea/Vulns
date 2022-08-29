<template>
  <div class="flex flex-col">
    <div
      class="text-3xl text-primary px-4 pt-4 pb-2 font-bold shadow-lg w-full bg-quaternary justify-between"
    >
      Dashboard
    </div>
    <DashboardGridComponentVue
      :userWidgets="userWidgets"
      :editMode="editMode"
      @save-widgets="displaySaving"
    />
  </div>
  <!-- <div class="w-96 h-96"><ChartLoaderComponentVue/></div> -->

  <button
    v-if="!currentlySaving"
    :title="editMode ? 'Save Dashboard' : 'Edit Dashboard'"
    class="absolute flex flex-row justify-center items-center content-center bottom-4 right-4 m-4 bg-secondary/50 w-16 h-16 px-2 py-1 hover:bg-tertiary/50 hover:text-primary ring-1 ring-quaternary/50 rounded-full shadow-md active:scale-110 transition-all"
    @click="toggleEditMode"
  >
    <Pencil v-if="!editMode" />
    <ContentSave v-else />
  </button>
  <button
    v-else
    class="absolute bottom-4 right-4 m-4 flex items-center content-center justify-center pl-4 bg-tertiary/50 w-16 h-16 px-2 py-1 ring-1 ring-gray-400 rounded-full shadow-md cursor-not-allowed"
    disabled
  >
    <SpinnerComponent />
  </button>
</template>

<script>
import DashboardGridComponentVue from "@/components/Dashboard/DashboardGridComponent.vue";
import AccountService from "@/services/AccountService";
import SpinnerComponent from "@/components/Spinner.vue";
import Pencil from "@/../node_modules/vue-material-design-icons/Pencil.vue";
import ContentSave from "@/../node_modules/vue-material-design-icons/ContentSave.vue";
import ChartLoaderComponentVue from "../components/Dashboard/ChartLoaderComponent.vue";
import defaultWidgets from "@/components/Dashboard/DefaultUserWidgets";

export default {
  name: "DashboardView",
  components: {
    DashboardGridComponentVue,
    SpinnerComponent,
    Pencil,
    ContentSave,
    ChartLoaderComponentVue,
  },
  created() {
    let vm = this;
    if (!this.$store.state.dashboard) {
      AccountService.getWidgets()
        .then((widgets) => {
          for (let i = widgets.length - 1; i >= 0; i--) {
            if (widgets[i].h == 0 && widgets[i].w == 0) {
              widgets.splice(i, 1);
            }
          }
          vm.userWidgets = widgets;
          if (vm.userWidgets) this.$store.dispatch("setDashboard", widgets);
          console.log(vm.userWidgets)
          if (vm.userWidgets) {
            if (vm.userWidgets.length === 0)
              vm.userWidgets = defaultWidgets;
              vm.$store.dispatch("setDashboard", vm.userWidgets);
          }
        })
        .catch((error) => {
          return;
        });
    } else {
      this.userWidgets = this.$store.state.dashboard;
    }
  },
  data() {
    return {
      editMode: false,
      userWidgets: null,
      currentlySaving: false,
    };
  },
  methods: {
    toggleEditMode() {
      this.editMode = !this.editMode;
    },
    displaySaving(value) {
      this.currentlySaving = value;
    },
  },
};
</script>

<style>
/* body {
  height: 50%;
} */
</style>

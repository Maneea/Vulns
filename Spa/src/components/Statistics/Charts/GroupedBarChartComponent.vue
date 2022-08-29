<template>
  <div class="flex flex-col h-full w-full">
    <div
      v-if="!this.hideTop"
      class="bg-quaternary p-2 flex flex-row items-center justify-between w-full"
    >
      <div />
      <p class="font-bold text-primary text-3xl">Area Chart</p>
      <button
        v-if="this.payload && !this.hasAddedToDashboard"
        @click="addToDashboard"
        title="Add to dashboard"
        class="ring-black flex gap-4 pr-4 flex-row items-center w-fit ring-2 shadow-lg p-2 bg-tertiary/40 hover:bg-secondary hover:text-quaternary active:scale-105 transition-all select-none cursor-point shadow-gray-500 text-primary font-mono font-bold rounded-md"
      >
        <HeartIcon />
        <p>Add to Dashboard</p>
      </button>
      <div v-else />
    </div>
    <VueApexCharts
      v-if="chartOptions && series"
      width="100%"
      class="p-2 bg-gradient-to-t from-white via-primary to-secondary/50"
      height="100%"
      type="bar"
      :options="chartOptions"
      :series="series"
    />
  </div>
</template>
<script>
import VueApexCharts from "vue3-apexcharts";
import HeartIcon from "@/../node_modules/vue-material-design-icons/Heart.vue";
import AccountService from "@/services/AccountService";
import { createToaster } from "@meforma/vue-toaster";

export default {
  name: "GroupedBarChartComponent",
  components: {
    VueApexCharts,
    HeartIcon,
  },
  props: {
    data: {
      default: null,
    },
    features: {
      default: [],
    },
    payload: {
      required: false,
    },
    aggregationLength: {
      required: false,
    },
    hideTop: {
      default: false,
    },
  },
  data() {
    return {
      chartOptions: null,
      series: null,
      hasAddedToDashboard: false,
    };
  },
  created() {
    let vm = this;
    vm.chartOptions = null;
    vm.series = null;
    if (Object.getOwnPropertyNames(vm.data[0]).length == 3) {
      vm.chartOptions = {
        xaxis: {
          title: {
            text: vm.features[0] + " / " + vm.features[1],
          },
          categories: [],
          group: {
            style: {
              fontSize: "10px",
              fontWeight: 700,
            },
            groups: [
              // { title: "2019", cols: 4 },
              // { title: "2020", cols: 4 },
            ],
          },
        },
        yaxis: {
          title: {
            text: vm.features[2],
          },
        },
      };
      vm.series = [
        {
          name: vm.features[1],
          data: [],
        },
      ];
      for (let i = 0; i != this.data.length; i++) {
        vm.series[0].data.push(this.data[i].feature3);
        vm.chartOptions.xaxis.categories.push(this.data[i].feature2);
        if (
          !vm.chartOptions.xaxis.group.groups
            .map((value) => value.title)
            .includes(vm.data[i].feature0)
        ) {
          vm.chartOptions.xaxis.group.groups.push({
            title: vm.data[i].feature0,
            cols: this.data.filter((value) => {
              if (vm.data[i].feature0 === value.feature0) return value;
            }).length,
          });
        }
      }
    }
  },
  methods: {
    async addToDashboard() {
      this.hasAddedToDashboard = true;
      let currentDashboard = null;
      let vm = this;
      if (!this.$store.state.dashboard) {
        await AccountService.getWidgets()
          .then((widgets) => {
            this.$store.dispatch("setDashboard", widgets);
            vm.currentDashboard = widgets;
          })
          .catch((error) => {
            return;
          });
      }
      if (!currentDashboard)
        currentDashboard = JSON.parse(
          JSON.stringify(this.$store.state.dashboard)
        );
      let highestIndex = 0;
      for (let widget of currentDashboard) {
        if (+widget.i >= highestIndex) highestIndex = +widget.i + 1;
      }
      currentDashboard.push({
        x: 0,
        y: 0,
        w: 6,
        h: 3,
        minW: 3,
        minH: 3,
        i: +(highestIndex + ""),
        component: "ChartLoaderComponent",
        features: JSON.parse(JSON.stringify(vm.features)),
        payload: JSON.parse(JSON.stringify(vm.payload)),
        chartComponent: "GroupedBarChartComponent",
        title: "Grouped Bar Chart",
        aggregationLength: JSON.parse(JSON.stringify(vm.aggregationLength)),
      });
      AccountService.setWidget(currentDashboard)
        .then(() => {
          vm.$store.dispatch("setDashboard", currentDashboard);
          const toaster = createToaster({
            position: "top",
            duration: 2000,
            type: "success",
          });
          toaster.show(
            `<div class="flex flex-row content-center gap-4 h-full w-full justify-between items-center"><svg style="color: white" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24" zoomAndPan="magnify" viewBox="0 0 30 30.000001" height="40" preserveAspectRatio="xMidYMid meet" version="1.0"><defs><clipPath id="id1"><path d="M 2.328125 4.222656 L 27.734375 4.222656 L 27.734375 24.542969 L 2.328125 24.542969 Z M 2.328125 4.222656 " clip-rule="nonzero" fill="white"></path></clipPath></defs><g clip-path="url(#id1)"><path fill="white" d="M 27.5 7.53125 L 24.464844 4.542969 C 24.15625 4.238281 23.65625 4.238281 23.347656 4.542969 L 11.035156 16.667969 L 6.824219 12.523438 C 6.527344 12.230469 6 12.230469 5.703125 12.523438 L 2.640625 15.539062 C 2.332031 15.84375 2.332031 16.335938 2.640625 16.640625 L 10.445312 24.324219 C 10.59375 24.472656 10.796875 24.554688 11.007812 24.554688 C 11.214844 24.554688 11.417969 24.472656 11.566406 24.324219 L 27.5 8.632812 C 27.648438 8.488281 27.734375 8.289062 27.734375 8.082031 C 27.734375 7.875 27.648438 7.679688 27.5 7.53125 Z M 27.5 7.53125 " fill-opacity="1" fill-rule="nonzero"></path></g></svg> <p class="font-bold">Widget Added Successfully!</p></div>`
          );
          vm.hasAddedToDashboard = true;
        })
        .catch((error) => {
          return;
        });
    },
  },
};
</script>
<style></style>

<template>
  <div v-if="this.userWidgets">
    <grid-layout
      :layout.sync="userWidgets"
      :col-num="16"
      :rowHeight="115"
      :is-draggable="editMode"
      :is-resizable="editMode"
      :vertical-compact="true"
      :use-css-transforms="true"
    >
      <grid-item
        v-for="item in userWidgets"
        :x="item.x"
        :y="item.y"
        :w="item.w"
        :h="item.h"
        :i="item.i"
        :minW="item.minW ? item.minW : 1"
        :minH="item.minH ? item.minH : 1"
        :maxW="item.maxW ? item.maxW : Infinity"
        :maxH="item.maxH ? item.maxH : Infinity"
        :preserveAspectRatio="
          item.preserveAspectRatio ? item.preserveAspectRatio : false
        "
        :class="
          this.editMode
            ? 'active-animation overflow-hidden p-[1px]'
            : 'overflow-hidden'
        "
      >
        <div
          v-if="editMode"
          class="absolute w-6 h-6 z-10 bg-red-400 ring-1 shadow-md hover:bg-red-300 hover:ring-red-400 hover:shadow-red-300 transition-transform active:scale-110 shadow-red-400 ring-red-500 cursor-pointer flex items-center justify-center rounded-sm top-1 right-1"
          @click="deleteWidget(item)"
        >
          <CloseIcon fillColor="#800000" :size="16" />
        </div>
        <div v-if="item.component" class="w-full h-full">
          <div

            class="absolute h-full w-full flex justify-center content-center items-center -z-10 select-none"
          >
            <SpinnerComponent />
          </div>

          <div class="flex flex-col h-full">
            <div
              class="bg-black py-2 px-2 w-full h-fit text-center text-primary text-bold flex items-center justify-center text-lg"
            >
              <p v-if="!this.editMode" class="font-bold">{{item.title}}</p>
              <input v-else v-model.lazy="item.title" class="bg-primary-800 text-center text-quaternary ring-1 rounded-lg w-full ring-primary"/>
            </div>
            <div class="h-full">
              <component
                :is="item.component"
                class="bg-white h-full w-full"
                :payload="item.payload"
                :features = "item.features"
                :chartComponent="item.chartComponent"
                :aggregationLength="item.aggregationLength"
                @handleClick="setFetchingId"
                :searchPage="item.searchPage"
              />
            </div>
          </div>
        </div>
        <div v-else>
          <div
            class="absolute h-full w-full flex justify-center content-center items-center -z-10 font-mono text-red-600 select-none"
          >
            No inner component provided!
            <!-- This should never be displayed to the user -->
          </div>
        </div>
      </grid-item>
    </grid-layout>
  </div>
  <div
    v-else
    class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2"
  >
    <SpinnerComponent />
  </div>
  <ModalComponentVue
    contentComponent="VulnModalContent"
    :fetchingId="this.fetchingId"
    @clear:fetchingId="clearFetching"
  />
</template>

<script>
import { GridLayout, GridItem } from "vue-grid-layout";
import LatestVulnComponent from "@/components/Dashboard/LatestVulnComponent.vue";
import VulnListComponent from "@/components/AdvancedSearch/VulnListComponent.vue";
import SpinnerComponent from "@/components/Spinner.vue";
import AccountService from "../../services/AccountService";
import { createToaster } from "@meforma/vue-toaster";
import CloseIcon from "@/../node_modules/vue-material-design-icons/Close.vue";
import ModalComponentVue from "@/components/Topbar/ModalComponent.vue";
import ChartLoaderComponent from "./ChartLoaderComponent.vue";

export default {
  name: "DashboardGridComponent",
  components: {
    ChartLoaderComponent,
    GridLayout,
    GridItem,
    LatestVulnComponent,
    SpinnerComponent,
    CloseIcon,
    VulnListComponent,
    ModalComponentVue
  },
  data() {
    return {
      fetchingId: null,
    };
  },
  props: {
    userWidgets: {
      type: Array,
    },
    editMode: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    editMode(newValue) {
      if (!newValue) {
        this.$emit("save-widgets", true);
        AccountService.setWidget(this.userWidgets)
          .then(() => {
            this.$emit("save-widgets", false);
            const toaster = createToaster({
              position: "top",
              duration: 2000,
              type: "success",
            });
            toaster.show(
              `<div class="flex flex-row content-center gap-4 h-full w-full justify-between items-center"><svg style="color: white" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24" zoomAndPan="magnify" viewBox="0 0 30 30.000001" height="40" preserveAspectRatio="xMidYMid meet" version="1.0"><defs><clipPath id="id1"><path d="M 2.328125 4.222656 L 27.734375 4.222656 L 27.734375 24.542969 L 2.328125 24.542969 Z M 2.328125 4.222656 " clip-rule="nonzero" fill="white"></path></clipPath></defs><g clip-path="url(#id1)"><path fill="white" d="M 27.5 7.53125 L 24.464844 4.542969 C 24.15625 4.238281 23.65625 4.238281 23.347656 4.542969 L 11.035156 16.667969 L 6.824219 12.523438 C 6.527344 12.230469 6 12.230469 5.703125 12.523438 L 2.640625 15.539062 C 2.332031 15.84375 2.332031 16.335938 2.640625 16.640625 L 10.445312 24.324219 C 10.59375 24.472656 10.796875 24.554688 11.007812 24.554688 C 11.214844 24.554688 11.417969 24.472656 11.566406 24.324219 L 27.5 8.632812 C 27.648438 8.488281 27.734375 8.289062 27.734375 8.082031 C 27.734375 7.875 27.648438 7.679688 27.5 7.53125 Z M 27.5 7.53125 " fill-opacity="1" fill-rule="nonzero"></path></g></svg> <p class="font-bold">Dashboard saved!</p></div>`
            );
            vm.$store.dispatch("setDashboard", currentDashboard);
          })
          .catch((error) => {
            this.$emit("save-widgets", false);
          });
      }
    },
  },
  methods: {
    deleteWidget(item) {
      for (let index = 0; index != this.userWidgets.length; index++) {
        if (this.userWidgets[index].i == item.i) {
          this.userWidgets[index] = {x:0, y:0, h:0, w:0, i:item.i}
          break;
        }
      }
    },
    setFetchingId(id) {
      this.fetchingId = id;
    },
    clearFetching() {
      this.fetchingId = null;
    },
  },
};
</script>

<style>
.vue-grid-layout {
  /* The grid itself */
  @apply bg-transparent;
}

.vue-grid-item:not(.vue-grid-placeholder) {
  /* The items */
  @apply ring-1 ring-gray-300 rounded-md bg-white shadow-lg;
}

.vue-grid-item .resizing {
  /* The resizing handles */
  @apply opacity-90 m-4 bg-blue-200;
}

.vue-grid-item .text {
  @apply select-none;
}

.vue-draggable-handle {
  position: absolute;
  width: 20px;
  height: 20px;
  top: 0;
  left: 0;
  background: url("data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='10' height='10'><circle cx='5' cy='5' r='5' fill='#999999'/></svg>")
    no-repeat;
  background-position: bottom right;
  padding: 0 8px 8px 0;
  background-repeat: no-repeat;
  background-origin: content-box;
  box-sizing: border-box;
  cursor: pointer;
}

.vue-grid-item.vue-grid-placeholder {
  @apply bg-tertiary rounded-md;
}

.active-animation {
  background-image: linear-gradient(90deg, #f31000, transparent 50%),
    linear-gradient(90deg, #3f72af, transparent 50%),
    linear-gradient(0deg, #3f72af, transparent 50%),
    linear-gradient(0deg, #3f72af, transparent 50%);
  background-repeat: repeat-x, repeat-x, repeat-y, repeat-y;
  background-size: 15px 2px, 15px 2px, 2px 15px, 2px 15px;
  background-position: left top, right bottom, left bottom, right top;
  animation: border-dance 1s infinite linear;
}
@keyframes border-dance {
  0% {
    background-position: left top, right bottom, left bottom, right top;
  }
  100% {
    background-position: left 15px top, right 15px bottom, left bottom 15px,
      right top 15px;
  }
}

.c-toast--success {
  background-color: #3f72af !important;
}
</style>

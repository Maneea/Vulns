<template>
  <div>
    <vue-final-modal
      v-model="showModal"
      classes="modal-container"
      content-class="modal-content"
      @closed="closed"
      @cancel="showModal = false"
    >
      <component :is="this.chartComponent" :aggregationLength="this.aggregationLength" :data="this.data" :features="this.units" :payload="this.payload"/>
    </vue-final-modal>
  </div>
</template>

<script>
import { VueFinalModal, ModalsContainer } from "vue-final-modal";
import JsonChartComponent from "@/components/Statistics/Charts/JsonChartComponent.vue"
import BarChartComponent from "@/components/Statistics/Charts/BarChartComponent.vue";
import LineChartComponent from "@/components/Statistics/Charts/LineChartComponent.vue";
import AreaChartComponent from "@/components/Statistics/Charts/AreaChartComponent.vue";
import PieChartComponent from "@/components/Statistics/Charts/PieChartComponent.vue";
import ScalerChartComponent from "@/components/Statistics/Charts/ScalerChartComponent.vue";
import GroupedBarChartComponent from "@/components/Statistics/Charts/GroupedBarChartComponent.vue";
import MultiLineChartComponent from "@/components/Statistics/Charts/MultiLineChartComponent.vue";
import MultiAreaChartComponent from "@/components/Statistics/Charts/MultiAreaChartComponent.vue";

export default {
  name: "ModalComponent",
  components: {
    VueFinalModal,
    ModalsContainer,
    JsonChartComponent,
    BarChartComponent,
    LineChartComponent,
    AreaChartComponent,
    PieChartComponent,
    ScalerChartComponent,
    GroupedBarChartComponent,
    MultiLineChartComponent,
    MultiAreaChartComponent
  },
  props: {
    chartComponent: {
      type: String,
      default: null,
    },
    data: {
      required: true,
    },
    units: {
      required: true
    },
    payload: {
      required: true
    },
    aggregationLength: {
      required: true
    }
  },
  data: () => ({
    showModal: false,
  }),
  watch: {
    chartComponent(newId) {
      if (newId !== null) {
        this.showModal = true;
      }
    },
  },
  methods: {
    closed() {
      this.$emit("clear:chartComponent", null);
    },
    propBinding() {
      return { data: this.data };
    },
  },
};
</script>

<style scoped>
:deep(.modal-container) {
  display: flex;
  justify-content: center;
  align-items: center;
  @apply h-full;
}
:deep(.modal-content) {
  display: flex;
  flex-direction: column;
  margin: 0 1rem;
  background: #fff;
  @apply h-5/6 w-10/12 shadow-2xl shadow-gray-400/90;
}
.modal__title {
  font-size: 1.5rem;
  font-weight: 700;
}
</style>

<style scoped>
.dark-mode div:deep(.modal-content) {
  border-color: #2d3748;
  background-color: #1a202c;
}
</style>

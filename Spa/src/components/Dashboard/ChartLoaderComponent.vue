<template>
  <component
    :is="this.chartComponent"
    :features="this.features"
    :data="this.cleanData"
    :hideTop="true"
    class="pb-2"
    v-if="this.displayGraph"
  />
</template>
<script>
import VulnService from "@/services/VulnService";
import BarChartComponent from "@/components/Statistics/Charts/BarChartComponent.vue";
import LineChartComponent from "@/components/Statistics/Charts/LineChartComponent.vue";
import AreaChartComponent from "@/components/Statistics/Charts/AreaChartComponent.vue";
import GroupedBarChartComponent from "@/components/Statistics/Charts/GroupedBarChartComponent.vue";
import MultiAreaChartComponent from "@/components/Statistics/Charts/MultiAreaChartComponent.vue";
import MultiLineChartComponent from "@/components/Statistics/Charts/MultiLineChartComponent.vue";
import PieChartComponent from "@/components/Statistics/Charts/PieChartComponent.vue";
import ScalerChartComponent from "@/components/Statistics/Charts/ScalerChartComponent.vue";


export default {
  name: "ChartLoaderComponent",
  components: {
    BarChartComponent,
    LineChartComponent,
    AreaChartComponent,
    GroupedBarChartComponent,
    MultiAreaChartComponent,
    MultiLineChartComponent,
    PieChartComponent,
    ScalerChartComponent
  },
  props: {
    payload: {
      required: true
    },
    features: {
      required: true,
    },
    chartComponent: {
      required: true,
    },
    aggregationLength: {
      required: true
    }
  },
  data() {
    return {
      response: null,
      cleanData: [],
      displayGraph: false
    };
  },
  created() {
    this.displayGraph = false
    let vm = this;
    VulnService.getStatistics(this.payload)
    .then((response) => {
      vm.response = response;
      
      if (response.length > 0) {
        let features = Object.getOwnPropertyNames(response[0]);
        for (let i = 0; i < response.length; i++) {
          vm.cleanData[i] = {};
          for (let j = 0; j < features.length; j++) {
            vm.cleanData[i]["feature" + j] = response[i][features[j]];
          }
        }
        // Removing second feature if conditions are met.
        if (features.length > 2 && vm.aggregationLength == 2) {
          features.splice(1, 1);
          vm.cleanData = vm.cleanData.map(
            ({ feature1, ...keepAttrs }) => keepAttrs
            );
          }
        }
        this.displayGraph = true
      })
      .catch((e) => {
        // this.loadSpinner = false;
      });
  },
};
</script>
<style></style>

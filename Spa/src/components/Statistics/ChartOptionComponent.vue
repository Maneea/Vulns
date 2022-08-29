<template>
  <div
    class="w-full h-fit overflow-hidden shadow-lg shadow-tertiary/30 hover:shadow-tertiary bg-gradient-to-l hover:from-blue-100 hover:via-white cursor-pointer select-none transition-transform active:scale-105 hover:to-blue-100 group flex flex-row from-secondary via-primary to-secondary rounded-md mt-2 mx-5 ring-1 ring-gray-400"
    @click="clickHandler"
  >
    <div
      class="h-full w-fit flex flex-col px-2 group-hover:bg-quaternary justify-center items-center bg-tertiary"
    >
      <div class="h-fit w-fit">
        <component :is="this.icon" fillColor="#DBE2EF" />
      </div>
    </div>
    <div class="flex grow flex-col items-center">
      <div class="flex flex-row justify-center gap-2">
        <p class="text-center text-lg text-quaternary font-bold">
          {{ this.title }}
        </p>
      </div>
      <div v-if="this.units">
        <div v-if="this.units.length === 1 && this.title === 'JSON'">
          <p class="text-quaternary/50 text-center mx-2">{{ this.units[0] }}</p>
        </div>
        <div v-else-if="this.units.length === 1 && this.title !== 'JSON'">
          <!-- FIXME: The content of the line below should be reconsidered  -->
          <p class="text-quaternary/50 text-center mx-2">
            Number of distinct values ({{ this.units[0].split("-")[0] }})
          </p>
        </div>
        <div
          v-else-if="
            this.units.length === 2 &&
            (this.title === 'Bar Chart' ||
              this.title === 'Line Chart' ||
              this.title === 'Area Chart')
          "
        >
          <p class="text-quaternary/50 text-center mx-2">
            X-axis: {{ this.units[0] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            Y-axis: {{ this.units[1] }}
          </p>
        </div>
        <div v-else-if="this.units.length === 2 && this.title === 'Pie Chart'">
          <p class="text-quaternary/50 text-center mx-2">
            Labels: {{ this.units[0] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            Values: {{ this.units[1] }}
          </p>
        </div>

        <div
          v-else-if="
            this.units.length === 3 && this.title === 'Grouped Bar Chart'
          "
        >
          <p class="text-quaternary/50 text-center mx-2">
            Groups: {{ this.units[0] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            X-axis: {{ this.units[1] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            Y-axis: {{ this.units[2] }}
          </p>
        </div>

        <div
          v-else-if="
            this.units.length === 3 &&
            (this.title === 'Multi-Line Chart' ||
              this.title === 'Multi-Area Chart')
          "
        >
          <p class="text-quaternary/50 text-center mx-2">
            Lines: {{ this.units[0] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            X-axis: {{ this.units[1] }}
          </p>
          <p class="text-quaternary/50 text-center mx-2">
            Y-axis: {{ this.units[2] }}
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import PollIcon from "@/../node_modules/vue-material-design-icons/Poll.vue";
import NumericIcon from "@/../node_modules/vue-material-design-icons/Numeric.vue";
import JsonIcon from "@/../node_modules/vue-material-design-icons/CodeJson.vue";
import PieIcon from "@/../node_modules/vue-material-design-icons/ChartPie.vue";
import ChartLineIcon from "@/../node_modules/vue-material-design-icons/ChartLine.vue";
import ChartAreasplineVariantIcon from "@/../node_modules/vue-material-design-icons/ChartAreasplineVariant.vue";

export default {
  name: "ChartOptionComponent",
  components: {
    PollIcon,
    NumericIcon,
    JsonIcon,
    PieIcon,
    ChartLineIcon,
    ChartAreasplineVariantIcon,
  },
  props: {
    units: {
      type: Array,
      required: true,
    },
    icon: {
      type: String,
      required: true,
    },
    title: {
      type: String,
      required: true,
    },
  },
  methods: {
    clickHandler() {
      let chosenChart = "JsonChartComponent";
      if (this.title === "Bar Chart") chosenChart = "BarChartComponent"
      else if (this.title === "Line Chart") chosenChart = "LineChartComponent"
      else if (this.title === "Area Chart") chosenChart = "AreaChartComponent"
      else if (this.title === "Pie Chart") chosenChart = "PieChartComponent"
      else if (this.title === "Scaler") chosenChart = "ScalerChartComponent"
      else if (this.title === "Grouped Bar Chart") chosenChart = "GroupedBarChartComponent"
      else if (this.title === "Grouped Bar Chart") chosenChart = "GroupedBarChartComponent"
      else if (this.title === "Multi-Line Chart") chosenChart = "MultiLineChartComponent"
      else if (this.title === "Multi-Area Chart") chosenChart = "MultiAreaChartComponent"
      
      this.$emit("update:chosenChart", chosenChart);
    },
  },
};
</script>
<style></style>

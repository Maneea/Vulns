<template>
  <div class="flex flex-col gap-2">
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class=" w-80">Confidentiality Impacts:</p>
      <impacts-search-component @update:impact="setConfidentialityImpacts" />
    </div>
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class=" w-80">Integrity Impacts:</p>
      <impacts-search-component @update:impact="setIntegrityImpacts" />
    </div>
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class=" w-80">Availability Impacts:</p>
      <impacts-search-component @update:impact="setavailabilityImpacts" />
    </div>
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class=" w-24">Severity:</p>
      <base-score-slider-component @update:score="setBaseScore" />
    </div>
    <div class="flex flex-row content-center items-center gap-4 mt-2">
      <p class="mr-3">Has Fix:</p>
      <tri-checkbox-component @update:data="setHasFix" />
    </div>
  </div>
</template>
<script>
import ImpactsSearchComponent from "@/components/AdvancedSearch/ImpactsSearchComponent.vue";
import BaseScoreSliderComponent from "./BaseScoreSliderComponent.vue";
import TriCheckboxComponent from "@/components/TriCheckboxComponent.vue";
export default {
  name: "ImpactContentCardComponent",
  components: {
    ImpactsSearchComponent,
    BaseScoreSliderComponent,
    TriCheckboxComponent,
  },
  methods: {
    setConfidentialityImpacts(impact) {
      this.$emit("update:data", { impact: impact, name: "confidentiality" });
    },
    setavailabilityImpacts(impact) {
      this.$emit("update:data", { impact: impact, name: "availability" });
    },
    setIntegrityImpacts(impact) {
      this.$emit("update:data", { impact: impact, name: "integrity" });
    },
    setBaseScore(score) {
      this.$emit("update:data", { score: score, name: "baseScore" });
    },
    setHasFix(state) {
      this.$emit("update:data", { state: state, name: "hasFix" });
    },
  },
};
</script>
<style></style>

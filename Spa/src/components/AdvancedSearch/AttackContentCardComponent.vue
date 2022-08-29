<template>
  <div class="flex flex-col gap-2">
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class="w-60">Attack Complexities:</p>
      <attack-complexities-search-component @update:attack-complexities="setAttackComplexities"></attack-complexities-search-component>
    </div>
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class="w-60">Attack Vectors:</p>
      <attack-vector-search-component @update:attack-vector="setAttackVectors"></attack-vector-search-component>
    </div>
    <div class="flex flex-row content-center items-center gap-4 mt-2">
      Has Exploit:
      <tri-checkbox-component @update:data="setHasExploit" />
    </div>
    <div class="flex flex-row content-center items-center gap-4 mt-2">
      Required User Interraction:
      <tri-checkbox-component @update:data="setHasUserInterraction" />
    </div>
  </div>
</template>
<script>
import AttackComplexitiesSearchComponent from "./AttackComplexitiesSearchComponent.vue";
import AttackVectorSearchComponent from "./AttackVectorSearchComponent.vue";
import TriCheckboxComponent from "@/components/TriCheckboxComponent.vue";
export default {
  name: "AttackContentCardComponent",
  components: {
    TriCheckboxComponent,
    AttackComplexitiesSearchComponent,
    AttackVectorSearchComponent,
  },
  methods: {
    setAttackVectors(attack) {
      this.$emit("update:data", { attack: attack, name: "vector" });
    },
    setAttackComplexities(attack) {
      this.$emit("update:data", { attack: attack, name: "complexity" });
    },
    setHasUserInterraction(state) {
      this.$emit("update:data", { state: state, name: "userInterraction" });
    },
    setHasExploit(state) {
      this.$emit("update:data", { state: state, name: "hasExploit" });
    }
  },
};
</script>
<style></style>

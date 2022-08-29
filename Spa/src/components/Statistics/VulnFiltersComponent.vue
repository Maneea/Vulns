<template>
  <div class="w-full flex flex-col rounded-t-md">
    <div
      class="text-3xl h-fit w-full bg-quaternary justify-between flex flex-row text-primary px-4 pt-4 pb-2 font-bold shadow-lg"
    >
      <p>Filters</p>
      <CloseIcon
        class="cursor-pointer active:scale-125 duration-100 transition-transform"
        @click="closeFilters"
      />
    </div>
    <div
      class="flex flex-row bg-primary mt-8 mx-8 mb-0 shadow-sm content-center items-center"
    >
      <div
        class="bg-tertiary h-fit py-[7px] w-44 px-3 text-primary font-bold flex flex-row gap-4"
      >
        <GavelIcon></GavelIcon>
        <p>ISSUERS</p>
      </div>
      <div class="grow">
        <issuers-search-component @update:issuers="setIssuers" />
      </div>
    </div>
    <div
      class="flex flex-row bg-primary m-4 mb-0 mx-8 shadow-sm content-center items-center"
    >
      <div
        class="bg-tertiary h-fit py-[7px] w-44 px-3 text-primary font-bold flex flex-row gap-4"
      >
        <ShieldRemoveOutline></ShieldRemoveOutline>
        <p>WEAKNESSES</p>
      </div>
      <div class="grow">
        <weaknesses-search-component
          placeholder="Any"
          @update:weaknesses="setWeaknesses"
        />
      </div>
    </div>
    <div class="ml-8 mr-8">
      <minimizable-card-component
        content="ProductContentCardComponent"
        title="PRODUCTS FILTERS"
        icon="AppsIcon"
        @update:data="setProduct"
      />
    </div>
    <div class="ml-8 mr-8">
      <minimizable-card-component
        content="ImpactContentCardComponent"
        title="IMPACT FILTERS"
        icon="ExclamationThickIcon"
        @update:data="setImpacts"
      />
    </div>
    <div class="ml-8 mr-8">
      <minimizable-card-component
        content="AttackContentCardComponent"
        title="ATTACK FILTERS"
        icon="BadgeAccountAlertOutline"
        @update:data="setAttacks"
      />
    </div>
    <div class="ml-8 mr-8">
      <minimizable-card-component
        content="DateContentCardComponent"
        title="DATE FILTERS"
        icon="CalenderBlankIcon"
        @update:data="setDates"
      />
    </div>
  </div>
</template>
<script>
import IssuersSearchComponent from "@/components/AdvancedSearch/IssuersSearchComponent.vue";
import WeaknessesSearchComponent from "@/components/AdvancedSearch/WeaknessesSearchComponent.vue";
import ProductTypesSearchComponent from "@/components/AdvancedSearch/ProductTypesSearchComponent.vue";
import ShieldRemoveOutline from "@/../node_modules/vue-material-design-icons/ShieldRemoveOutline.vue";
import MinimizableCardComponent from "@/components/MinimizableCardComponent.vue";
import GavelIcon from "@/../node_modules/vue-material-design-icons/Gavel.vue";
import OfficeBuildingIcon from "@/../node_modules/vue-material-design-icons/OfficeBuilding.vue";
import VulnListComponent from "@/components/AdvancedSearch/VulnListComponent.vue";
import CloseIcon from "@/../node_modules/vue-material-design-icons/Close.vue";

export default {
  name: "VulnFiltersComponent",
  components: {
    IssuersSearchComponent,
    WeaknessesSearchComponent,
    ProductTypesSearchComponent,
    MinimizableCardComponent,
    GavelIcon,
    OfficeBuildingIcon,
    ShieldRemoveOutline,
    VulnListComponent,
    CloseIcon,
  },
  methods: {
    closeFilters() {
      this.$emit("closeFilter");
    },
    setIssuers(data) {
      this.$emit("setIssuers", data);
    },
    setWeaknesses(data) {
      this.$emit("setWeaknesses", data);
    },
    setProduct(data) {
      this.$emit("setProduct", data);
    },
    setImpacts(data) {
      this.$emit("setImpacts", data);
    },
    setAttacks(data) {
      this.$emit("setAttacks", data);
    },
    setDates(data) {
      this.$emit("setDates", data);
    },
  },
};
</script>
<style></style>

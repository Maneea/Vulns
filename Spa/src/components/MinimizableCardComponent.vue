<template>
  <div
    :class="
      'bg-primary mt-4 rounded-b-lg ring-1 ring-tertiary/10 shadow-lg flex flex-col gap-2 w-full ' +
      (isminimized ? 'card ' : 'cardExpanded ') + overflow
    "
  >
    <div
      class="flex flex-row bg-tertiary text-primary border-b-[1px] hover:cursor-pointer hover:bg-quaternary/90 transition-colors border-gray-400 shadow-md justify-between p-3"
      @click="isminimized = !isminimized"
    >
      <div class="flex flex-row gap-4">
        <component :is="icon" />
        <div class="font-bold pt-[1px] select-none">{{ this.title }}</div>
      </div>
      <chevron-down-icon
        :class="'transition-all ' + (isminimized ? '' : 'rotate-180')"
      ></chevron-down-icon>
    </div>
    <component :is="content" class="mt-2 p-3 pt-0 pl-4" @update:data="emitData" />
  </div>
</template>
<script>
import ChevronDownIcon from "@/../node_modules/vue-material-design-icons/ChevronDown.vue";
import DateContentCardComponent from "@/components/AdvancedSearch/DateContentCardComponent.vue";
import CalenderBlankIcon from "@/../node_modules/vue-material-design-icons/CalendarBlank.vue";
import ExclamationThickIcon from "@/../node_modules/vue-material-design-icons/ExclamationThick.vue";
import ImpactContentCardComponent from "@/components/AdvancedSearch/ImpactContentCardComponent.vue";
import AttackContentCardComponent from "@/components/AdvancedSearch/AttackContentCardComponent.vue";
import BadgeAccountAlertOutline from "@/../node_modules/vue-material-design-icons/BadgeAccountAlertOutline.vue";
import ShieldRemoveOutline from "@/../node_modules/vue-material-design-icons/ShieldRemoveOutline.vue"
import AppsIcon from "@/../node_modules/vue-material-design-icons/Apps.vue";
import ProductContentCardComponent from "@/components/AdvancedSearch/ProductContentCardComponent.vue";
export default {
  name: "MinimizeableCardComponent",
  components: {
    ChevronDownIcon,
    DateContentCardComponent,
    CalenderBlankIcon,
    ExclamationThickIcon,
    ImpactContentCardComponent,
    AttackContentCardComponent,
    BadgeAccountAlertOutline,
    ShieldRemoveOutline,
    AppsIcon,
    ProductContentCardComponent
  },
  props: {
    title: {
      type: String,
      default: "TITLE",
    },
    icon: {
      type: String,
      require: false,
    },
    content: {
      type: String,
      require: false,
    },
    isMinimized: {
      type: Boolean,
      default: true,
    },
  },
  methods: {
    emitData(data) {
      this.$emit("update:data", data);
    },
  },
  watch: {
    isminimized(newValue) {
      let vm = this
      if (newValue === false) {
        setTimeout(() => {
          if (vm.isminimized === false) 
            vm.overflow = '!overflow-visible'
        }, 500);
      }
      else {
        vm.overflow = ""
      }
    },
  },
  data() {
    return {
      isminimized: this.isMinimized,
      overflow: this.isMinimized ? '' : '!overflow-visible',
    };
  },
};
</script>
<style scoped>
.card {
  overflow: hidden;
  max-height: 48px;
  transition: max-height 0.25s ease-out;
}
.cardExpanded {
  overflow: hidden;
  max-height: 500px;
  transition: max-height 0.5s ease-in;
}
</style>

<template>
  <div
    :class="
      'h-4 w-4 rounded-sm relative transition-all select-none shadow-lg mb-1 cursor-pointer duration-100 active:scale-110 ring-1 ' +
      (state
        ? state === 1
          ? 'bg-stone-200/25 ring-stone-500 hover:bg-stone-200 shadow-stone-400'
          : 'bg-blue-200 ring-blue-500 hover:bg-blue-300/90 shadow-blue-300'
        : 'bg-red-200 ring-red-500 hover:bg-red-300/90 shadow-red-300')
    "
    @click="changeState"
  >
    <div
      :class="
        'absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 transition-opacity ' +
        (state === 2 ? 'opacity-100' : 'opacity-0')
      "
    >
      <CheckIcon :size=12 fillColor="#000080	"></CheckIcon>
    </div>
    <div
      :class="
        'absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 transition-opacity ' +
        (state === 0 ? 'opacity-100' : 'opacity-0')
      "
    >
      <CloseIcon :size=12 fillColor="#800000"></CloseIcon>
    </div>
    <div
      :class="
        'absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 transition-opacity ' +
        (state === 1 ? 'opacity-100' : 'opacity-0')
      "
    >
      <MinusIcon :size=12></MinusIcon>
    </div>
  </div>
</template>
<script>
import CheckIcon from "@/../node_modules/vue-material-design-icons/Check.vue";
import CloseIcon from "@/../node_modules/vue-material-design-icons/Close.vue";
import MinusIcon from "@/../node_modules/vue-material-design-icons/Minus.vue";

export default {
  name: "TriCheckboxComponent",
  components: { CheckIcon, CloseIcon, MinusIcon },
  data() {
    return {
      state: 1, // 0 = false, 1 = neutral, 2 = true
    };
  },
  methods: {
    changeState() {
      this.state++;
      if (this.state > 2) {
        this.state = 0;
      }
      if (this.state === 0) this.$emit("update:data", false);
      else if (this.state === 1) this.$emit("update:data", null);
      else if (this.state === 2) this.$emit("update:data", true);
      
    },
  },
};
</script>
<style></style>

<!-- Refer to https://nightcatsama.github.io/vue-slider-component/#/ for more details on vue-slider. -->
<template>
    <div class="w-full mr-2">
      <vue-slider v-model="score" width="auto" :min=0 :max=10 :interval="0.1" :adsorb=false :lazy=true :enable-cross=false> </vue-slider> 
    </div>
</template>
<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'

export default {
  name: "BaseScoreSliderComponent",
  components: { VueSlider },
  data() {
    return {
      score: [0, 10],
    };
  },
  watch: {
    score(newValue) {
      this.$emit("update:score", newValue);
    }
  }
}
</script>
<style>
  
</style>
<template>
  <div
    v-if="this.vulns"
    class="h-full w-full bg-gradient-to-tl from-quaternary/80 to-quaternary/90 select-none border-x-[1px] shadow-2xl shadow-black border-quaternary"
  >
    <div :class="'flex flex-col h-full ' + (searchPage ? 'relative' : '')">
      <div class="grow overflow-y-auto overflow-x-hidden" v-if="this.loading === false">
        <div
          v-for="(vuln, index) in this.vulns.results"
          @click="handleClick(vuln.id)"
          class="flex flex-row justify-between items-center m-2 border-2 border-quaternary/75 bg-secondary hover:bg-primary hover:shadow-white overflow-hidden transition-transform active:scale-[1.03] cursor-pointer pr-2 rounded-md shadow-md shadow-secondary"
        >
          <div class="flex flex-row gap-4 items-center">
            <div
              class="flex items-center py-4 px-2 overflow-hidden font-mono justify-center bg-quaternary font-bold text-white"
            >

              {{ index != 9 ? ((this.vulns.page == 0 ? "" : this.vulns.page) - 1 + "") + (index + 1) : this.vulns.page + "0" }}
            </div>
            <div class="flex flex-col">
              <p class="font-mono text-lg font-bold">{{ vuln.id }}</p>
              <p class="text-sm opacity-50 italic">{{ vuln.issuer }}</p>
            </div>
          </div>
          <div class="max-w-[3rem] max-h-12 flex items-center justify-center">
            <img
              :src="
                'https://vuln.maneea.net/logos/Issuers/' + vuln.issuerId + '.png'
              "
              class="ring-1 p-1 rounded-md ring-gray-600 shadow-md bg-secondary shadow-gray-500"
            />
          </div>
        </div>
        <div class="h-20"/>
      </div>
      <div class="grow flex items-center justify-center" v-else>
        <Spinner/>
      </div>
      <div
        :class="'absolute bottom-0 bg-quaternary text-primary w-full flex flex-row gap-4 items-start justify-center px-6 py-2 min-h-[3rem] h-12 max-h-12'"
      >
        <button
          class="arrow"
          v-if="(this.vulns.page != 1) && !this.loading"
          @click="changePage(-1)"
        >
          <ArrowLeftBold fillColor="#112D4E"/>
        </button>
        <button class="disabled-arrow" v-else>
          <ArrowLeftBold fillColor="#AAAAAA"/>
        </button>

        Page {{ this.vulns.page }} /
        {{ (this.vulns.size == 10) ? Math.ceil(this.vulns.total / this.vulns.size) : this.vulns.page }}

        <button
          class="arrow"
          v-if="
            (Math.ceil(this.vulns.total / this.vulns.size) != this.vulns.page) && !this.loading
          "
          @click="changePage(1)"
        >
          <ArrowRightBold fillColor="#112D4E"/>
        </button>
        <button class="disabled-arrow" v-else>
          <ArrowRightBold fillColor="#AAAAAA"/>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import ArrowRightBold from "@/../node_modules/vue-material-design-icons/ArrowRightBold.vue";
import ArrowLeftBold from "@/../node_modules/vue-material-design-icons/ArrowLeftBold.vue";
import VulnService from "@/services/VulnService";
import Spinner from "@/components/Spinner.vue";

export default {
  name: "VulnListComponent",
  components: {
    ArrowLeftBold,
    ArrowRightBold,
    Spinner
  },
  props: {
    vulnsProp: {
      required: false,
    },
    payload: {
      required: true,
    },
    ignoreCreated: {
      default: false
    },
    searchPage: {
      default: false
    }
  },
  data() {
    return {
      vulns: this.vulnsProp,
      loading: false,
    };
  },
  watch: {
    vulnsProp(newVal) {
      this.vulns = newVal;
    },
  },
  methods: {
    handleClick(id) {
      this.$emit("handleClick", id);
    },
    changePage(changeBy) {
      this.loading = true
      let vm = this;
      VulnService.getVulnAdvancedSearch({
        ...this.payload,
        page: this.vulns.page + changeBy,
      }).then((response) => {
        vm.loading = false
        vm.vulns = response;
      }).catch((e)=>{
        vm.loading = false
        vm.vulns = null;
      });
    },
  },
  created() {
    if (!this.ignoreCreated) {
      let vm = this
      vm.loading = true
      VulnService.getVulnAdvancedSearch({
        ...this.payload,
        page: 1,
      }).then((response) => {
        vm.loading = false
        vm.vulns = response;
      }).catch((e)=>{
        vm.loading = false
        vm.vulns = null;
      });
    }
  },
};
</script>
<style>
.arrow {
  @apply ring-1 rounded-sm p-1 bg-secondary/75 ring-black shadow-lg shadow-tertiary/70 w-fit h-fit cursor-pointer hover:bg-secondary active:scale-110 transition-transform;
}

.disabled-arrow {
  @apply ring-1 rounded-sm p-1 opacity-50 bg-gray-600/50 ring-black shadow-sm shadow-gray-500 w-fit h-fit cursor-not-allowed;
}
</style>

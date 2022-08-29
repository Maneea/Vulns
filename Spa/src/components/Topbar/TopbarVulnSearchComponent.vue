<template>
  <vSelect
    :placeholder="this.placeholder"
    label="id"
    :options="options"
    @search="fetchOptions"
    class="bg-white w-[48rem] rounded-md"
    :filterBy="filterMethod"
    single
    v-model="selected"
  >
    <template #no-options>
      <div class="italic opacity-75">{{this.options ? "Type to search for a vulnerability..." : "No results found"}}</div>
    </template>
    <template #open-indicator>
      <MagnifyIcon fillColor="#3F72AF"></MagnifyIcon>
    </template>
    <template #option="{ id, description }">
        <div class="flex flex-col">
          <p class="font-bold">{{ id }}</p>
          <p class="italic opacity-50 text-sm">{{ description  }}</p>
        </div>
    </template>
  </vSelect>
</template>

<script>
import vSelect from "vue-select";
import VulnService from "@/services/VulnService";
import MagnifyIcon from "@/../node_modules/vue-material-design-icons/Magnify.vue";


export default {
  name: "TopbarVulnerabilitiesSearchComponent",
  components: { vSelect, MagnifyIcon },
  data() {
    return {
      options: [],
      selected: null,
    };
  },
  watch: { 
    selected(newValue) {
      if(this.selected !== null) {
        this.$emit("update:vulnerability", this.selected);
        this.selected = null
      }
    }
  },
  props : {
    placeholder: {
      type: String,
      default: "Type to search for a vulnerability",
      required: false
    }
  },
  created() {
    VulnService.getVuln({phrase: "a"}).then(weaknesses => {
      this.options = weaknesses.results;
    });
  },
  methods: {
    filterMethod(option, label, search) {
      return label
    },
    async fetchOptions(search, loading) {
      if (search) {
        loading(true);
        await VulnService.getVuln({phrase : search})
          .then((response) => {
              this.options = response.results;
              loading(false);
          })
          .catch((error) => {
            return
          });
      }
    },
  },
};
</script>

<style scoped>
:root {
  --vs-colors--lightest: rgba(60, 60, 60, 0.26);
  --vs-colors--light: rgba(60, 60, 60, 0.5);
  --vs-colors--dark: #333;
  --vs-colors--darkest: rgba(0, 0, 0, 0.15);

  /* Search Input */
  --vs-search-input-color: rgb(0, 0, 0);
  --vs-search-input-bg: rgb(255, 255, 255);
  --vs-search-input-placeholder-color: rgb(170, 170, 170) !important;

  /* Font */
  --vs-font-size: 1rem;
  --vs-line-height: 1.62rem !important;

  /* Disabled State */
  --vs-state-disabled-bg: rgb(248, 248, 248);
  --vs-state-disabled-color: var(--vs-colors--light);
  --vs-state-disabled-controls-color: var(--vs-colors--light);
  --vs-state-disabled-cursor: not-allowed;

  /* Borders */
  --vs-border-color: var(--vs-colors--lightest);
  --vs-border-width: 1px;
  --vs-border-style: solid;
  --vs-border-radius: 0px 8px 8px 0px !important;

  /* Actions: house the component controls */
  --vs-actions-padding: 4px 6px 0 3px;

  /* Component Controls: Clear, Open Indicator */
  --vs-controls-color: var(--vs-colors--light);
  --vs-controls-size: 1;
  --vs-controls--deselect-text-shadow: 0 1px 0 #fff;

  /* Selected */
  --vs-selected-bg: #f0f0f0;
  --vs-selected-color: var(--vs-colors--dark);
  --vs-selected-border-color: var(--vs-border-color);
  --vs-selected-border-style: var(--vs-border-style);
  --vs-selected-border-width: var(--vs-border-width);

  /* Dropdown */
  --vs-dropdown-bg: #fff;
  --vs-dropdown-color: inherit;
  --vs-dropdown-z-index: 1000;
  --vs-dropdown-min-width: 160px;
  --vs-dropdown-max-height: 350px;
  --vs-dropdown-box-shadow: 0px 3px 6px 0px var(--vs-colors--darkest);

  /* Options */
  --vs-dropdown-option-bg: #000;
  --vs-dropdown-option-color: var(--vs-dropdown-color);
  --vs-dropdown-option-padding: 3px 20px;

  /* Active State */
  --vs-dropdown-option--active-bg: #5897fb;
  --vs-dropdown-option--active-color: #fff;

  /* Deselect State */
  --vs-dropdown-option--deselect-bg: #fb5858;
  --vs-dropdown-option--deselect-color: #fff;

  /* Transitions */
  --vs-transition-timing-function: cubic-bezier(1, -0.115, 0.975, 0.855);
  --vs-transition-duration: 150ms;
}
</style>
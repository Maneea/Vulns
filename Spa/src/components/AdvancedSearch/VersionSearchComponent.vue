<template style="background: #282c34; padding: 1rem; border-radius: 0.3rem">
  <vSelect
    :placeholder="this.versions.length === 0 ? '' : 'Any'"
    :options="this.versions"
    class="bg-white w-full"
    multiple
    v-model="selected"
    :disabled="this.versions.length === 0"
    :loading="this.applyLoading"
    :appendToBody="true"
  >
  </vSelect>
</template>

<script>
import vSelect from "vue-select";

export default {
  name: "VersionSearchComponent",
  components: { vSelect },
  data() {
    return {
      options: [],
      selected: [],
      oldValue: [],
    };
  },
  props: {
    versions: {
      type: Array,
      default: () => [],
    },
    applyLoading: {
      type: Boolean,
      default: false,
    },
    sel: {
      require: true,
    },
    product: {
      type: null || String,
      default: null,
    },
    isDelete: {
      type: Boolean,
      required: true,
    }
  },
  watch: {
    selected(newValue) {
      if (newValue != this.sel) {
        this.$emit(
          "update:versions",
          JSON.parse(JSON.stringify(this.selected))
        );
      }
    },
    sel(newValue) {
      if (newValue !== this.selected) {
        this.selected = newValue;
      }
    },
    versions(newValue) {
      this.options = newValue;
    },
    product() {
      if (!this.isDelete) {
        this.selected = null; //FIXME: currently this holds up to 100 versions only because the options are a prop, its should be dynamic like the products and vendors!
      } else {
        this.$emit("finished-delete");
      }
    },
  },
};
</script>

<style>
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
  --vs-line-height: 1.4;

  /* Disabled State */
  --vs-state-disabled-bg: rgb(230, 230, 230) !important;
  --vs-state-disabled-color: var(--vs-colors--light);
  --vs-state-disabled-controls-color: var(--vs-colors--light);
  --vs-state-disabled-cursor: not-allowed;

  /* Borders */
  --vs-border-color: var(--vs-colors--lightest);
  --vs-border-width: 1px;
  --vs-border-style: solid;
  --vs-border-radius: 4px;

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

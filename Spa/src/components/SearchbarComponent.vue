<template>
  <div class="flex flex-row">
    <!-- The dropdown menu option -->
    <Menu as="div" class="relative inline-block text-left">
      <div>
        <MenuButton
          class="inline-flex w-40 justify-between rounded-l-lg border border-gray-300 border-r-0 shadow-sm pr-4 pl-4 py-2 bg-tertiary text-sm text-white font-bold hover:bg-quaternary"
        >
          {{ active }}
          <ChevronDownIcon class="h-5 w-5" aria-hidden="true" />
        </MenuButton>
      </div>
      <transition
        enter-active-class="transition ease-out duration-100"
        enter-from-class="transform opacity-0 scale-95"
        enter-to-class="transform opacity-100 scale-100"
        leave-active-class="transition ease-in duration-75"
        leave-from-class="transform opacity-100 scale-100"
        leave-to-class="transform opacity-0 scale-95"
      >
        <MenuItems
          class="origin-top-right absolute right-0 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5"
        >
          <div class="py-1">
            <MenuItem :v-slot="active">
              <a
                @click="setOption('vuln')"
                :class="'block px-4 py-2 text-sm cursor-pointer ' + ((active === 'Vulnerabilities') ? 'bg-tertiary text-white' : 'bg-gray-100 hover:bg-gray-200')"
                >Vulnerabilities</a
              >
            </MenuItem>
            <MenuItem :v-slot="active">
              <a
                @click="setOption('weakness')"
                :class="'block px-4 py-2 text-sm cursor-pointer ' + ((active === 'Weaknesses') ? 'bg-tertiary text-white' : 'bg-gray-100 hover:bg-gray-200')"
                >Weaknesses</a
              >
            </MenuItem>
          </div>
        </MenuItems>
      </transition>
    </Menu>

    <!-- The vue-select part -->
    <div v-if="active === 'Vulnerabilities'">
      <topbar-vulnerabilities-search-component-vue @update:vulnerability="showModal"></topbar-vulnerabilities-search-component-vue>
    </div>

    <div v-else="active === 'Weaknesses'">
      <TopbarWeaknessesSearchComponentVue @update:weakness="showModal"></TopbarWeaknessesSearchComponentVue>
    </div>

    <ModalComponentVue :contentComponent="contentType" :fetchingId="this.fetchingId" @clear:fetchingId="clearFetching"/>

  </div>
</template>

<script>
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/vue";
import { ChevronDownIcon } from "@heroicons/vue/solid";
import vSelect from "vue-select";
import WeaknessService from "@/services/WeaknessService";
import TopbarWeaknessesSearchComponentVue from "@/components/Topbar/TopbarWeaknessesSearchComponent.vue";
import TopbarVulnerabilitiesSearchComponentVue from "@/components/Topbar/TopbarVulnSearchComponent.vue";
import ModalComponentVue from "@/components/Topbar/ModalComponent.vue";

export default {
  name: "SearchbarComponent",
  components: {
    Menu,
    MenuButton,
    MenuItem,
    MenuItems,
    ChevronDownIcon,
    vSelect,
    TopbarWeaknessesSearchComponentVue,
    TopbarVulnerabilitiesSearchComponentVue,
    ModalComponentVue
},
  watch: { // watch for changes in the ${selected} array, and update the ${returned} array accordingly.
    selected(newValue) {
      this.returned = newValue.map(item => item.id);
      this.$emit("update:weaknesses", this.returned);
    },
  },
  data() {
    return {
      userInput: "",
      active: "Vulnerabilities",
      options: [],
      selected: "",
      contentType: "VulnModalContent",
      fetchingId: null
    };
  },
  methods: {
    setOption(option) {
      if (option === "vuln") {
        this.active = "Vulnerabilities";
      } else if (option === "weakness") {
        this.active = "Weaknesses";
      }
    },
    filterMethod(option, label, search) {
      return label
    },
    async fetchOptions(search, loading) {
      if (search) {
        loading(true);
        await WeaknessService.getWeaknesses(search)
          .then((response) => {
              this.options = response.results;
              loading(false);
          })
          .catch((error) => {
            return
          });
      }
    },
    showModal(data) {
      if (this.active === "Vulnerabilities") {
        this.contentType = "VulnModalContent";
        this.fetchingId = data.id;
      } else if (this.active === "Weaknesses") {
        this.contentType = "WeaknessModalContent";
        this.fetchingId = data.id;
      }
    },
    clearFetching() {
      this.fetchingId = null;
    }
  },
};
</script>

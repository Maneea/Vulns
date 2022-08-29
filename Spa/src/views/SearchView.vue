<template>
  <div class="flex flex-col h-full">
    <div
      class="text-3xl h-fit w-full bg-quaternary text-primary px-4 pt-4 pb-2 font-bold shadow-lg"
    >
      Search
    </div>
    <!-- <div class="w-full h-[1px] bg-gray-400" /> -->
    <div class="flex flex-row h-full">
      <div class="w-4/5 flex flex-col grow">
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
        <div class="flex flex-row">
          <button
            @click="search"
            class="ring-black items-center ml-8 gap-4 flex flex-row w-fit my-4 ring-2 shadow-lg pl-2 pr-4 py-2 bg-quaternary/90 hover:bg-secondary hover:text-quaternary active:scale-105 transition-all select-none cursor-point shadow-gray-500 text-primary font-mono font-bold rounded-md"
          >
          <MagnifyIcon/>  
          <p>Search</p>
          </button>
          <button
            v-if="displayAdd"
            @click="addToDashboard"
            title="Add to dashboard"
            class="ring-black flex gap-4 pr-4 flex-row items-center w-fit ml-8 my-4 ring-2 shadow-lg p-2 bg-quaternary hover:bg-secondary hover:text-quaternary active:scale-105 transition-all select-none cursor-point shadow-gray-500 text-primary font-mono font-bold rounded-md"
          >
          <HeartIcon/>
            <p>Add to Dashboard</p>
          </button>
        </div>
      </div>
      <div class="w-1/5 shadow-xl h-full">
        <div
          class="bg-gradient-to-tl from-quaternary/80 to-quaternary/90 h-full border-x-[1px] border-quaternary shadow-2xl shadow-black"
          v-if="!this.returnedVulns && !this.loading"
        />

        <VulnListComponent
          class="h-full w-full"
          :ignoreCreated="true"
          :searchPage="true"
          :vulnsProp="this.returnedVulns"
          :payload="this.listPayload"
          @handleClick="setFetchingId"
          v-if="!this.loading"
        />
        <div
          v-else
          class="w-full h-full flex items-center justify-center bg-gradient-to-tl from-quaternary/80 to-quaternary/90 border-x-[1px] border-quaternary shadow-2xl shadow-black"
        >
          <Spinner />
        </div>
      </div>
    </div>
  </div>
  <ModalComponentVue
    contentComponent="VulnModalContent"
    :fetchingId="this.fetchingId"
    @clear:fetchingId="clearFetching"
  />
</template>

<script>
import IssuersSearchComponent from "@/components/AdvancedSearch/IssuersSearchComponent.vue";
import WeaknessesSearchComponent from "@/components/AdvancedSearch/WeaknessesSearchComponent.vue";
import ProductTypesSearchComponent from "@/components/AdvancedSearch/ProductTypesSearchComponent.vue";
import VulnService from "@/services/VulnService";
import ShieldRemoveOutline from "@/../node_modules/vue-material-design-icons/ShieldRemoveOutline.vue";
import MinimizableCardComponent from "@/components/MinimizableCardComponent.vue";
import GavelIcon from "@/../node_modules/vue-material-design-icons/Gavel.vue";
import OfficeBuildingIcon from "@/../node_modules/vue-material-design-icons/OfficeBuilding.vue";
import VulnListComponent from "@/components/AdvancedSearch/VulnListComponent.vue";
import ModalComponentVue from "@/components/Topbar/ModalComponent.vue";
import Spinner from "@/components/Spinner.vue";
import AccountService from "@/services/AccountService";
import { createToaster } from "@meforma/vue-toaster";
import HeartIcon from "@/../node_modules/vue-material-design-icons/Heart.vue";
import MagnifyIcon from "@/../node_modules/vue-material-design-icons/Magnify.vue";

export default {
  name: "SearchView",
  components: {
    HeartIcon,
    IssuersSearchComponent,
    WeaknessesSearchComponent,
    ProductTypesSearchComponent,
    MinimizableCardComponent,
    GavelIcon,
    OfficeBuildingIcon,
    ShieldRemoveOutline,
    VulnListComponent,
    ModalComponentVue,
    Spinner,
    MagnifyIcon
  },
  data() {
    return {
      publishedFrom: null,
      publishedTo: null,
      modifiedFrom: null,
      modifiedTo: null,
      issuers: null,
      severityFrom: null,
      severityTo: null,
      hasExploit: null,
      hasFix: null,
      isUserInteractionRequired: null,
      confidentialityImpacts: null,
      integrityImpacts: null,
      availabilityImpacts: null,
      attackVectors: null,
      attackComplexities: null,
      productType: null,
      products: null,
      weaknesses: null,
      weaknessPlatforms: null,
      vendors: null,
      page: 1,
      size: 10,
      returnedVulns: null,
      fetchingId: null,
      listPayload: null,
      loading: false,
      displayAdd: false
    };
  },
  methods: {
    async addToDashboard() {
      let currentDashboard = null
      let vm = this
      if (!this.$store.state.dashboard) {
        await AccountService.getWidgets()
          .then((widgets) => {
            this.$store.dispatch("setDashboard", widgets);
            vm.currentDashboard = widgets
          })
          .catch((error) => {
            return;
          });
      }
      if(!currentDashboard)
      currentDashboard = JSON.parse(
        JSON.stringify(this.$store.state.dashboard)
      );
      let highestIndex = 0;
      for (let widget of currentDashboard) {
        if (+widget.i >= highestIndex) highestIndex = +widget.i + 1;
      }
      currentDashboard.push({
        x: 10,
        y: 32,
        w: 4,
        h: 3,
        minW: 3,
        minH: 3,
        i: highestIndex,
        component: "VulnListComponent",
        payload: this.listPayload,
        title: "Search Query Results",
        searchPage: false
      });
      AccountService.setWidget(currentDashboard)
        .then(() => {
          vm.$store.dispatch("setDashboard", currentDashboard);
          const toaster = createToaster({
              position: "top",
              duration: 2000,
              type: "success",
            });
            toaster.show(
              `<div class="flex flex-row content-center gap-4 h-full w-full justify-between items-center"><svg style="color: white" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24" zoomAndPan="magnify" viewBox="0 0 30 30.000001" height="40" preserveAspectRatio="xMidYMid meet" version="1.0"><defs><clipPath id="id1"><path d="M 2.328125 4.222656 L 27.734375 4.222656 L 27.734375 24.542969 L 2.328125 24.542969 Z M 2.328125 4.222656 " clip-rule="nonzero" fill="white"></path></clipPath></defs><g clip-path="url(#id1)"><path fill="white" d="M 27.5 7.53125 L 24.464844 4.542969 C 24.15625 4.238281 23.65625 4.238281 23.347656 4.542969 L 11.035156 16.667969 L 6.824219 12.523438 C 6.527344 12.230469 6 12.230469 5.703125 12.523438 L 2.640625 15.539062 C 2.332031 15.84375 2.332031 16.335938 2.640625 16.640625 L 10.445312 24.324219 C 10.59375 24.472656 10.796875 24.554688 11.007812 24.554688 C 11.214844 24.554688 11.417969 24.472656 11.566406 24.324219 L 27.5 8.632812 C 27.648438 8.488281 27.734375 8.289062 27.734375 8.082031 C 27.734375 7.875 27.648438 7.679688 27.5 7.53125 Z M 27.5 7.53125 " fill-opacity="1" fill-rule="nonzero"></path></g></svg> <p class="font-bold">Widget Added Successfully!</p></div>`
            );
            vm.displayAdd = false
        })
        .catch((error) => {
          return
        });
    },
    setFetchingId(id) {
      this.fetchingId = id;
    },
    clearFetching() {
      this.fetchingId = null;
    },
    setDates(data) {
      let vm = this;
      if (data.name === "publicationDate") {
        if (data.date) {
          vm.publishedFrom = data.date[0].toISOString();
          vm.publishedTo = data.date[1].toISOString();
        } else {
          vm.publishedFrom = null;
          vm.publishedTo = null;
        }
        response;
      } else if (data.name === "modificationDate") {
        if (data.date) {
          vm.modifiedFrom = data.date[0].toISOString();
          vm.modifiedTo = data.date[1].toISOString();
        } else {
          vm.modifiedFrom = null;
          vm.modifiedTo = null;
        }
      }
    },
    setImpacts(data) {
      let vm = this;
      if (data.name === "confidentiality") {
        if (data.impact.length === 0) vm.confidentialityImpacts = null;
        else vm.confidentialityImpacts = data.impact;
      } else if (data.name === "availability") {
        if (data.impact.length === 0) vm.availabilityImpacts = null;
        else vm.availabilityImpacts = data.impact;
      } else if (data.name === "integrity") {
        if (data.impact.length === 0) vm.integrityImpacts = null;
        else vm.integrityImpacts = data.impact;
      } else if (data.name === "baseScore") {
        vm.severityFrom = data.score[0];
        vm.severityTo = data.score[1];
      } else if (data.name === "hasFix") {
        vm.hasFix = data.state;
      }
    },
    setAttacks(data) {
      let vm = this;
      if (data.name === "vector") {
        if (data.attack.length === 0) vm.attackVectors = null;
        else vm.attackVectors = data.attack;
      } else if (data.name === "complexity") {
        if (data.attack.length === 0) vm.attackComplexities = null;
        else vm.attackComplexities = data.attack;
      } else if (data.name === "hasExploit") {
        vm.hasExploit = data.state;
      } else if (data.name === "userInterraction") {
        vm.isUserInteractionRequired = data.state;
      }
    },
    setWeaknesses(data) {
      let vm = this;
      if (data.length === 0) vm.weaknesses = null;
      else vm.weaknesses = data;
    },

    setIssuers(issuers) {
      if (issuers.length === 0) this.issuers = null;
      else this.issuers = issuers;
    },
    setVendors(vendors) {
      if (vendors.length === 0) this.vendors = null;
      else this.vendors = vendors;
    },
    setProduct(data) {
      let vm = this;
      if (data.name === "products") {
        if (data.type.length === 0) vm.products = null;
        else vm.products = data.type;
      } else if (data.name === "productTypes") {
        if (data.type.length === 0) vm.productType = null;
        else vm.productType = data.type;
      }
    },
    adjustProducts(payload) {
      if (payload.vulnerableProducts) {
        for (
          let index = 0;
          index < payload.vulnerableProducts.length;
          index++
        ) {
          if (payload.vulnerableProducts[index].vendor)
            payload[`vulnerableProducts[${index}].vendor`] =
              payload.vulnerableProducts[index].vendor;
          if (payload.vulnerableProducts[index].productType)
            payload[`vulnerableProducts[${index}].productType`] =
              payload.vulnerableProducts[index].productType;
          if (payload.vulnerableProducts[index].product)
            payload[`vulnerableProducts[${index}].product`] =
              payload.vulnerableProducts[index].product;
          if (payload.vulnerableProducts[index].version)
            payload[`vulnerableProducts[${index}].version`] =
              payload.vulnerableProducts[index].version;
        }
        delete payload.vulnerableProducts;
      }
      return payload;
    },
    adjustIssuers(payload) {
      if (payload.issuers) {
        for (
          let index = 0;
          index < payload.issuers.length;
          index++
        ) {
            payload[`issuers[${index}]`] =
              payload.issuers[index];
        }
        delete payload.issuers;
      }
      return payload;
    },
    adjustWeaknesses(payload) {
      if (payload.weaknesses) {
        for (
          let index = 0;
          index < payload.weaknesses.length;
          index++
        ) {
            payload[`weaknesses[${index}]`] =
              payload.weaknesses[index];
        }
        delete payload.weaknesses;
      }
      return payload;
    },
    adjustAttacks(payload) {
      if (payload.attackComplexities) {
        for (
          let index = 0;
          index < payload.attackComplexities.length;
          index++
        ) {
            payload[`attackComplexities[${index}]`] =
              payload.attackComplexities[index];
        }
        delete payload.attackComplexities;
      }
      if (payload.attackVectors) {
        for (
          let index = 0;
          index < payload.attackVectors.length;
          index++
        ) {
            payload[`attackVectors[${index}]`] =
              payload.attackVectors[index];
        }
        delete payload.attackVectors;
      }
      return payload
    },
    adjustImpacts(payload) {
      if (payload.confidentialityImpacts) {
        for (
          let index = 0;
          index < payload.confidentialityImpacts.length;
          index++
        ) {
            payload[`confidentialityImpacts[${index}]`] =
              payload.confidentialityImpacts[index];
        }
        delete payload.confidentialityImpacts;
      }
      if (payload.integrityImpacts) {
        for (
          let index = 0;
          index < payload.integrityImpacts.length;
          index++
        ) {
            payload[`integrityImpacts[${index}]`] =
              payload.integrityImpacts[index];
        }
        delete payload.integrityImpacts;
      }
      if (payload.availabilityImpacts) {
        for (
          let index = 0;
          index < payload.availabilityImpacts.length;
          index++
        ) {
            payload[`availabilityImpacts[${index}]`] =
              payload.availabilityImpacts[index];
        }
        delete payload.availabilityImpacts;
      }
      return payload
    },
    
    
    search() {
      let vm = this;
      this.loading = true;
      let payload = {};
      if (this.publishedFrom !== null)
        payload.publishedFrom = this.publishedFrom;
      if (this.publishedTo !== null) payload.publishedTo = this.publishedTo;
      if (this.modifiedFrom !== null) payload.modifiedFrom = this.modifiedFrom;
      if (this.modifiedTo !== null) payload.modifiedTo = this.modifiedTo;
      if (this.issuers !== null) payload.issuers = this.issuers;
      if (this.severityFrom !== null) payload.severityFrom = this.severityFrom;
      if (this.severityTo !== null) payload.severityTo = this.severityTo;
      if (this.hasExploit !== null) payload.hasExploit = this.hasExploit;
      if (this.hasFix !== null) payload.hasFix = this.hasFix;
      if (this.isUserInteractionRequired !== null)
        payload.isUserInteractionRequired = this.isUserInteractionRequired;
      if (this.confidentialityImpacts)
        payload.confidentialityImpacts = this.confidentialityImpacts;
      if (this.integrityImpacts)
        payload.integrityImpacts = this.integrityImpacts;
      if (this.availabilityImpacts)
        payload.availabilityImpacts = this.availabilityImpacts;
      if (this.attackVectors) payload.attackVectors = this.attackVectors;
      if (this.attackComplexities)
        payload.attackComplexities = this.attackComplexities;
      if (this.weaknesses) payload.weaknesses = this.weaknesses;
      payload.page = 1;
      if (this.vendors) payload.vendors = this.vendors;
      if (this.weaknessPlatforms)
        payload.weaknessPlatforms = this.weaknessPlatforms;
      if (this.products) {
        let toBeSent = [];
        for (let i = 0; i < this.products.length; i++) {
          if (this.products[i].version) {
            for (let j = 0; j < this.products[i].version.length; j++) {
              toBeSent.push({
                ...this.products[i],
                version: this.products[i].version[j],
              });
            }
          } else if (this.products[i].vendor) {
            toBeSent.push(this.products[i]);
          }
        }
        if (toBeSent.length > 0) {
          payload.vulnerableProducts = JSON.parse(JSON.stringify(toBeSent));
        }
      }
      if (this.productType) {
        if (!payload.vulnerableProducts) {
          payload.vulnerableProducts = [];
        }
        for (let i = 0; i < this.productType.length; i++) {
          payload.vulnerableProducts.push({ productType: this.productType[i] });
        }
      }
      payload = this.adjustProducts(payload);
      payload = this.adjustIssuers(payload);
      payload = this.adjustWeaknesses(payload);
      payload = this.adjustAttacks(payload);
      payload = this.adjustImpacts(payload);
      vm.listPayload = JSON.parse(JSON.stringify(payload));
      VulnService.getVulnAdvancedSearch(payload)
        .then((response) => {
          vm.returnedVulns = response;
          vm.loading = false;
          vm.displayAdd = true
        })
        .catch((e) => {
          vm.returnedVulns = null;
          vm.loading = false;
        });
    },
  },
};
</script>

<style></style>

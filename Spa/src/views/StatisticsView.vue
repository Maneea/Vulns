<template>
  <div class="grow flex flex-col h-full" @click="a">
    <div
      class="text-3xl text-primary px-4 pt-4 pb-2 font-bold shadow-lg w-full bg-quaternary justify-between"
    >
      Statistics
    </div>
    <div class="flex flex-row grow">
      <div class="h-full mt-8 mx-8 w-3/4">
        <div class="flex flex-col gap-6">
          <div
            class="flex flex-row gap-4 justify-between items-center content-center"
          >
            <div class="flex flex-row gap-4 justify-center items-center">
              <div
                v-if="aggregations.length < 10"
                class="border-2 border-tertiary select-none shadow-md w-fit py-1 px-3 rounded-md flex flex-row gap-4 text-tertiary bg-tertiary/10 hover:bg-tertiary/20 transition-transform active:scale-105 hover:cursor-pointer"
                @click="addAggregation"
              >
                <PlusIcon />
                <p>Increase aggregations</p>
              </div>
              <div
                v-if="aggregations.length > 1"
                class="border-2 border-red-500 w-fit py-1 px-3 select-none shadow-md rounded-md flex flex-row gap-4 text-red-800 bg-red-400/10 hover:bg-red-400/20 transition-transform active:scale-105 hover:cursor-pointer"
                @click="removeAggregation"
              >
                <MinusIcon />
                <p>Reduce aggregations</p>
              </div>
            </div>

            <div
              class="flex flex-row w-fit h-fit py-1 items-center justify-center content-center px-3 gap-4 ring-1 ring-[#999999] rounded-sm hover:bg-[#777777]/5 select-none cursor-pointer active:scale-105 transition-transform"
              @click="displayFilters"
            >
              <FilterIcon class="-ml-2" fillColor="#777777" />
              <p class="font-thin text-lg text-[#777777]">Filters</p>
            </div>
          </div>
          <div
            :class="
              'absolute w-8/12 min-h-fit max-h-[45rem] overflow-auto -mt-6 shadow-black/50 shadow-2xl pb-8 rounded-md ring-1 ring-gray-400 bg-gradient-to-b from-secondary to-primary z-[3] transition-all duration-200 ' +
              (this.showFilters ? 'scale-100 opacity-100' : 'scale-0 opacity-0')
            "
            @click="b"
          >
            <VulnFiltersComponent
              @closeFilter="closeFilters"
              @setIssuers="setIssuers"
              @setWeaknesses="setWeaknesses"
              @setProduct="setProduct"
              @setImpacts="setImpacts"
              @setAttacks="setAttacks"
              @setDates="setDates"
            />
          </div>
          <div
            v-for="(aggregation, index) in aggregations"
            :class="
              index > this.cardinalityIndex
                ? 'opacity-50 transition-opacity'
                : 'transition-opacity'
            "
          >
            <AggregationComponent
              :index="index"
              :cardinalityIndex="this.cardinalityIndex"
              :aggregation="aggregation"
              :warning="warnings[index]"
              @update-field="(...args) => this.setField(index, ...args)"
              @update-type="(...args) => this.setType(index, ...args)"
              @update-value="(...args) => this.setValue(index, ...args)"
            />
          </div>
        </div>

        <button
            @click="search"
            class="ring-black items-center gap-4 flex flex-row w-fit my-4 ring-2 shadow-lg pl-2 pr-4 py-2 bg-quaternary/90 hover:bg-secondary hover:text-quaternary active:scale-105 transition-all select-none cursor-point shadow-gray-500 text-primary font-mono font-bold rounded-md"
          >
          <MagnifyIcon/>  
          <p>Search</p>
          </button>
      </div>

      <div
        class="border-x-[1px] w-1/4 flex px-2 flex-col items-center shadow-xl bg-stone-200 grow"
      >
        <div
          v-if="loadSpinner"
          class="h-full flex flex-col items-center justify-center"
        >
          <Spinner />
        </div>
        <ChartOptionComponent
          v-for="option in optionValues"
          v-else
          :units="option.units"
          :icon="option.icon"
          :title="option.title"
          @update:chosenChart="updateChosenChart"
        />
      </div>
    </div>
  </div>
  <ChartsModalComponent
    :chartComponent="chosenChart"
    :data="displayedData"
    :units="units"
    @clear:chartComponent="this.chosenChart = null"
    :payload="this.payload"
    :aggregationLength="this.aggregations.length"
  />
</template>

<script>
import AggregationComponent from "@/components/Statistics/AggregationComponent.vue";
import PlusIcon from "@/../node_modules/vue-material-design-icons/Plus.vue";
import MinusIcon from "@/../node_modules/vue-material-design-icons/Minus.vue";
import FilterIcon from "@/../node_modules/vue-material-design-icons/Filter.vue";
import VulnService from "@/services/VulnService";
import ScalerChartComponent from "@/components/Statistics/Charts/ScalerChartComponent.vue";
import ChartOptionComponent from "@/components/Statistics/ChartOptionComponent.vue";
import ChartsModalComponent from "@/components/Statistics/ChartsModalComponent.vue";
import Spinner from "@/components/Spinner.vue";
import VulnFiltersComponent from "@/components/Statistics/VulnFiltersComponent.vue";
import MagnifyIcon from "@/../node_modules/vue-material-design-icons/Magnify.vue";

export default {
  name: "StatisticsView",
  components: {
    AggregationComponent,
    PlusIcon,
    MinusIcon,
    FilterIcon,
    ScalerChartComponent,
    ChartOptionComponent,
    ChartsModalComponent,
    Spinner,
    VulnFiltersComponent,
    MagnifyIcon,

    // Vulnerability Filters
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
  },
  data() {
    return {
      aggregations: [{ field: null, type: [] }],
      warnings: [[false, false]],
      payload: {},
      showFilters: false,
      inFilter: false,
      response: null,
      cleanData: [],
      units: [],
      optionValues: [],
      aggregationLength: 0,
      chosenChart: null,
      displayedData: null,
      cardinalityIndex: Number.POSITIVE_INFINITY,
      loadSpinner: false,
    };
  },
  watch: {
    aggregations: {
      handler(newValue) {
        let keepSearchingCardinality = true;
        for (let i = 0; i < newValue.length; i++) {
          this.warnings[i] = [false, false];
          if (keepSearchingCardinality && newValue[i].type == "Cardinality") {
            this.cardinalityIndex = i + "";
            keepSearchingCardinality = false;
          }
        }
        if (keepSearchingCardinality) {
          this.cardinalityIndex = Number.POSITIVE_INFINITY;
        }
      },
      deep: true,
    },
    response(newVal) {
      this.cleanData = [];
      this.displayedData = null;
      this.units = [];
      this.optionValues = [];
      let vm = this;
      if (newVal.length > 0) {
        let features = Object.getOwnPropertyNames(newVal[0]);
        for (let i = 0; i < newVal.length; i++) {
          vm.cleanData[i] = {};
          for (let j = 0; j < features.length; j++) {
            vm.cleanData[i]["feature" + j] = newVal[i][features[j]];
          }
        }
        // Removing second feature if conditions are met.
        if (features.length > 2 && vm.aggregationLength == 2) {
          features.splice(1, 1);
          vm.cleanData = vm.cleanData.map(
            ({ feature1, ...keepAttrs }) => keepAttrs
          );
        }

        if (features.length == 1) {
          vm.units[0] = features[0];
          vm.optionValues.push({
            units: vm.units,
            icon: "NumericIcon",
            title: "Scaler",
          });
        } else if (features.length == 2) {
          vm.units[0] = features[0];
          if (vm.aggregationLength == 1) {
            vm.units[1] = "Vulnerabilities";
          } else {
            let splitted = features[1].split("-");
            vm.units[1] = `${splitted[1]} ${splitted[2]}`;
          }
          if (vm.response.length <= 40) {
            vm.optionValues.push({
              units: vm.units,
              icon: "PollIcon",
              title: "Bar Chart",
            });
          }
          let rangeFlag = false;
          for (let i = 0; i != vm.aggregationLength; i++) {
            if (vm.aggregations[i].field.includes("Range")) {
              rangeFlag = true;
              break;
            }
          }
          if (rangeFlag) {
            vm.optionValues.push({
              units: vm.units,
              icon: "ChartLineIcon",
              title: "Line Chart",
            });
            vm.optionValues.push({
              units: vm.units,
              icon: "ChartAreasplineVariantIcon",
              title: "Area Chart",
            });
            if (vm.aggregations[0].field.includes("Severity")) {
              vm.optionValues.push({
                units: vm.units,
                icon: "PieIcon",
                title: "Pie Chart",
              });
            }
          } else {
            vm.optionValues.push({
              units: vm.units,
              icon: "PieIcon",
              title: "Pie Chart",
            });
          }
        } else if (features.length === 3) {
          if (vm.aggregationLength === 2) {
            vm.units[0] = features[0];
            let splitted = features[2].split("-");
            vm.units[1] = splitted[1];
            vm.units[2] = splitted[1] + " " + splitted[2];
          } else {
            vm.units = JSON.parse(JSON.stringify(features));
          }
          let rangeFlag = false;
          for (let i = 0; i != vm.aggregationLength; i++) {
            if (vm.aggregations[i].field.includes("Range")) {
              rangeFlag = true;
              break;
            }
          }
          if (rangeFlag) {
            vm.optionValues.push({
              units: vm.units,
              icon: "ChartLineIcon",
              title: "Multi-Line Chart",
            });
            vm.optionValues.push({
              units: vm.units,
              icon: "ChartAreasplineVariantIcon",
              title: "Multi-Area Chart",
            });
          } else {
            vm.optionValues.push({
              units: vm.units,
              icon: "PollIcon",
              title: "Grouped Bar Chart",
            });
          }
        }
        vm.optionValues.push({
          units: ["JSON representation of the data."],
          icon: "JsonIcon",
          title: "JSON",
        });
      }
      this.loadSpinner = false;
    },
  },
  methods: {
    updateChosenChart(chart) {
      this.chosenChart = chart;
      if (chart === "JsonChartComponent") {
        this.displayedData = JSON.parse(JSON.stringify(this.response));
      } else {
        this.displayedData = JSON.parse(JSON.stringify(this.cleanData));
      }
    },
    a() {
      //purely for testing
      if (!this.inFilter) {
        this.showFilters = false;
      }
      this.inFilter = false;
    },
    b() {
      //purely for testing
      this.inFilter = true;
    },
    closeFilters() {
      this.showFilters = false;
      this.inFilter = false;
    },
    displayFilters() {
      this.showFilters = !this.showFilters;
      this.inFilter = this.showFilters;
    },
    addAggregation() {
      this.aggregations.push({ field: null, type: [] });
      this.warnings.push([false, false]);
    },
    removeAggregation() {
      this.aggregations.pop();
      this.warnings.pop();
    },
    setField(index, field) {
      if (field) this.aggregations[index].field = field.payloadField;
      else this.aggregations[index].field = null;
    },
    setType(index, type) {
      this.aggregations[index].type = type;
    },
    setValue(index, value) {
      this.aggregations[index].value = value;
    },
    changeFormat(params) {
      let toBeReturned = {};
      const addFields = (level, depth) => {
        if (level.SubAggregation) addFields(level.SubAggregation, depth + 1);
        if (level.Field)
          toBeReturned["SubAggregation.".repeat(depth) + "Field"] = level.Field;
        if (level.Type)
          toBeReturned["SubAggregation.".repeat(depth) + "Type"] = level.Type;
        if (level.TypeValue)
          toBeReturned["SubAggregation.".repeat(depth) + "TypeValue"] =
            level.TypeValue;
      };
      addFields(params, 0);
      return toBeReturned;
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
        for (let index = 0; index < payload.issuers.length; index++) {
          payload[`issuers[${index}]`] = payload.issuers[index];
        }
        delete payload.issuers;
      }
      return payload;
    },
    adjustWeaknesses(payload) {
      if (payload.weaknesses) {
        for (let index = 0; index < payload.weaknesses.length; index++) {
          payload[`weaknesses[${index}]`] = payload.weaknesses[index];
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
        for (let index = 0; index < payload.attackVectors.length; index++) {
          payload[`attackVectors[${index}]`] = payload.attackVectors[index];
        }
        delete payload.attackVectors;
      }
      return payload;
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
        for (let index = 0; index < payload.integrityImpacts.length; index++) {
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
      return payload;
    },

    search() {
      this.loadSpinner = true;
      let vm = this;
      this.payload = {};
      this.aggregationLength = this.aggregations.length;
      for (let i = 0; i < this.aggregationLength; i++) {
        if (this.aggregations[i].field == null) {
          this.warnings[i][0] = true;
        } else {
          this.warnings[i][0] = false;
        }
        if (!this.aggregations[i].type) {
          this.warnings[i][1] = true;
        } else {
          if (this.aggregations[i].type.length == 0) {
            this.warnings[i][1] = true;
          } else {
            this.warnings[i][1] = false;
          }
        }
        if (this.aggregations[i].value === null) {
          delete this.aggregations[i].value;
        }
      }
      for (let i = 0; i < this.warnings.length; i++) {
        if (this.warnings[i][0] || this.warnings[i][1]) {
          this.loadSpinner = false;
          return;
        }
      }
      let previousAggregation = null;
      for (let i = this.aggregationLength - 1; i >= 0; i--) {
        this.payload.TypeValue = null;
        this.payload.Field = this.aggregations[i].field;
        this.payload.Type = this.aggregations[i].type;
        if (this.aggregations[i].value) {
          this.payload.TypeValue = this.aggregations[i].value;
        }
        if (previousAggregation) {
          this.payload.SubAggregation = JSON.parse(
            JSON.stringify(previousAggregation)
          );
        }
        previousAggregation = JSON.parse(JSON.stringify(this.payload));
      }
      this.payload = this.changeFormat(
        JSON.parse(JSON.stringify(this.payload))
      );

      if (this.publishedFrom !== null)
        this.payload.publishedFrom = this.publishedFrom;
      if (this.publishedTo !== null)
        this.payload.publishedTo = this.publishedTo;
      if (this.modifiedFrom !== null)
        this.payload.modifiedFrom = this.modifiedFrom;
      if (this.modifiedTo !== null) this.payload.modifiedTo = this.modifiedTo;
      if (this.issuers !== null) this.payload.issuers = this.issuers;
      if (this.severityFrom !== null)
        this.payload.severityFrom = this.severityFrom;
      if (this.severityTo !== null) this.payload.severityTo = this.severityTo;
      if (this.hasExploit !== null) this.payload.hasExploit = this.hasExploit;
      if (this.hasFix !== null) this.payload.hasFix = this.hasFix;
      if (this.isUserInteractionRequired !== null)
        this.payload.isUserInteractionRequired = this.isUserInteractionRequired;
      if (this.confidentialityImpacts)
        this.payload.confidentialityImpacts = this.confidentialityImpacts;
      if (this.integrityImpacts)
        this.payload.integrityImpacts = this.integrityImpacts;
      if (this.availabilityImpacts)
        this.payload.availabilityImpacts = this.availabilityImpacts;
      if (this.attackVectors) this.payload.attackVectors = this.attackVectors;
      if (this.attackComplexities)
        this.payload.attackComplexities = this.attackComplexities;
      if (this.weaknesses) this.payload.weaknesses = this.weaknesses;
      if (this.vendors) this.payload.vendors = this.vendors;
      if (this.weaknessPlatforms)
        this.payload.weaknessPlatforms = this.weaknessPlatforms;
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
          this.payload.vulnerableProducts = JSON.parse(
            JSON.stringify(toBeSent)
          );
        }
      }
      if (this.productType) {
        if (!this.payload.vulnerableProducts) {
          this.payload.vulnerableProducts = [];
        }
        for (let i = 0; i < this.productType.length; i++) {
          this.payload.vulnerableProducts.push({
            productType: this.productType[i],
          });
        }
      }
      this.payload = this.adjustProducts(this.payload);
      this.payload = this.adjustIssuers(this.payload);
      this.payload = this.adjustWeaknesses(this.payload);
      this.payload = this.adjustAttacks(this.payload);
      this.payload = this.adjustImpacts(this.payload);
      VulnService.getStatistics(this.payload)
        .then((response) => {
          vm.response = response;
        })
        .catch((e) => {
          this.loadSpinner = false;
        });
    },
  },
};
</script>

<style></style>

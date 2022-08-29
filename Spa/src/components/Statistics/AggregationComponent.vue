<template>
  <div>
    <div class="flex flex-col gap-2 border-l-2 border-quaternary/25 pl-3">
      <p class="text-quaternary/50" v-if="this.index <= this.cardinalityIndex">
        {{ this.index + 1
        }}{{
          (this.index + 1 + "")[(this.index + 1 + "").length - 1] == "1"
            ? "st"
            : (this.index + 1 + "")[(this.index + 1 + "").length - 1] == "2"
            ? "nd"
            : (this.index + 1 + "")[(this.index + 1 + "").length - 1] == "3"
            ? "rd"
            : "th"
        }}
        Aggregation
      </p>
      <p v-else class="text-red-400">This aggregation is omitted,</p>
      <div class="flex flex-row">
        <div
          :class="
            'flex flex-row h-fit py-[7px] w-28 px-2 text-primary font-bold gap-2 transition-colors duration-100 ' +
            (this.warning[0] ? 'bg-red-500' : 'bg-tertiary')
          "
        >
          <CodeTagsIcon />
          <p>FIELD</p>
        </div>
        <field-search-component
          :jsonOptions="fields"
          @field-selected="updateField"
          :warning="this.warning[0]"
        />
      </div>
      <div class="flex flex-row">
        <div
          :class="
            'flex flex-row h-fit py-[7px] w-28 px-2 text-primary font-bold gap-2 transition-colors ' +
            (this.warning[1] ? 'bg-red-500' : 'bg-tertiary')
          "
        >
          <ChartBoxOutline />
          <p>TYPE</p>
        </div>
        <FieldTypeSearchComponent
          :types="types"
          @type-selected="updateType"
          :warning="this.warning[1]"
        />
      </div>
      <div class="flex flex-row" v-if="displayNumberField || displayDateField">
        <div
          :class="
            'flex flex-row h-fit py-[7px] w-28 px-2 text-primary font-bold gap-2 transition-colors ' +
            (this.warning[1] ? 'bg-red-500' : 'bg-tertiary')
          "
        >
          <Numeric7Box />
          <p>VALUE</p>
        </div>
        <input
          v-model="selectedNumber"
          type="number"
          placeholder="Default number of terms: 10"
          min="1"
          class="ring-1 px-2 w-full ring-gray-400/50"
          v-if="displayNumberField"
        />
        <DateValuesSearchComponent
          v-else-if="displayDateField"
          @value-selected="updateVal"
        />
      </div>
    </div>
  </div>
</template>
<script>
import FieldSearchComponent from "./FieldSearchComponent.vue";
import FieldTypeSearchComponent from "./FieldTypeSearchComponent.vue";
import jsonOptions from "@/components/Statistics/statisticsParameters.json";
import CodeTagsIcon from "@/../node_modules/vue-material-design-icons/CodeTags.vue";
import ChartBoxOutline from "@/../node_modules/vue-material-design-icons/ChartBoxOutline.vue";
import Numeric7Box from "@/../node_modules/vue-material-design-icons/Numeric7Box.vue";
import DateValuesSearchComponent from "@/components/Statistics/DateValuesSearchComponent.vue";
export default {
  name: "AggregationComponent",
  components: {
    FieldSearchComponent,
    FieldTypeSearchComponent,
    CodeTagsIcon,
    ChartBoxOutline,
    DateValuesSearchComponent,
    Numeric7Box
  },
  data() {
    return {
      fields: jsonOptions,
      types: [],
      chosenType: null,
      chosenField: null,
      selectedNumber: null,
      displayNumberField: false,
      displayDateField: false,
    };
  },
  props: {
    index: {
      default: 1,
    },
    aggregation: {
      required: true,
    },
    warning: {
      default: [false, false],
    },
    cardinalityIndex: {
      default: Number.POSITIVE_INFINITY,
    },
  },
  methods: {
    updateField(field) {
      this.chosenField = field;
      if (field) {
        this.types = field.type;
      } else {
        this.types = [];
      }
      this.$emit("update-field", field);
      this.$emit("update-type", []);
    },
    updateType(type) {
      this.updateVal(null);
      this.chosenType = type;
      this.$emit("update-type", type);
      if (this.chosenField) {
        this.valueFieldCheck();
      } else {
        this.displayDateField = false;
        this.displayNumberField = false;
      }
    },
    updateVal(value) {
      if (value == "") value = 10;
      this.$emit("update-value", value); // FIXME: handle this in the parent
    },
    valueFieldCheck() {
      let vm = this;
      if (
        this.fields
          .map((val) => {
            return !!val.hasValue && vm.chosenField.field == val.field;
          })
          .includes(true)
      ) {
        if (vm.chosenType == "Terms") {
          vm.selectedNumber = 10;
          vm.updateVal(10);
          vm.displayNumberField = true;
          vm.displayDateField = false;
        } else if (vm.chosenType == "DateHistogram") {
          vm.updateVal("Year");
          vm.displayDateField = true;
          vm.displayNumberField = false;
        } else {
          vm.displayNumberField = false;
          vm.displayDateField = false;
        }
      } else {
        vm.displayNumberField = false;
        vm.displayDateField = false;
      }
    },
  },
  watch: {
    selectedNumber(newVal, oldVal) {
      if (newVal < 1 && Number.isInteger(newVal)) {
        this.selectedNumber = oldVal;
      } else {
        this.updateVal(this.selectedNumber);
      }
    },
  },
};
</script>
<style></style>

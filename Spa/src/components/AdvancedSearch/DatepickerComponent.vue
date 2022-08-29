<template>
  <Datepicker
    placeholder="Any"
    v-model="date"
    @update:modelValue="handleDate"
    range
    multiCalendars
    :presetRanges="presetRanges"
    hideOffsetDates
    nowButtonLabel="Current"
    :enableTimePicker="false"
    :maxDate="new Date()"
    autoApply
    :closeOnAutoApply="true"
    :transitions="false"
    clearable
  ></Datepicker>
</template>

<script>
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import {
  endOfMonth,
  endOfYear,
  startOfMonth,
  startOfWeek,
  endOfWeek,
  startOfYear,
  subWeeks,
} from "date-fns";

export default {
  name: "DatepickerComponent",
  components: { Datepicker },
  data() {
    return {
      date: null,
      presetRanges: [
        { label: "Today", range: [new Date(), new Date()] },
        {
          label: "This Week",
          range: [
            startOfWeek(subWeeks(new Date(), 1)),
            endOfWeek(subWeeks(new Date(), 1)),
          ],
        },
        {
          label: "This Month",
          range: [startOfMonth(new Date()), endOfMonth(new Date())],
        },
        {
          label: "This Year",
          range: [startOfYear(new Date()), endOfYear(new Date())],
        },
      ],
    };
  },
  methods: {
    handleDate(date) {
      //FIXME: Backend will change the date format to something else.
      this.$emit("update:date", date);
    },
  },
};
</script>

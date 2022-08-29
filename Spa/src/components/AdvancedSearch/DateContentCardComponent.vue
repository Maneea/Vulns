<template>
  <div class="flex flex-col gap-2">
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class="w-48">Publication Date:</p>
      <div class="w-full">
        <datepicker-component-vue @update:date="setPublicationDate" />
      </div>
    </div>
    <div class="flex flex-row content-center items-center justify-between gap-4">
      <p class="w-48">Modification Date:</p>
      <div class="w-full">
        <datepicker-component-vue @update:date="setModificationDate" />
      </div>
    </div>
  </div>
</template>
<script>
import DatepickerComponentVue from './DatepickerComponent.vue';

export default {
  name: "DateContentCardComponent",
  components : {DatepickerComponentVue},
  methods: {
    setPublicationDate(date) {
      this.$emit("update:data", {date: date, name: "publicationDate"});
    },
    setModificationDate(date) {
      this.$emit("update:data", {date: date, name: "modificationDate"});
    },
  },
}
</script>
<style></style>

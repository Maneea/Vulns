<template>
  <div class="flex flex-col max-h-full">
    <div
      class="bg-quaternary p-2 flex flex-row items-center justify-between"
    >
      <div/>
      <p class="font-bold text-primary text-3xl">JSON</p>
      <div @click="saveFile" class="text-primary ring-1 shadow-md rounded-md px-3 py-1 bg-tertiary flex flex-row gap-4 cursor-pointer select-none hover:bg-tertiary/80 transition-transform active:scale-105">
        <ExportVariantIcon/>
        <p>Export</p>
      </div>
    </div>
    <div class="grow overflow-y-auto">
      <VueJsonPretty
        :data="this.data"
        class="bg-gradient-to-t from-white via-primary to-secondary/50"
        :showLineNumber="true"
        :showLength="true"
      />
    </div>
  </div>
</template>
<script>
import VueJsonPretty from "vue-json-pretty";
import "vue-json-pretty/lib/styles.css";
import ExportVariantIcon from "@/../node_modules/vue-material-design-icons/ExportVariant.vue";
export default {
  name: "JsonChartComponent",
  components: {
    VueJsonPretty,
    ExportVariantIcon
  },
  props: {
    data: {
      required: true,
    },
  },
  methods: {
    saveFile: function () {
      const data = JSON.stringify(this.data);
      const blob = new Blob([data], { type: "text/plain" });
      const e = document.createEvent("MouseEvents"),
      a = document.createElement("a");
      a.download = "Data.json";
      a.href = window.URL.createObjectURL(blob);
      a.dataset.downloadurl = ["Data/json", a.download, a.href].join(":");
      e.initEvent(
        "click",
        true,
        false,
        window,
        0,
        0,
        0,
        0,
        0,
        false,
        false,
        false,
        false,
        0,
        null
      );
      a.dispatchEvent(e);
    },
  },
};
</script>
<style></style>

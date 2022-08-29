<template>
  <!--TODO: This widget's UI is temporary, a better UI is needed -->
  <div v-if="this.vulns" class="h-full font-mono">
  <div class="absolute right-1 top-1 p-3 ring-1 ring-gray-300 rounded-3xl shadow-lg bg-red-100">Latest Vulnerabilities Widget</div>
    <div class="flex flex-col h-full bg-red-100 overflow-auto">
      <div
        v-for="vuln in this.vulns"
        class="ring-1 ring-black even:bg-gray-100 odd:bg-gray-200 grow flex flex-col content-center justify-center"
      >
        <div class><b>CVE ID:</b> {{ vuln.id }}</div>
        <div class><b>MODIFIED AT:</b>{{ vuln.modifiedAt }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import VulnService from "@/services/VulnService";

export default {
  name: "LatestVulnComponent",
  data() {
    return {
      vulns: null,
    };
  },
  created() {
    let vm = this
    VulnService.getLatestVuln(1, 10).then((vulns) => {
      vm.vulns = vulns;
    }).catch((error) => {
      console.log("Can't fetch vulnerabilities: ", error.message)
    });
  },
};
</script>

<style></style>

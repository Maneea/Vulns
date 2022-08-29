<template>
  <div
    v-if="this.type === 'VulnModalContent'"
    class="flex flex-col overflow-auto grow"
  >
    <!-- Upon change, preserve the v-if -->
    <div v-if="vuln" class="h-full flex flex-col">
      <div
        class="w-full h-32 bg-gray-900 shadow-lg flex flex-row p-2 content-center items-center justify-between"
      >
        <div class="pl-8 h-full flex flex-col justify-center">
          <p class="text-6xl text-white font-mono">{{ vuln.id }}</p>
          <p class="text-md text-secondary italic">
            {{ vuln.issuer.organization }}
          </p>
        </div>
        <img
          :src="
            'https://vuln.maneea.net/logos/Issuers/' + vuln.issuer.id + '.png'
          "
          class="h-full max-w-xs py-1 mx-4"
        />
      </div>

      <div class="bg-blue-100 flex flex-row grow">
        <div class="w-4/5 bg-secondary border-r-[1px] border-gray-900 p-2">
          <div class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg">
            <div
              class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
            >
              <NoteTextOutline fillColor="#FFFFFF" />
              <p class="font-bold text-primary">Description</p>
            </div>
            <p class="p-2">{{ vuln.description }}</p>
          </div>
          <div class="w-full bg-gray-400 h-[1px] my-4" />

          <div class="flex flex-row gap-4">
            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-1/2 h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <AppsIcon fillColor="#FFFFFF" />
                <p class="font-bold text-primary">Vulnerable Products</p>
              </div>
              <ul class="p-2" v-if="vuln.vulnerableProducts.length > 0">
                <li
                  v-for="(product, index) in vuln.vulnerableProducts"
                  class="my-2 flex flex-row h-fit"
                >
                  <div
                    class="ring-1 ring-tertiary rounded-l-md bg-tertiary font-bold text-white w-8 flex justify-center items-center"
                  >
                    <p class="">{{ index + 1 }}</p>
                  </div>
                  <div
                    class="ring-1 ring-tertiary rounded-r-lg bg-primary/30 flex flex-col p-2 w-full"
                  >
                    <div class="flex flex-row gap-2">
                      <p class="font-bold">Product:</p>
                      <p>{{ product.title }}</p>
                    </div>
                    <div class="flex flex-row gap-2">
                      <p class="font-bold">Version:</p>
                      <p>{{ product.version }}</p>
                    </div>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No vulnerable products found.</p>
              </div>
            </div>

            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-1/2 h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <ShieldRemoveOutline fillColor="#FFFFFF" />
                <p class="font-bold text-primary">Weaknesses</p>
              </div>
              <ul class="p-2" v-if="vuln.weaknesses.length > 0">
                <li
                  v-for="(weakness, index) in vuln.weaknesses"
                  class="my-2 flex flex-row h-fit"
                >
                  <div
                    class="ring-1 ring-tertiary rounded-l-md bg-tertiary font-bold text-white w-8 flex justify-center items-center"
                  >
                    <p class="">{{ index + 1 }}</p>
                  </div>
                  <div
                    class="ring-1 ring-tertiary rounded-r-lg bg-primary/30 flex flex-col p-2 w-full"
                  >
                    <div class="flex flex-row gap-2">
                      <p class="font-bold">ID:</p>
                      <p>{{ weakness.id }}</p>
                    </div>
                    <div class="">
                      <p class="font-bold inline">Description: </p>
                      <p class="inline">{{ weakness.name }}</p>
                    </div>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No weaknesses found.</p>
              </div>
            </div>
          </div>
          <div class="w-full bg-gray-400 h-[1px] my-4" />

          <div class="flex flex-row gap-4">
            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-full h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <Book fillColor="#FFFFFF" />
                <p class="font-bold text-primary">References</p>
              </div>
              <ul class="p-2" v-if="vuln.references.length > 0">
                <li
                  v-for="(reference, index) in vuln.references"
                  class="my-2 flex flex-row h-fit"
                >
                  <div
                    class="ring-1 ring-tertiary rounded-l-md bg-tertiary font-bold text-white w-8 flex justify-center items-center"
                  >
                    <p class="">{{ index + 1 }}</p>
                  </div>
                  <div
                    class="ring-1 ring-tertiary rounded-r-lg bg-primary/30 flex flex-col p-2 w-full"
                  >
                    <div class="flex flex-row gap-2">
                      <a class="text-blue-600" :href="reference.url">{{
                        reference.url
                      }}</a>
                    </div>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No references found.</p>
              </div>
            </div>
          </div>
        </div>
        <div class="flex flex-col w-1/5 p-2 gap-0 items-center">
          <p class="text-2xl">General Information</p>
          <div class="w-full bg-gray-400 h-[1px] my-4" />

          <div
            class="ring-1 mb-2 p-3 bg-secondary ring-quaternary shadow-lg w-full rounded-sm"
          >
            <div class="">
              <p class="inline font-bold">Published: </p>
              <p class="inline">{{ vuln.publishedAt.split('T')[0] }}</p>
            </div>
            <div class="">
              <p class="inline font-bold">Modified: </p>
              <p class="inline">{{ vuln.modifiedAt.split('T')[0] }}</p>
            </div>
          </div>

          <div
            class="ring-1 mb-2 p-3 bg-secondary ring-quaternary shadow-lg w-full rounded-sm"
          >
            <div class="">
              <p class="inline font-bold">Impact Score: </p>
              <p class="inline">{{ vuln.impactScore }} / 10</p>
            </div>
            <div class="">
              <p class="inline font-bold">Base Score: </p>
              <p class="inline">{{ vuln.baseScore }} / 10</p>
            </div>
            <div class="">
              <p class="inline font-bold">Level: </p>
              <p class="inline">{{ vuln.level }}</p>
            </div>
          </div>

          <div
            class="ring-1 mb-2 p-3 bg-secondary ring-quaternary shadow-lg w-full rounded-sm"
          >
            <div class="">
              <p class="inline font-bold">Has Exploit?: </p>
              <p class="inline">{{ vuln.hasExploit ? "✔️" : "❌" }}</p>
            </div>
            <div class="">
              <p class="inline font-bold">Has Fix?: </p>
              <p class="inline">{{ vuln.hasFix ? "✔️" : "❌" }}</p>
            </div>
            <div class="">
              <p class="inline font-bold">Requires User Interaction?: </p>
              <p class="inline">
                {{ vuln.userInteractionRequired ? "✔️" : "❌" }}
              </p>
            </div>
          </div>

        </div>
      </div>
    </div>
    <div v-else class="flex content-center items-center justify-center h-full">
      <Spinner />
    </div>
  </div>
</template>
<script>
import VulnService from "@/services/VulnService";
import Spinner from "@/components/Spinner.vue";
import NoteTextOutline from "@/../node_modules/vue-material-design-icons/NoteTextOutline.vue";
import AppsIcon from "@/../node_modules/vue-material-design-icons/Apps.vue";
import ShieldRemoveOutline from "@/../node_modules/vue-material-design-icons/ShieldRemoveOutline.vue";
import Book from "@/../node_modules/vue-material-design-icons/Book.vue";
export default {
  name: "VulnModalContent",
  components: {
    NoteTextOutline,
    Spinner,
    AppsIcon,
    ShieldRemoveOutline,
    Book,
  },
  props: {
    fetchingId: String,
    type: String,
  },
  data() {
    return {
      vuln: null,
    };
  },
  watch: {
    fetchingId(newId) {
      this.vuln = null;
      if (newId !== null && this.type === "VulnModalContent") {
        VulnService.getDetailedVuln(newId)
          .then((res) => {
            this.vuln = res;
          })
          .catch((err) => {
            return;
          });
      }
    },
  },
};
</script>
<style></style>

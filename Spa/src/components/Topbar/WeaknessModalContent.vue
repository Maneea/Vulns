<template>
  <div
    v-if="this.type === 'WeaknessModalContent'"
    class="flex flex-col overflow-auto grow"
  >
    <!-- Upon change, preserve the v-if -->
    <div v-if="weakness" class="h-full flex flex-col">
      <div
        class="w-full h-32 bg-gray-900 shadow-lg flex flex-row p-2 content-center items-center justify-between"
      >
        <div class="pl-8 h-full flex flex-col justify-center">
          <p class="text-6xl text-white font-mono">{{ weakness.id }}</p>
          <p class="text-md text-secondary italic">
            {{ weakness.name }}
          </p>
        </div>
        <div class="flex flex-col">
          <div class="text-6xl text-white font-bold opacity-25 mr-6">
            {{ weakness.type }}
          </div>
        </div>
      </div>

      <div class="bg-blue-100 flex flex-row grow">
        <div class="w-full bg-secondary border-r-[1px] border-gray-900 p-2">
          <div class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg">
            <div
              class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
            >
              <NoteTextOutline fillColor="#FFFFFF" />
              <p class="font-bold text-primary">Description</p>
            </div>
            <p class="p-2">{{ weakness.description }}</p>
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
                <p class="font-bold text-primary">Affected Resources</p>
              </div>
              <ul class="p-2" v-if="weakness.affectedResources.length > 0">
                <li
                  v-for="(
                    affectedResource, index
                  ) in weakness.affectedResources"
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
                    <p>{{ affectedResource }}</p>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No affected resources found.</p>
              </div>
            </div>

            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-1/2 h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <DesktopClassic fillColor="#FFFFFF" />
                <p class="font-bold text-primary">Platforms</p>
              </div>
              <ul class="p-2" v-if="weakness.platforms.length > 0">
                <li
                  v-for="(platform, index) in weakness.platforms"
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
                      <p class="font-bold">Name: </p>
                      <p>{{ platform.name }}</p>
                    </div>
                    <div class="">
                      <p class="font-bold inline">Type: </p>
                      <p class="inline">{{ platform.type }}</p>
                    </div>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No Platforms found.</p>
              </div>
            </div>
          </div>
          <div class="w-full bg-gray-400 h-[1px] my-4" />

          <div class="w-full bg-gray-400 h-[1px] my-4" />
          <div class="flex flex-row gap-4">
            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-1/2 h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <ArrowUpThick fillColor="#FFFFFF" />
                <p class="font-bold text-primary">Parent Weakness</p>
              </div>
              <ul class="p-2" v-if="weakness.parent">
                <div class="flex flex-row gap-2">
                  <p class="font-bold">Name:</p>
                  <p>{{ weakness.parent.name }}</p>
                </div>

                <div class="flex flex-row gap-2">
                  <p class="font-bold">ID:</p>
                  <p>{{ weakness.parent.id }}</p>
                </div>
              </ul>
              <div v-else>
                <p class="p-2">No parent weakness found.</p>
              </div>
            </div>

            <div
              class="bg-tertiary/10 ring-1 ring-tertiary rounded-md shadow-lg w-1/2 h-fit"
            >
              <div
                class="flex flex-row gap-2 items-center w-full bg-quaternary p-2 rounded-t-md"
              >
                <ArrowDownThick fillColor="#FFFFFF" />
                <p class="font-bold text-primary">Children Weaknesses</p>
              </div>
              <ul class="p-2" v-if="weakness.children.length > 0">
                <li
                  v-for="(child, index) in weakness.children"
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
                      <p class="font-bold">Name:</p>
                      <p>{{ child.name }}</p>
                    </div>
                    <div class="">
                      <p class="font-bold inline">ID: </p>
                      <p class="inline">{{ child.id }}</p>
                    </div>
                  </div>
                </li>
              </ul>
              <div v-else>
                <p class="p-2">No children found.</p>
              </div>
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
import WeaknessService from "@/services/WeaknessService";
import Spinner from "@/components/Spinner.vue";
import NoteTextOutline from "@/../node_modules/vue-material-design-icons/NoteTextOutline.vue";
import AppsIcon from "@/../node_modules/vue-material-design-icons/Apps.vue";
import DesktopClassic from "@/../node_modules/vue-material-design-icons/DesktopClassic.vue";
import ArrowUpThick from "@/../node_modules/vue-material-design-icons/ArrowUpThick.vue";
import ArrowDownThick from "@/../node_modules/vue-material-design-icons/ArrowDownThick.vue";
export default {
  name: "VulnModalContent",
  components: {
    NoteTextOutline,
    Spinner,
    AppsIcon,
    DesktopClassic,
    ArrowUpThick,
    ArrowDownThick
  },
  props: {
    fetchingId: String,
    type: String,
  },
  data() {
    return {
      weakness: null,
    };
  },
  watch: {
    fetchingId(newId) {
      this.weakness = null;
      if (newId !== null && this.type === "WeaknessModalContent") {
        WeaknessService.getDetailedWeakness(newId)
          .then((res) => {
            this.weakness = res;
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

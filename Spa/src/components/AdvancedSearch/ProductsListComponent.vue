<template>
  <div
    class="w-full bg-white/10 h-fit flex flex-col gap-2 rounded-sm p-2 ring-1 ring-gray-400/50 shadow-lg max-h-[368px] overflow-y-auto"
  >
    <div v-for="(product, index) in this.products" class="flex flex-row">
      <div
        class="flex flex-col gap-2 bg-gray-200 justify-between p-2 w-full rounded-sm h-fit ring-1 ring-gray-400 items-center"
      >
        <div class="flex flex-row justify-start w-full mx-4">
          <p class="w-24 flex items-center">Vendor:</p>
          <VendorsSearchComponent
            @update:vendor="(...args) => this.setVendor(index, ...args)"
            class="w-full"
            :sel="this.products[index].vendor"
          />
        </div>
        <div class="flex flex-row justify-start w-full mx-4">
          <p class="w-24 flex items-center">Product:</p>
          <ProductsSearchComponent
            @update:product="(...args) => this.setProduct(index, ...args)"
            @finished-delete="this.isDelete = false"
            :isDelete="this.isDelete"
            :applyLoading="this.applyProductLoading[index]"
            class="w-full"
            :sel="this.products[index].product"
            :vendor = "this.products[index].vendor"
          />
        </div>
        <div class="flex flex-row justify-start w-full mx-4">
          <p class="w-24 flex items-center">Versions:</p>
          <VersionSearchComponent
            :versions="this.versions[index]"
            :applyLoading="this.applyVersionLoading[index]"
            @update:versions="(...args) => this.setVersions(index, ...args)"
            @finished-delete="this.isDelete = false"
            :sel="this.products[index].version"
            :product = "this.products[index].product"
            :isDelete="this.isDelete"
          />
        </div>
      </div>
        <button
          class="bg-red-300 ring-1 ring-red-500 rounded-r-md hover:bg-red-400/75 hover:cursor-pointer active:bg-red-600/75 transition-transform shadow-md shadow-red-200 mr-1 w-8 flex content-center justify-center items-center"
          @click="this.removeProduct(index)"
        >
          <TrashCanOutline :size="20" fillColor="#B00000"></TrashCanOutline>
        </button>
    </div>
    <div>
      <div
        class="bg-blue-200 ring-1 ring-blue-400 rounded-md hover:bg-blue-300/75 hover:cursor-pointer active:scale-x-[1.01] active:scale-y-[1.15] transition-transform shadow-md shadow-blue-200 w-full h-8 flex content-center justify-center items-center"
        @click="addProduct"
      >
        <PlusThick :size="20" fillColor="#4444FF"></PlusThick>
      </div>
    </div>
  </div>
</template>
<script>
import TrashCanOutline from "@/../node_modules/vue-material-design-icons/TrashCanOutline.vue";
import PlusThick from "@/../node_modules/vue-material-design-icons/PlusThick.vue";
import ProductsSearchComponent from "@/components/AdvancedSearch/ProductsSearchComponent.vue";
import VersionSearchComponent from "@/components/AdvancedSearch/VersionSearchComponent.vue";
import ProductService from "@/services/ProductService";
import VendorsSearchComponent from "./VendorsSearchComponent.vue";
export default {
  name: "ProductsListComponent",
  components: {
    TrashCanOutline,
    PlusThick,
    ProductsSearchComponent,
    VersionSearchComponent,
    VendorsSearchComponent
},
  data() {
    return {
      products: [],
      versions: [],
      applyProductLoading: [],
      applyVersionLoading: [],
      isDelete: false,
    };
  },
  methods: {
    setVendor(index, vendor) {
      this.products[index].vendor = vendor;
      this.$emit("update:products", this.products);
    },
    async setProduct(index, product) {
      this.products[index].product = product;
      this.versions[index] = [];
      if (product){
        this.applyVersionLoading[index] = true;
        this.versions[index] = await ProductService.getVersionsByProduct(
          this.products[index].vendor,
          this.products[index].product
        );
        this.applyVersionLoading[index] = false;
      }
      this.$emit("update:products", this.products);
    },
    removeProduct(index) {
      let productCopy = JSON.parse(JSON.stringify(this.products));
      this.isDelete = true
      this.products.splice(index, 1);
      this.versions.splice(index, 1);
      this.applyProductLoading.splice(index, 1);
      this.applyVersionLoading.splice(index, 1);
      this.$emit("update:products", this.products);

      if (this.products.length == 0) {
        this.isDelete = false
      }
      else {
        let flag = true;
        for (let i = 0; i < this.products.length; i++) {
          if (this.products[i].vendor != productCopy[i].vendor || this.products[i].product != productCopy[i].product) {
            flag = false;
            break;
          }
        }
        if (flag) {
          this.isDelete = false
        }

      }
    },
    addProduct() {
      this.applyVersionLoading.push(false);
      this.applyProductLoading.push(false);
      this.products.push({
        vendor: null,
        product: null,
        version: null,
      });
    },
    setVersions(index, version) {
      this.products[index].version = version;
      this.$emit("update:products", this.products);
    },
  },
};
</script>
<style></style>

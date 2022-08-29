<template>
  <div class="flex flex-col gap-2">
    
    <div
      class="flex flex-row justify-between gap-4"
    >
      <p class="w-36">Products:</p>
      <products-list-component @update:products="setProducts"></products-list-component>
    </div>
    <div
      class="flex flex-row content-center items-center justify-between gap-4"
    >
      <p class="w-36">Product Types:</p>
      <product-types-search-component @update:product-types="setProductTypes" />
    </div>
  </div>
</template>
<script>
import ProductTypesSearchComponent from "./ProductTypesSearchComponent.vue";
import ProductsListComponent from "./ProductsListComponent.vue"
export default {
  name: "ProductContentCardComponent",
  components: {
    ProductTypesSearchComponent,
    ProductsListComponent
  },
  methods: {
    setProductTypes(productType) {
      this.$emit("update:data", { type: productType, name: "productTypes" });
    },
    setProducts(products){
      this.$emit("update:data", { type: products, name: "products" });
    }
  },
};
</script>
<style></style>

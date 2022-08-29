import apiClient from "@/services/NetworkServices";
const PREFIX = "products";
let vendorController = new AbortController(); // Used to abort old vendor requests.
let productController = new AbortController(); // Used to abort old product requests.
let versionController = new AbortController(); // Used to abort old version requests.
let productSearch = ""; // Used to check if the user has changed the search ${DELAY} ms.
let vendorsSearch = ""; // Used to check if the user has changed the search ${DELAY} ms.
const DELAY = 300; // Delay before the search is performed.

export default {
  async getProducts(search = "*", size = 100, page = 1, useController = true) {
    if (useController) {
      productController.abort();
      productController = new AbortController();
    }
    productSearch = search;
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    if (productSearch !== search) {
      return Promise.reject("Search changed");
    }
    return apiClient
      .get(
        `${PREFIX}/typeahead/products/${search}`,
        { size, page },
        productController.signal
      )
      .then((response) => response.data)
      .catch((error) => {
        if (error.code) {
          if (error.code == "ERR_CANCELED") {
            return Promise.reject({
              ...error,
              message: "The request was aborted",
            });
          }
        }
        if (error.response.status === 400) {
          return Promise.reject({
            ...error,
            message: "The input in the request parameters is not valid",
          });
        } else if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          });
        } else if (error.response.status === 404) {
          return Promise.reject({
            ...error,
            message:
              "No CPE Numbering Authorities matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  async getVendors(search = "*", size = 100, page = 1) {
    vendorController.abort();
    vendorsSearch = search;
    vendorController = new AbortController();
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    if (vendorsSearch !== search) {
      return Promise.reject("Search changed");
    }
    return apiClient
      .get(
        `${PREFIX}/typeahead/vendors/${search}`,
        { phrase: search, size, page },
        vendorController.signal
      )
      .then((response) => response.data)
      .catch((error) => {
        if (error.code) {
          if (error.code == "ERR_CANCELED") {
            return Promise.reject({
              ...error,
              message: "The request was aborted",
            });
          }
        }
        if (error.response.status === 400) {
          return Promise.reject({
            ...error,
            message: "The input in the request parameters is not valid",
          });
        } else if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          });
        } else if (error.response.status === 404) {
          return Promise.reject({
            ...error,
            message:
              "No CPE Numbering Authorities matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  async getProductsByVendor(vendor, product = "*", size = 10, useController = true) {
    if (useController) {
      productController.abort();
      productController = new AbortController();
    }
    productSearch = product;
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    if (productSearch !== product) {
      return Promise.reject("Search changed");
    }
    return apiClient
    .get(`${PREFIX}/typeahead/vendors/${vendor}/products/${product}`, { size }, productController.signal)
    .then((response) => response.data.results)
    .catch((error) => {
      if (error.code) {
        if (error.code == "ERR_CANCELED") {
          return Promise.reject({
            ...error,
            message: "The request was aborted",
          });
        }
      }
      if (error.response.status === 400) {
        return Promise.reject({
          ...error,
          message: "The input in the request parameters is not valid",
        });
      } else if (error.response.status === 401) {
        return Promise.reject({
          ...error,
          message: "The user is not authorized",
        });
      } else if (error.response.status === 404) {
        return Promise.reject({
          ...error,
          message:
            "No CPE Numbering Authorities matching the given criteria were found",
        });
      } else {
        return Promise.reject({
          ...error,
          message: "Something unexpectedly went wrong",
        });
      }
    });
  },
  async getVersionsByProduct(vendor, product, phrase="*", size = 100, useController = true) {
    if (useController) {
      versionController.abort();
      versionController = new AbortController();
    }
    return apiClient
    .get(`${PREFIX}/typeahead/vendors/${vendor}/products/${product}/versions/${phrase}`, { size }, versionController.signal)
    .then((response) => response.data.results)
    .catch((error) => {
      if (error.code) {
        if (error.code == "ERR_CANCELED") {
          return Promise.reject({
            ...error,
            message: "The request was aborted",
          });
        }
      }
      if (error.response.status === 400) {
        return Promise.reject({
          ...error,
          message: "The input in the request parameters is not valid",
        });
      } else if (error.response.status === 401) {
        return Promise.reject({
          ...error,
          message: "The user is not authorized",
        });
      } else if (error.response.status === 404) {
        return Promise.reject({
          ...error,
          message:
            "No versions were found for the given product and vendor",
        });
      } else {
        return Promise.reject({
          ...error,
          message: "Something unexpectedly went wrong",
        });
      }
    });
  }
};

import apiClient from "@/services/NetworkServices";
const PREFIX = "issuers";
let controller = new AbortController(); // Used to abort old requests.
let issuerSearch = ""; // Used to check if the user has changed the search ${DELAY} ms.
const DELAY = 300; // Delay before the search is performed.

export default {
  async getIssuers(search = "*", size = 10, page = 1) {
    controller.abort();
    issuerSearch = search;
    controller = new AbortController();
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    if (issuerSearch !== search) {
      return Promise.reject("Search changed");
    }
    return apiClient
      .get(
        `${PREFIX}/typeahead/`,
        { phrase: search, size, page },
        controller.signal
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
              "No CVE Numbering Authorities matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
};

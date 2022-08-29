import apiClient from "@/services/NetworkServices";
const PREFIX = "weaknesses";
let controller = new AbortController(); // Used to abort old requests.
let weaknessSearch = ""; // Used to check if the user has changed the search ${DELAY} ms.
const DELAY = 300; // Delay before the search is performed.

export default {
  async getWeaknesses(search = "a", size = 10, page = 1) {
    controller.abort();
    weaknessSearch = search;
    controller = new AbortController();
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    if (weaknessSearch !== search) {
      return Promise.reject("Search changed");
    }
    return apiClient
      .get(
        `${PREFIX}/typeahead`,
        { phrase: search, size, page },
        controller.signal
      )
      .then((response) => response.data)
      .catch((error) => {
        if (error.code) {
          if (error.code == "ERR_CANCELED"){
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
              "No Weaknesses matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },

  getPlatforms() {
    return apiClient
      .get(`${PREFIX}/platforms`)
      .then((response) => response.data)
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },

  getDetailedWeakness(cweId) {
    return apiClient
      .get(`${PREFIX}/${cweId}`)
      .then((response) => response.data)
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          });
        } else { //FIXME: add the other error cases
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
};

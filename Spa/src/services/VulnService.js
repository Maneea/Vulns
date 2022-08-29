import apiClient from "@/services/NetworkServices";
const PREFIX = "vulnerabilities";
export default {
  async getLatestVuln() {
    return apiClient
      .get(`${PREFIX}/latest`)
      .then((response) => {
        return response.data;
      })
      .catch((error) => {
        if (error.response.status === 400) {
          return Promise.reject({
            ...error,
            message: "Invalid input request parameters",
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
              "No vulnerabilities matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  getImpacts() {
    return apiClient
      .get(`${PREFIX}/impacts`)
      .then((response) => {
        return response.data;
      })
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
  getAttackVectors() {
    return apiClient
      .get(`${PREFIX}/attack-vectors`)
      .then((response) => {
        return response.data;
      })
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
  getAttackComplexities() {
    return apiClient
      .get(`${PREFIX}/attack-complixities`)
      .then((response) => {
        return response.data;
      })
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
  getVuln(params) {
    return apiClient
      .get(`${PREFIX}/typeahead`, { ...params })
      .then((response) => {
        return response.data;
      })
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
  getDetailedVuln(cveId) {
    return apiClient
      .get(`${PREFIX}/${cveId}`)
      .then((response) => {
        return response.data;
      })
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          }); //FIXME: add other error messages
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  getSearchParameters() {
    return apiClient
      .get(`${PREFIX}/search/parameters`)
      .then((response) => {
        return response.data;
      })
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
  getVulnAdvancedSearch(params) {
    return apiClient
      .get(`${PREFIX}/search`, { ...params })
      .then((response) => {
        return response.data;
      })
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized",
          });
        } else if (error.response.status === 404) {
          return Promise.reject({
            ...error,
            message:
              "No vulnerabilities matching the given criteria were found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  getStatistics(params) {
    return apiClient
      .get(`${PREFIX}/statistics`, { ...params })
      .then((response) => {
        return response.data;
      })
      .catch((error) => { //FIXME: add other error messages!
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
  }

};

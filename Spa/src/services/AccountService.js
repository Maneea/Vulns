import apiClient from "./NetworkServices";
import defaultWidgets from "@/components/Dashboard/DefaultUserWidgets"
const PREFIX = "account";
export default {
  async getWidgets() {
    return apiClient
      .get(`${PREFIX}/widgets`)
      .then((response) => {
        try {
          if (response.data != "" || response.data != "[]")
            return JSON.parse(response.data.replaceAll("\\", ""));
          return defaultWidgets
        }
        catch (error) {
          return Promise.reject(error);
        }
      })
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized to access this resource",
          });
        } else if (error.response.status === 404) {
          return Promise.reject({
            ...error,
            message: "No User with the given ID in the JWT claims was found",
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  async setWidget(widgets) {
    let payload = JSON.stringify(widgets).replaceAll('"', '\\"')
    apiClient
    .put(`${PREFIX}/widgets`, payload)
      .then((data) => {
        return data;
      })
      .catch((error) => {
        if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "The user is not authorized to update this resource",
          });
        } else if (error.response.status === 404) {
          return Promise.reject({
            ...error,
            message: "No User with the given ID in the JWT claims was found",
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

import apiClient from "./NetworkServices";
const PREFIX = "auth";
export default {
  async register(credentials) {
    return apiClient
      .post(`${PREFIX}/register`, credentials)
      .then((data) => {
        return data;
      })
      .catch((error) => {
        if (error.response.status === 400) {
          return Promise.reject({
            ...error,
            message: "Invalid registeration body",
          });
        } else if (error.response.status === 406) {
          const errorsList = error.response.data.errors;
          return Promise.reject({
            ...error,
            message: errorsList.map((error) => error.description).join("\n"), // Backend's list of errors
          });
        } else {
          return Promise.reject({
            ...error,
            message: "Something unexpectedly went wrong",
          });
        }
      });
  },
  async login(credentials) {
    return apiClient
      .post(`${PREFIX}/login`, credentials)
      .then((data) => {
        return data;
      })
      .catch((error) => {
        if (error.response.status === 400) {
          return Promise.reject({
            ...error,
            message: "Invalid login body",
          });
        } else if (error.response.status === 401) {
          return Promise.reject({
            ...error,
            message: "Invalid login credentials",
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

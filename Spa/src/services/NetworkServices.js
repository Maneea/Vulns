import axios from "axios";
import store from "@/store";
import router from "@/router";
const client = axios.create({
  baseURL: "https://vuln.maneea.net/",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

function headers() {
  var headers = {
    Accept: "application/json",
    "Content-Type": "application/json",
  };

  if (store.state.user)
    headers["Authorization"] = `Bearer ${store.state.user.token}`;
  return headers;
}

export default {
  get: (endpoint, params, signal=null) =>
    client.get(endpoint, { params, headers: headers(), signal}).catch((error) => {
      try { // In case the error does not contain a status parameter
        if (error.response.status === 401 && router.name !== "LoginView") {
          store.dispatch("logout", true);
        }
      }
      finally {
        return Promise.reject(error);
      }
    }),
  post: (endpoint, params) =>
    client.post(endpoint, params, { headers: headers() }).catch((error) => {
      if (error.response.status === 401 && router.name === "LoginView") {
        store.dispatch("logout", true);
      }
      return Promise.reject(error);
    }),
  put: (endpoint, params) => 
  client.put(endpoint, params, { headers: headers() }).catch((error) => {
    if (error.response.status === 401 && router.name === "LoginView") {
      store.dispatch("logout", true);
    }
    return Promise.reject(error);
  }),
  // delete: (endpoint, params) => client.delete(endpoint, { params, headers: headers() }),
};

import { createStore } from "vuex";
import AuthService from "@/services/AuthService";
import router from "@/router";

export default createStore({
  state: {
    transparency: false,
    user: null,
    dashboard: null
  },
  getters: {},
  mutations: {
    SET_TRANSPARENCY(state, value) {
      state.transparency = value;
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem("user", JSON.stringify(user));
    },
    CLEAR_USER(state) {
      state.user = null;
      localStorage.removeItem("user");
    },
    SET_DASHBOARD(state, dashboard){
      state.dashboard = dashboard
    }
  },
  actions: {
    async setDashboard({ commit }, dashboard) {
      commit("SET_DASHBOARD", dashboard)
    },
    async register({ commit }, credentials) {
      return AuthService.register(credentials)
        .then((response) => {
          commit("SET_USER", response.data);
          return credentials;
        })
        .catch((error) => {
          throw new Error(error.message);
        });
    },
    async login({ commit }, credentials) {
      return AuthService.login(credentials)
        .then((response) => {
          commit("SET_USER", response.data);
          return credentials;
        })
        .catch((error) => {
          throw new Error(error.message);
        });
    },
    logout({ commit }) {
      commit("CLEAR_USER");
      router.push("/login");
    },
  },
  modules: {},
});

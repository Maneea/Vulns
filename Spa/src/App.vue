<template>
  <link
    rel="stylesheet"
    href="https://fonts.googleapis.com/css?family=Poppins"
  />
  <div class="h-screen w-screen flex flex-col">
    <div>
      <router-view name="Topbar"></router-view>
    </div>
    <div class="flex flex-row grow overflow-x-hidden">
      <div
        @mouseover="
          applyTransparency = this.$store.commit('SET_TRANSPARENCY', true)
        "
        @mouseout="
          applyTransparency = this.$store.commit('SET_TRANSPARENCY', false)
        "
      >
        <div>
          <router-view name="Sidebar"></router-view>
        </div>
      </div>
      <div
        :class="
          this.$store.state.transparency
            ? 'transition-opacity duration-300 grow w-full h-full opacity-50 '
            : 'transition-opacity duration-300 grow w-full h-full'
        "
      >
        <div
          @mouseover="
            applyTransparency = this.$store.commit('SET_TRANSPARENCY', false)
          "
          class="bg-gradient-to-t from-primary via-primary to-secondary h-full w-full flex-col overflow-x-hidden"
        >
          <router-view />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import jwt_decode from "jwt-decode";

export default {
  name: "App",
  data() {
    return {
      applyTransparency: false,
    };
  },
  created() {
    //FIXME: It would be ideal to check if the page needs authentication and based on that, reroute to login if needed.
    // Currently, it reroutes to login every time an invalid/expired token is found.
    const user = localStorage.getItem("user");
    if (user) {
      try {
        const unixCurrent = new Date().getTime() / 1000;
        const unixExpiration = jwt_decode(JSON.parse(user).token).exp;
        if (+unixExpiration >= +unixCurrent) {
          this.$store.commit("SET_USER", JSON.parse(user));
        } else {
          console.log("Token expired, logging out");
          this.$store.dispatch("logout", true);
        }
      } catch (e) {
        console.log("Invalid Token, logging out");
        this.$store.dispatch("logout", true);
      }
    }
  },
};
</script>

<style>
body {
  font-family: "Poppins";
}
</style>

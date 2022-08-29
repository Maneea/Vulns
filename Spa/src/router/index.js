import { createRouter, createWebHistory } from "vue-router";
import DashboardView from "@/views/DashboardView.vue";
import LandingView from "@/views/LandingView.vue";
import SidebarComponent from "@/components/SidebarComponent.vue";
import TopbarComponent from "@/components/TopbarComponent.vue";
import StatisticsView from "@/views/StatisticsView.vue";
import WatchlistView from "@/views/WatchlistView.vue";
import SearchView from "@/views/SearchView.vue";
import RegisterView from "@/views/RegisterView.vue";
import LoginView from "@/views/LoginView.vue";
import store from "@/store/index"; // Vuex store
const routes = [
  {
    path: "/",
    name: "LandingView",
    redirect: { name: "DashboardView" },
    component: LandingView,
  },
  {
    path: "/dashboard",
    name: "DashboardView",
    components: {
      default: DashboardView,
      Sidebar: SidebarComponent,
      Topbar: TopbarComponent,
    },
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/search",
    name: "SearchView",
    components: {
      default: SearchView,
      Sidebar: SidebarComponent,
      Topbar: TopbarComponent,
    },
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/statistics",
    name: "StatisticsView",
    components: {
      default: StatisticsView,
      Sidebar: SidebarComponent,
      Topbar: TopbarComponent,
    },
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/register",
    name: "RegisterView",
    component: RegisterView,
  },
  {
    path: "/login",
    name: "LoginView",
    component: LoginView,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  if (
    to.matched.some((record) => record.meta.requiresAuth) &&
    !store.state.user
  ) {
    next({ name: "LoginView" });
  } else if (
    ["RegisterView", "LoginView"].includes(to.name) &&
    store.state.user
  ) {
    next({ name: "DashboardView" });
  } else {
    next();
  }
});

export default router;

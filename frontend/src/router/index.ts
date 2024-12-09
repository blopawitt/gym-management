import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import HomeView from "../views/HomeView.vue";
import ClassesView from "../views/ClassesView.vue";
import LoginView from "../views/LoginView.vue";
import MembersView from "@/views/MembersView.vue";
import TrainersView from "@/views/TrainersView.vue";
import SubscriptionsView from "@/views/SubscriptionsView.vue";
import MoneyView from "@/views/MoneyView.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    meta: { requiresAuth: true },
  },
  {
    path: "/trainers",
    name: "trainers",
    component: TrainersView,
    meta: { requiresAuth: true },
  },
  {
    path: "/classes",
    name: "classes",
    component: ClassesView,
    meta: { requiresAuth: true },
  },
  {
    path: "/members",
    name: "members",
    component: MembersView,
    meta: { requiresAuth: true },
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
  },
  {
    path: "/subscriptions",
    name: "subscriptions",
    component: SubscriptionsView,
    meta: { requiresAuth: true },
  },
  {
    path: "/money",
    name: "money",
    component: MoneyView,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
  const token = localStorage.getItem("token");

  if (requiresAuth && !token) {
    next("/login");
  } else {
    next();
  }
});

export default router;

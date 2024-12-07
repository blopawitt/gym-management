import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import PrimeVue from "primevue/config";
import "primeicons/primeicons.css"; // Importar ícones do PrimeVue
import "@fortawesome/fontawesome-free/css/all.css"; // Importar Font Awesome
import "./assets/tailwind.css"; // Importar Tailwind CSS
import "./assets/global.css"; // Importar estilos globais

const app = createApp(App);

app.use(store);
app.use(router);
app.use(PrimeVue);

app.mount("#app");

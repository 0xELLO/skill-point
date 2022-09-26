import { createApp } from "vue";
import { createPinia } from "pinia";

import "jquery";
import "popper.js";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "font-awesome/css/font-awesome.min.css";

import App from "./App.vue";
import router from "./router";

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.mount("#app");

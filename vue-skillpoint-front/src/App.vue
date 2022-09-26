<script lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import Slider from "./components/Slider.vue";
import { Options, Vue } from "vue-class-component";

import { useGameStore } from "@/stores/games";
import { GameService } from "./services/GameService";
import { CookieService } from "./services/CookieService";
import { useIdentityStore } from "@/stores/identity";
import { IdentityService } from "./services/IdentityService";


@Options({
  components: {
    Header,
    Footer
  }
})
export default class App extends Vue {

    gameStore = useGameStore();
    gameService = new GameService();
    cookieService = new CookieService();
    identityStore = useIdentityStore();
    useIdentityService = new IdentityService();

    async created(): Promise<void> {
    this.gameStore.$state.games = await this.gameService.getAll() ?? [];
    if (this.cookieService.checkCookie("token")){
      let token = this.cookieService.getCookie("token");
      let refreshToken = this.cookieService.getCookie("refreshToken");
          this.identityStore.$state = {jwt: {
            token: token,
            refreshToken: refreshToken
          }}
      this.useIdentityService.refreshIdentity();
    }
  }
}
</script>

<template>
  <Header/>
  <main role="main">
          <RouterView />
  </main>
  <Footer/>
</template>

<style>
body {
  background-color: #e6e8f4;
}
</style>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { useIdentityStore } from "@/stores/identity";
import { CookieService } from "@/services/CookieService";


    @Options({
    })
export default class Header extends Vue {
      identityStore = useIdentityStore();
      cookieService = new CookieService();

      logOutClicked(){
          this.identityStore.jwt = null;
          this.cookieService.deleteTokenCookies();
      }
  }
</script>
<template>
    <nav class="navbar navbar-expand-lg">
      <div class="container">
        <div class="  navbar-collapse d-flex justify-content-between">
          <ul class="navbar-nav">

            <li class="nav-item">
              <RouterLink to="/" class="nav-link mx-2" href="#" active-class="active">SKILL POINT</RouterLink>
            </li>

            <li class="nav-item">
              <RouterLink :to="{name: 'createlobby'}" class="nav-link mx-2" href="#" active-class="active">Crate lobby
              </RouterLink>
            </li>

            <li class="nav-item">
              <RouterLink :to="{name: 'joinlobby'}" class="nav-link mx-2" href="#" active-class="active">Join lobby</RouterLink>
            </li>

          </ul>
          <div class="d-flex ">
            <template v-if="identityStore.$state.jwt == null">

              <span class="float: left;">
                <RouterLink :to="{name: 'login'}" class="nav-link mx-2" href="#" active-class="active">Login</RouterLink>
              </span>
              <span>
                <RouterLink :to="{name: 'register'}" class="nav-link mx-2" href="#" active-class="active">Register</RouterLink>
              </span>

            </template>
            <template v-else>

              <a @click="logOutClicked" to="login" class="nav-link mx-2" href="#" active-class="active">Logout</a>

            </template>
          </div>
        </div>
      </div>
    </nav>
</template>

<style>
@media screen and (min-width:992px){
    .navbar {
        background-color: white;
    }
    .nav-item {
    line-height:10px;
    color: black;
    }

    .nav-link {
        color: black;
        font-size: 20px;
    }
}</style>
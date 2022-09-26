import ReactionGame from "@/views/Games/ReactionGame.vue";
import TypingGame from "@/views/Games/TypingGame.vue";
import MemoryGame from "@/views/Games/MemoryGame.vue";
import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import Login from "@/views/identity/Login.vue";
import Register from "@/views/identity/Register.vue";
import CrateLobby from "@/views/Lobby/CrateLobby.vue";
import JoinLobby from "@/views/Lobby/JoinLobby.vue";
import Lobby from "@/views/Lobby/Lobby.vue";


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [    
    { path: "/", name: "home", component: HomeView, },
    { path: "/typinggame", name: "typinggame", component: TypingGame, props: true},
    { path: "/reactiongame", name: "reactiongame", component: ReactionGame, props: true},
    { path: "/memorygame", name: "memorygame", component: MemoryGame, props: true},
    { path: "/login", name: "login", component: Login,},
    { path: "/register", name: "register", component: Register,},
    { path: "/createlobby", name: "createlobby", component: CrateLobby,},
    { path: "/joinlobby", name: "joinlobby", component: JoinLobby,},
    { path: "/lobby/:token", name: "lobby", component: Lobby, props: true},
  ]
  });

export default router;

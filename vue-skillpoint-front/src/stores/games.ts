import type { IGame } from "@/domain/IGame";
import { GameService } from "@/services/GameService";
import { defineStore } from "pinia";

export const useGameStore = defineStore({
  id: "game",
  state: () => ({
    games: [
    ] as IGame[],
    currentGame: null as IGame | null,
  }),
  getters: {
    gameTyping: (state) => state.games.find(game => game.logoUrl == "keyboard"),
    gameMemory: (state) => state.games.find(game => game.logoUrl == "memory"),
    gameReaction: (state) => state.games.find(game => game.logoUrl == "reaction"),
    findById: (state)  => (id: string) => {return state.games.find(game => game.id == id)}
  },
  actions: {
    add(game: IGame) {
      this.games.push(game);
    }
  },
});
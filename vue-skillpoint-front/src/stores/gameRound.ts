import type { IGameRound } from "@/domain/IGameRound";
import { defineStore } from "pinia";

export const useGameRoundStore = defineStore({
  id: "gameRound",
  state: () => ({
    gameRound: null as IGameRound | null
  }),
  getters: {

  },
  actions: {

  },
});
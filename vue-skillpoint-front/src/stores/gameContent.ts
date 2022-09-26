import type { IGameContent } from "@/domain/IGameContent";
import { defineStore } from "pinia";

export const useGameContentStore = defineStore({
  id: "gameContent",
  state: () => ({
    gameContent: null as IGameContent | null,
  }),
  getters: {
  },
  actions: {
  },
});
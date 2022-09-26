import type { IMatch } from "@/domain/IMatch";
import { defineStore } from "pinia";

export const useMatchStore = defineStore({
  id: "match",
  state: () => ({
    match: null as IMatch | null
  }),
  getters: {

  },
  actions: {

  },
});
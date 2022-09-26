import type { IUserRoundResult } from "@/domain/IUserRoundResult";
import { defineStore } from "pinia";

export const useUserRoundResultStore = defineStore({
  id: "userroundresult",
  state: () => ({
    userRoundResults: [] as IUserRoundResult[]
  }),
  getters: {
    sortTyping(): IUserRoundResult[]{
        return this.userRoundResults.sort((n1,n2) => {
            if (n1.result > n2.result){
                return 1
            }

            if (n1.result < n2.result) {
                return -1;
            }
            return 0;
        })
    }
  },
  actions: {

  },
});
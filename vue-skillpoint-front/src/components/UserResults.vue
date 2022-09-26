<script lang="ts">
import type { IGame } from "@/domain/IGame";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { useUserRoundResultStore } from "@/stores/userRoundResult";
import { useGameRoundStore } from "@/stores/gameRound";
import type { IUserRoundResult } from "@/domain/IUserRoundResult";
import { useGameStore } from "@/stores/games";
import { HelpersService } from "@/services/HelpersService";

    @Options({
        props: {
            game: null as IGame | null,
            gameType: String
        },
    })
export default class UserResults extends Vue {
    game!: IGame
    gameType! : string

    gameInitiolizerService = new GameInitiolizerService();
    helpersService = new HelpersService();
    useUserRoundResultStore = useUserRoundResultStore()
    useGameRoundStore = useGameRoundStore()
    roundResults : IUserRoundResult[] = []
    useGameStore = useGameStore();

    async mounted() {
        console.log(`user reulsts mounted + ${this.game.title} + `)
        if (this.gameType == 'singleplayer') {
            this.useUserRoundResultStore.$state.userRoundResults = await this.gameInitiolizerService.getAllUserResultForGame(this.game);
        } else {
            console.log("getting specific roud results")
            let result = await this.helpersService.GetUserRoundResultByRound(this.useGameRoundStore.$state.gameRound!.id!);
            console.log(result);
            if (result == null || result.length == 0) {
                result = [{gameRoundId: "noId", result: "No results", email:""}] as IUserRoundResult[];
            }
            this.useUserRoundResultStore.$state.userRoundResults = result;
        }
        console.log()
        if (this.game == this.useGameStore.gameTyping){
            this.roundResults = this.useUserRoundResultStore.sortTyping;
        } else {
            this.roundResults = this.useUserRoundResultStore.$state.userRoundResults;
        }
    }
  }
</script>
<template>
    <div class="container result-container mt-3">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <table class="table table-result">
                    <thead class="text-center">
                        <tr class="text-center">
                            <th colspan="2" class="text-center" style="font-size: 25px;" scope="col">Results</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="roundResult of roundResults" :key="roundResult.id">
                            <td class="text-center" style="width: 50%" v-if="(gameType != 'singleplayer') && (roundResult.gameRoundId != 'noId')">{{roundResult.email}}</td>
                            <td class="text-center" style="width: 50%">{{roundResult.result}} </td>
                        </tr>
                    </tbody>

                </table>


            </div>

        </div>
    </div>
</template>

<style>
    .table-result {
        background-color: white;
        border-radius: 5px;;
    }


</style>
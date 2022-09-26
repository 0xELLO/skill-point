<script lang="ts">
import type { IUsers } from "@/domain/IUsers";
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { IdentityService } from "@/services/IdentityService";
import { useGameStore } from "@/stores/games";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import UserResults from "@/components/UserResults.vue";
import type { IGame } from "@/domain/IGame";

@Options({
    components: {
        UserResults
    },
    props: {
        token: String,
    }
})
export default class Lobby extends Vue{
    token!: string;
    gameInitService = new GameInitiolizerService();
    useGameStore = useGameStore();
    joined: string[] = [];
    game = this.useGameStore.$state.currentGame as IGame;

    async mounted(){
        setInterval(async () => {
             this.joined = (await this.gameInitService.getMatchPlayers() as IUsers).emails;
        }, 1000)

    }
    
    playClicked(){
        if (this.useGameStore.$state.currentGame?.id == this.useGameStore.gameTyping?.id) {
            this.$router.push({name: 'typinggame', params: {gameType: 'multiplayer', token: this.token}})
        }
        if (this.useGameStore.$state.currentGame?.id == this.useGameStore.gameMemory?.id) {
            this.$router.push({name: 'memorygame', params: {gameType: 'multiplayer', token: this.token}})
        }
        if (this.useGameStore.$state.currentGame?.id == this.useGameStore.gameReaction?.id) {
            this.$router.push({name: 'reactiongame', params: {gameType: 'multiplayer', token: this.token}})
        }
    }
}
</script>

<template>
    <div class="wrapper normal-color align-items-center d-flex justify-content-center">
        <div class="text-container absolute-center align-items-center text-center">
            <span class="align-middle" style="font-size: 50px;">{{token}}</span> <br />
            <button @click="playClicked" class="btn align-middle button-normal">Play</button>
        </div>
    </div>

        <div class="container result-container mt-3">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <table class="table table-result ">
                    <thead>
                        <tr>
                            <th class="text-center" style="font-size: 25px;" scope="col">Joined</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class=" text-center" v-for="email of joined" :key="email">
                            <td>
                                {{email}}
                            </td>
                        </tr>
                    </tbody>

                </table>


            </div>

        </div>
    </div>
    <UserResults :game="game"/>
</template>

<style>
.button-normal{
    background-color: #FFCB4D;
    font-size: 25px;
    padding-left: 30px;
    padding-right: 30px;
}
</style>
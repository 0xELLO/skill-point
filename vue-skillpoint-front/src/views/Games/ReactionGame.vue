<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { useIdentityStore } from "@/stores/identity";
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { useGameStore } from "@/stores/games";
import type { IGame } from "@/domain/IGame";
import UserResults from "@/components/UserResults.vue";

@Options({
    components: {
        UserResults
    },
        props: {
        gameType: String, //singleplayer // multiplayer
        token: String,

    }
})
export default class ReactionGame extends Vue{
    gameType! : string;
    token!: string;

    gamePhase: string = "idle"; // idle started failed success show
    startTime: number = 0; 
    result: number = 0;
    timerId: number = 0;

    identityStore = useIdentityStore();
    gameInitiolizerService = new GameInitiolizerService();
    useGameStore = useGameStore();

    game = this.useGameStore.gameReaction as IGame;

    async mounted(){
        if (this.identityStore.$state.jwt == null) {
            this.$router.push("/login");
        }
    }

    wrapperCliked(){
        if ((this.gamePhase === "idle" || this.gamePhase == "failed" || this.gamePhase == "success")) {
            this.startTime = 0;
            this.result = 0;
            this.gamePhase = "started";
            this.startGame();

        } else if (this.gamePhase = "show") {
            var clickedTime =  Date.now();
            this.result = clickedTime - this.startTime;
            this.gamePhase = "success";
            this.finishGame()
        } else if (this.gamePhase = "started") {
            this.gamePhase = "failed";
        }
    }

    finishGame(){
        this.gameInitiolizerService.finishGame(this.result.toString());
    }

    async startGame(){
        if (this.gameType == 'singleplayer') {
            await this.gameInitiolizerService.initiolizeSinglePlayerGame(this.useGameStore.gameReaction as IGame)
        }
        this.gamePhase = "started";
        this.startTime = 0;
        this.result = 0;
        let executionTime =  Math.random() * (8000 - 2000) + 2000;

        this.timerId = setTimeout(() => {

            this.gamePhase = "show";
            this.startTime = Date.now();
            }, executionTime);
    }

    failGame(){
        clearTimeout(this.timerId);
        this.startTime = 0;
        this.result = 0;
        this.gamePhase = "failed";
    }
    
    returnToLobby(){
        console.log(this.token)
        if (this.gameType == 'multiplayer') {
            this.$router.push({name: 'lobby', params: {token: this.token}})
        }
    }
    
}
</script>

<template>
<div @click="wrapperCliked" class="wrapper wrapper-reaction normal-color" v-if="gamePhase == 'idle'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">Reaction game</span> <br />
        <span>Click to start</span>
    </div>
</div>

<div @click="failGame" class="start-color wrapper wrapper-reaction" v-else-if="gamePhase == 'started'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">Wait for green...</span> <br />
    </div>
</div>

<div @click="wrapperCliked" class="wrapper wrapper-reaction show-color" v-else-if="gamePhase == 'show'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">Click, fast!</span> <br />
    </div>
</div>

<div @click="wrapperCliked" class="wrapper wrapper-reaction normal-color" v-else-if="gamePhase == 'failed'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">Too soon!</span> <br />
        <span>Click to try again!</span>
    </div>
</div>

<div @click="wrapperCliked" class="wrapper wrapper-reaction normal-color" v-else-if="gamePhase == 'success'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">{{result}} ms</span> <br />
        <span v-if="gameType == 'singleplayer'">Click to try again!</span>
        <button v-else @click="returnToLobby" class="btn button-normal">Return to lobby</button>
    </div>
</div>

 <UserResults :game="game" :gameType="'singleplayer'"/>

</template>

<style>
.wrapper-reaction {
    color: white;
    display: flex;
    justify-content: center;
    align-items: center; 
    cursor: pointer;
}

.text-container {
    color: white;
    font-size: 45px;
}

.normal-color {
    background-color:#49a2e6 ;
}

.start-color {
    background-color: #f74545;
}

.show-color {
    background-color: #51f598;
}


</style>
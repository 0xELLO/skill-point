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
        token: String
    }
})
export default class MemoryGame extends Vue{
    gameType! : string;
        token!: string;



    currentNumber: string = "";
    numberLength: number = 0;
    gamePhase: string = "idle";
    userInput: string = "";
        identityStore = useIdentityStore();
    gameInitiolizerService = new GameInitiolizerService();
    useGameStore = useGameStore();

    game = this.useGameStore.gameMemory as IGame;


    async mounted(){
        if (this.identityStore.$state.jwt == null) {
            this.$router.push("/login");
        }
    }

    getNewNumber(): string{
        this.numberLength++;
        let res = "";
        for (let i = 0; i < this.numberLength; i++) {
            let num = (Math.floor(Math.random() * (9 - 0)) + 0).toString();
            res += num;
        }
        this.currentNumber = res;
        return res;
    }
    
    async startNewGame(){
        if (this.gameType == 'singleplayer') {
            await this.gameInitiolizerService.initiolizeSinglePlayerGame(this.useGameStore.gameMemory as IGame)
        }
        this.currentNumber = '';
        this.userInput = "";
        this.numberLength = 0;
        this.startGame();
    }

    startGame(){
        this.getNewNumber();
        this.gamePhase = "start"
        
        setTimeout(() => {
            this.gamePhase = "type";
            }, 5000);
    }

     async onKeyUp(event: KeyboardEvent) {
         if (event.key == "Enter"){
            if (this.userInput == this.currentNumber) {
                this.userInput = "";
                this.startGame();
            } else {
                await this.finishGame();
                this.gamePhase = "failed";
            }
        }
     }

    async finishGame(){
        await this.gameInitiolizerService.finishGame(this.numberLength.toString());
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

    <div @click="startNewGame" class="wrapper wrapper-reaction normal-color" v-if="gamePhase == 'idle'">
        <div class="text-container text-center">
            <span style="font-size: 50px;">Memory game</span> <br />
            <span>Click to start</span>
        </div>
    </div>

    <div class="wrapper wrapper-reaction normal-color" v-else-if="gamePhase == 'start'">
        <div class="text-container text-center">
            <span style="font-size: 50px;">Try to remeber number!</span> <br />
            <span>{{currentNumber}}</span>
            <div class="container-loading container">
                <div class="Loading"></div>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-reaction normal-color" v-else-if="gamePhase == 'type'">
        <div class="text-container text-center">
            <span style="font-size: 50px;">What was the number?</span> <br />
            <input ref="inputNumber" @keyup="onKeyUp($event)" v-model="userInput" autofocus style="text-align: center;" class="mt-3 input-number" type="text">
        </div>
    </div>

    <div @click="startNewGame" class="wrapper wrapper-reaction normal-color" v-else-if="gamePhase == 'failed'">
        <div class="text-container text-center">
            <span style="font-size: 50px;">Wrong!</span> <br />
            <span v-if="gameType == 'singleplayer'">Try again!</span>
            <button v-else @click="returnToLobby" class="btn button-normal">Return to lobby</button>
        </div>
    </div>


     <UserResults :game="game" :gameType="'singleplayer'"/>
</template>
<style>

.container-loading {
    width: 40%;
}

.input-number {
    border-radius: 5px;
}


.Loading {
  position: relative;
  display: inline-block;
  width: 100%;
  height: 10px;
  background: #f1f1f1;
  box-shadow: inset 0 0 5px rgba(0, 0, 0, .2);
  border-radius: 4px;
  overflow: hidden;
}

.Loading:after {
  content: '';
  position: absolute;
  left: 0;
  width: 0;
  height: 100%;
  border-radius: 4px;
  box-shadow: 0 0 5px rgba(0, 0, 0, .2);
  animation: load 5s infinite;
}

@keyframes load {
  0% {
    width: 0;
    background: #a28089;
  }
  
  25% {
    width: 40%;
    background: #a0d2eb;
  }
  
  50% {
    width: 60%;
    background: #ffa8b6;
  }
  
  75% {
    width: 75%;
    background: #d0bdf4;
  }
  
  100% {
    width: 100%;
    background: #494d5f;
  }
}
</style>
<script lang="ts">
import {
    Options,
    Vue
} from "vue-class-component";
import { RouterLink } from "vue-router";
import type { ITextRepresentation} from "@/views/Games/ITextRepresentation"
import { GameContentService } from "@/services/GameContentService";
import type { IGameContent } from "@/domain/IGameContent";
import { IdentityService } from "@/services/IdentityService";
import { useIdentityStore } from "@/stores/identity";
import { MatchService } from "@/services/MatchService";
import type { IMatch } from "@/domain/IMatch";
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { GameService } from "@/services/GameService";
import {useGameStore} from "@/stores/games"
import type { IGame } from "@/domain/IGame";
import { useGameContentStore } from "@/stores/gameContent";
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
export default class TypingGame extends Vue{
    gameType! : string;
    token! : string;

    timerString: string = "00:00";
    startTime: number = 0;
    timerStarted: boolean = false;
    wpm: number = 0;
    lastEntryNumber: number = 1;
    userInput = "";
    text: string = "";
    timerId: number = 0;
    gamePhase: string = "idle";

    identityStore = useIdentityStore();
    gameInitiolizerService = new GameInitiolizerService();
    gameService = new GameService();
    useGameStore = useGameStore(); 
    useGameContentStore = useGameContentStore();
    
    //text: string = await this.gameContentSerive.getRandomTypingGame();
    textAsCharSpan: ITextRepresentation[] = [];
    GameContentService = new GameContentService();
    MatchService = new MatchService();

    game: IGame = this.useGameStore.gameTyping!;


    async mounted(){
        console.log(this.gameType)
        if (this.identityStore.$state.jwt == null) {
            this.$router.push("/login");

        } 
    }

    async startGame(){
        if (this.gameType == 'singleplayer') {
            await this.gameInitiolizerService.initiolizeSinglePlayerGame(this.useGameStore.gameTyping as IGame)
        }
        this.text = this.useGameContentStore.$state.gameContent?.content ?? "undefiend";
        this.transformToCharSpan();
        this.gamePhase = "started";     
    }

    finishGame(){
        clearTimeout(this.timerId);
        this.lastEntryNumber = this.text.length;
        this.calculateWpm()
        this.gamePhase = "finished";
        this.gameInitiolizerService.finishGame(this.wpm.toString());
    }

    startTimer() {
        this.startTime = Date.now();
        this.timerId = setInterval(() => {
            this.getTimerTime()
            this.calculateWpm()
        }, 1000)
    }

    transformToCharSpan() {
        this.text.split('').forEach((character, index) => {
            var charObject: ITextRepresentation = {
                char: character,
                status: 1
            }
            this.textAsCharSpan.push(charObject);
        })
    }

    onKeyUp(event: KeyboardEvent) {
        if (this.timerStarted == false) {
            this.startTimer();
            this.timerStarted = true;
        }

        let inputValues = this.userInput.split('');

        let correct = true;
        this.textAsCharSpan.forEach((textObject, index) => {
            let inputChar = inputValues[index];
            if (inputChar == null) {
                textObject.status = 1;
                correct = false;
            } else if (inputChar === textObject.char) {
                textObject.status = 3;
                this.lastEntryNumber = index;
            } else {
                textObject.status = 2;
                this.lastEntryNumber = index;
                correct = false;
            }
        });
        if (correct) {
            this.finishGame();
            this.gamePhase == 'finished';
        }
    }


    calculateWpm() {
        let seconds = Math.floor((Date.now() - this.startTime) / 1000);
        let upper = Math.floor(this.lastEntryNumber / 5);
        let bottom = seconds / 60.0;

        console.log("upper: " + upper + " bottom: " + bottom);
        this.wpm = Math.floor(upper / bottom);
    }

    getTimerTime() {
        let seconds = Math.floor((Date.now() - this.startTime) / 1000)
        this.timerString = new Date(seconds * 1000).toISOString().slice(14, 19);
    }

    wrapperCliked(){
        this.startGame();
    }

    returnToLobby(){
        console.log(this.token)
        if (this.gameType == 'multiplayer') {
            this.$router.push({name: 'lobby', params: {token: this.token}})
        }
    }
}
</script>

<template >
<div @click="wrapperCliked" class="wrapper wrapper-reaction normal-color" v-if="gamePhase == 'idle'">
    <div class="text-container text-center">
        <span style="font-size: 50px;">Typing game</span> <br />
        <span>Click to start</span>
    </div>
</div>

<div class="wrapper normal-color" v-if="gamePhase !== 'idle'">
    <div class="game-container pb-3 container">

        <div class="row " v-if="gamePhase == 'finished'">
            <div class="d-flex justify-content-center text-center">
                <div class="col-md-4">
                    <div class="container timer">
                        Your result
                    </div>
                </div>
            </div>
        </div>

        <template v-if="gamePhase == 'started' || gamePhase == 'finished'">

            <div class="row ">
                <div class="d-flex justify-content-center text-center">
                    <div class="col-md-4">
                        <div class="container timer">
                            {{timerString}}
                        </div>
                    </div>

                </div>
            </div>

            <div class="row ">
                <div class="d-flex justify-content-center text-center">
                    <div class="col-md-4">
                        <div class="container timer">
                            {{wpm}} WPM
                        </div>
                    </div>

                </div>
            </div>

        </template>


        <template v-if="gamePhase == 'started'">

            <div class="row justify-content-md-center mt-2">
                <div class="col-lg-8">
                    <div class="container text-block">
                        <template v-for="character of textAsCharSpan" :key="character.status">

                            <template v-if="character.status == 2">
                                <span class="error">{{character.char}}</span>
                            </template>

                            <template v-else-if="character.status == 3">
                                <span class="good">{{character.char}}</span>
                            </template>

                            <template v-else>
                                <span>{{character.char}}</span>
                            </template>

                        </template>
                    </div>
                </div>
            </div>


            <div class="row justify-content-md-center mt-3">
                <div class="col-md-6">
                    <div class="container" style="padding: 0;">
                        <textarea @keyup="onKeyUp($event)" v-model="userInput" autofocus class="input-block"
                            spellcheck="false"></textarea>
                    </div>
                </div>
            </div>

        </template>

        <template v-if="gamePhase == 'finished' && gameType == 'singleplayer'">

            <div class="row ">
                <div class="d-flex justify-content-center text-center">
                    <div class="col-md-4">
                        <div class="container timer">
                            <button @click="startGame" class="btn btn-warning">Try again</button>
                        </div>
                    </div>

                </div>
            </div>

        </template>
        <template v-if="gamePhase == 'finished' && gameType == 'multiplayer'">
            
            <div class="row ">
                <div class="d-flex justify-content-center text-center">
                    <div class="col-md-4">
                        <div class="container timer">
                            <button @click="returnToLobby" class="btn button-normal">Return to lobby</button>
                        </div>
                    </div>

                </div>
            </div>
        </template>


    </div>

</div>

<UserResults :game="game" :gameType="'singleplayer'"/>
</template>

<style>
.wrapper {
    padding-top: 40px;
    padding-bottom: 40px;
    color: white;
    min-height: 350px;
    box-sizing: border-box;
}

.text-block {
    background-color: #bfd8ff;
    font-size: large;
    border-radius: 5px;
    padding: 20px;
    color: black;
}

.input-block{
    background-color: white;
    border: 2px solid #A1922E;
    outline: none;
    width: 100%;
    height: 8rem;
    resize: none;
    padding: .6rem 1rem;
    font-size: large;
    border-radius: .5rem;
}

.input-block:focus {
  border-color: black;
}

.good {
    color: green;
}

.error {
    color: red;
}

.timer {
    font-size: 40px;
}

.button-newtext{
    background-color:#FFCB4D ;
    font-size: 20px;
    margin-top: 20px;
}

</style>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import GameList from "@/components/GameList.vue";
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { useGameStore } from "@/stores/games";
@Options({
    components: {
        GameList
    }
})
export default class CreateLobby extends Vue{
    
    gameInitiolizerService = new GameInitiolizerService();
    useGameStore = useGameStore();

    async initTypingGame(){
        let token = await this.gameInitiolizerService.initiolizeMultyPlayerGame(this.useGameStore.gameTyping!)
        this.$router.push({name: "lobby",  params: {token: token}})
    }

    async initMemoryGame(){
        let token = await this.gameInitiolizerService.initiolizeMultyPlayerGame(this.useGameStore.gameMemory!)
        this.$router.push({name: "lobby",  params: {token: token}})

    }

    async initReactionGame(){
        let token = await this.gameInitiolizerService.initiolizeMultyPlayerGame(this.useGameStore.gameReaction!)
        this.$router.push({name: "lobby",  params: {token: token}})
    }
}
</script>

<template>
    <div class="wrapper normal-color align-items-center d-flex justify-content-center">
            <div class="text-container absolute-center align-items-center">
                <span class="align-middle" style="font-size: 50px;">Choose on of the games below</span> <br />
            </div>
    </div>
    <GameList @typingClicked="initTypingGame" @memoryClicked="initMemoryGame" @reactionClicked="initReactionGame"/>
</template>

<style>
    .standartButton{
        border-radius: 5px;
        background-color: #FFCB4D;
    }


</style>    
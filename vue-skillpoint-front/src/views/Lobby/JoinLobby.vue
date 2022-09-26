<script lang="ts">
import { GameInitiolizerService } from "@/services/GameInitiolizerService";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

export default class JoinLobby extends Vue{
    gameInitiolizerService = new GameInitiolizerService();
    tokenInput = "";
    async joinGame(){
        await this.gameInitiolizerService.joinInMatch(this.tokenInput);
        this.$router.push({name: "lobby",  params: {token: this.tokenInput}})
    }

    onKeyUp(event: KeyboardEvent){
        if (event.key == 'Enter'){
            this.joinGame();
        }
    }
}
</script>

<template>
    <div class="wrapper normal-color align-items-center d-flex justify-content-center">
        <div class="text-container absolute-center align-items-center text-center">
            <span class="" style="font-size: 50px;">Enter game token</span> <br/>
            <input @keyup="onKeyUp($event)" v-model="tokenInput" type="text" class="align-middle input-token" style="font-size: 50px;"/>
        </div>
    </div>
</template>

<style>
.input-token{
    border-radius: 10px;
}
</style>
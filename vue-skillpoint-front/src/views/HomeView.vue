<script lang="ts">
import { RouterLink } from "vue-router";
import { Options, Vue } from "vue-class-component";
import { useGameStore } from "@/stores/games";
import GameList from "@/components/GameList.vue";

@Options({
  components: {
    GameList
  }
})
export default class HomeView extends Vue {
  gameStore = useGameStore();

   getActiveImgUrl(name: string): string{
     return `src/assets/${name}-active.png`
   }
   getNormalImgUrl(name: string): string{
     return `src/assets/${name}-normal.png`
   } 

  getLinkName(name: string){
    if(name == "keyboard") {
      return "typinggame";
    } else if (name == "reaction") {
      return "reactiongame";
    } else if (name == "memory") {
      return "memorygame";
    } else {
      return "home";
    }
  }

  redirectToTyping(){
    this.$router.push({name: "typinggame", params: {gameType: "singleplayer", token: "asdf"}});
  }

   redirectToMemory(){
    this.$router.push({name: "memorygame", params: {gameType: "singleplayer"}});
  }

   redirectToRection(){
    this.$router.push({name: "reactiongame", params: {gameType: "singleplayer"}});
  }

}
</script>

<template>
<div id="myCarousel" class="carousel wrapper wrapper-reaction normal-color slide" data-bs-ride="carousel">
    <div class="carousel-inner">
      <div class="carousel-item active text-center">
        <div class="container">
            <div class="title-container" style="font-size: 50px;">{{gameStore.gameTyping?.title}}</div>
            <div style="font-size: 25px;">{{gameStore.gameTyping?.longDescription}}</div>
        </div>
      </div>
      <div class="carousel-item text-center">
        <div class="container">
            <div class="title-container" style="font-size: 50px;">{{gameStore.gameMemory?.title}}</div>
            <div style="font-size: 25px;">{{gameStore.gameMemory?.longDescription}}</div>
        </div>
      </div>
      <div class="carousel-item text-center">
        <div class="container">
            <div class="title-container" style="font-size: 50px;">{{gameStore.gameReaction?.title}}</div>
            <div style="font-size: 25px;">{{gameStore.gameReaction?.longDescription}}</div>
        </div>
      </div>
    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#myCarousel" data-bs-slide="prev">
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#myCarousel" data-bs-slide="next">
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Next</span>
    </button>
  </div>

  <GameList @typingClicked="redirectToTyping" @reactionClicked="redirectToRection" @memoryClicked="redirectToMemory"/>
</template>

<style>
  .game-list-block {
    min-height: 280px;
    box-sizing: border-box;
    background-color: white;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
    border-radius: 10px;
    cursor: pointer;
  }

  .game-list-block .logo-container img:last-child{
    display: none;
  }

  .game-list-block:hover .logo-container img:last-child{
    display:inline-block;
  }

  .game-list-block:hover {
    border: 5px solid #49A2E6;
  }


  .game-list-block:hover .logo-container img:first-child{
    display: none;
  }

  a:link { text-decoration: none; color: black;}
  a:visited { text-decoration: none; color: black;}
  a:hover { text-decoration: none; color: black;}
  a:active { text-decoration: none; color: black;}

  .title-container{
    font-size: 30px;
    font-weight: bold;
    padding-bottom: 20px;
  }

  .logo-container{
    padding-top: 20px;;
  }

  .description-container {
    font-size: 20px;
  }



</style>

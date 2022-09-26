<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { useGameStore } from "@/stores/games";
@Options({
    emits: ["typingClicked", "memoryClicked", "reactionClicked"]
})
export default class GameList extends Vue{
    gameStore = useGameStore();

}
</script>

<template>
    <div class="container-md mt-4">
  <div class="row">

    <div @click="$emit('typingClicked')" class="col">
        <div class="container game-list-block text-center">
          <div class="logo-container">
            <img src="@/assets/keyboard-normal.png">
            <img src="@/assets/keyboard-active.png">
          </div>
          <div class="title-container">{{gameStore.gameTyping?.title}}</div>
          <div class="description-container">{{gameStore.gameTyping?.shortDescription}}</div>
        </div>
    </div>

    <div @click="$emit('reactionClicked')" class="col">
        <div class="container game-list-block text-center">
          <div class="logo-container">
            <img src="@/assets/reaction-normal.png">
            <img src="@/assets/reaction-active.png">
          </div>
          <div class="title-container">{{gameStore.gameReaction?.title}}</div>
          <div class="description-container">{{gameStore.gameReaction?.shortDescription}}</div>
        </div>
    </div>

    <div @click="$emit('memoryClicked')" class="col">
        <div class="container game-list-block text-center">
          <div class="logo-container">
            <img src="@/assets/memory-normal.png">
            <img src="@/assets/memory-active.png">
          </div>
          <div class="title-container">{{gameStore.gameMemory?.title}}</div>
          <div class="description-container">{{gameStore.gameMemory?.shortDescription}}</div>
        </div>
    </div>

  </div>
</div>
</template>
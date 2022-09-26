<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

    @Options({
    })
export default class Slider extends Vue {
  }
</script>
<template>
    <div class="container slider">
        slider
    </div>
</template>

<style>
    .slider {
        background-color: red;
    }
</style>
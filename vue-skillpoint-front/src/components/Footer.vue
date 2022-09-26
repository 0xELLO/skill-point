<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

    @Options({
    })
export default class Footer extends Vue {
  }
</script>
<template>
    <div id="sticky-footer" class="flex-shrink-0 py-4 bg-dark text-white-50 fixed-bottom">
    <div class="container text-center">
      <small>SkillPoint</small>
    </div>
  </div>
</template>
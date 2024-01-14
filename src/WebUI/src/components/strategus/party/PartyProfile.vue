<script setup lang="ts">
import { type Party } from '@/models/strategus/party';

const { party } = defineProps<{ party: Party }>();
</script>

<template>
  <div
    class="w-[20rem] rounded-3xl bg-base-100 bg-opacity-90 p-6 text-content-200 backdrop-blur-sm"
  >
    <div class="flex flex-col gap-2">
      <UserMedia :user="party.user" class="max-w-[12rem]" />

      <div class="flex items-center gap-1.5">
        <Coin :value="10000" />
        <Media icon="member" :label="String(party.troops)" />
      </div>

      <div>Status: {{ party.status }}</div>

      <div>Terrain: Plain</div>

      <Media icon="crosshair" :label="party.position.coordinates.join(' ')" />
    </div>
  </div>
</template>

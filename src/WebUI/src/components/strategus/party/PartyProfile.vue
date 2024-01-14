<script setup lang="ts">
import { type Party } from '@/models/strategus/party';

const { party } = defineProps<{ party: Party }>();
</script>

<template>
  <div
    class="w-[20rem] rounded-3xl bg-base-100 bg-opacity-90 p-6 text-content-200 backdrop-blur-sm"
  >
    <div class="flex flex-col gap-1">
      <div>{{ party.user.name }}</div>

      <Media icon="member" :label="String(party.troops)" />

      <Coin :value="10000" />

      <div>Status: {{ party.status }}</div>

      <Media icon="crosshair" :label="party.position.coordinates.join(' ')" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { clsx } from 'clsx';
import { LMarker, LIcon } from '@vue-leaflet/vue-leaflet';
import { SettlementType, type SettlementPublic } from '@/models/strategus/settlement';
import { positionToLatLng } from '@/utils/geometry';
import { settlementIconByType } from '@/services/strategus-service/settlement';
import { hexToRGBA } from '@/utils/color';

const { settlement } = defineProps<{ settlement: SettlementPublic }>();

const settlementMarkerStyle = computed(() => {
  const output = {
    ...settlementIconByType[settlement.type],
    baseClass: '',
    baseStyle: '',
  };

  switch (settlement.type) {
    case SettlementType.Town:
      output.baseClass = clsx('text-sm px-2 py-1.5 gap-2');
    case SettlementType.Castle:
      output.baseClass = clsx('text-xs px-1.5 py-1 gap-1.5');
    case SettlementType.Village:
      output.baseClass = clsx('text-2xs px-1 py-1 gap-1');
  }

  if (settlement.owner !== null && settlement.owner.clan !== null) {
    output.baseStyle = `background-color: ${hexToRGBA(settlement.owner.clan.primaryColor, 0.3)};`; // TODO: clan tag, clan banner, tweak
  }

  return output;
});
</script>

<template>
  <LMarker
    :latLng="positionToLatLng(settlement.position.coordinates)"
    :options="{ bubblingMouseEvents: false }"
  >
    <LIcon className="!flex justify-center items-center">
      <div
        :style="settlementMarkerStyle.baseStyle"
        class="flex items-center whitespace-nowrap rounded-md bg-base-100 bg-opacity-30 text-white hover:ring"
        :class="settlementMarkerStyle.baseClass"
        :title="$t(`strategus.settlementType.${settlement.type}`)"
      >
        <OIcon
          :icon="settlementMarkerStyle.icon"
          :size="settlementMarkerStyle.iconSize"
          class="self-baseline"
        />
        <div class="leading-snug">{{ settlement.name }}</div>
      </div>
    </LIcon>
  </LMarker>
</template>

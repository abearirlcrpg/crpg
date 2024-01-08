<script setup lang="ts">
import { getSettlementGarrisonItems } from '@/services/strategus-service/settlement';
import { settlementKey } from '@/symbols/strategus/settlement';

const route = useRoute<'StrategusSettlementIdGarrison'>();

const settlement = injectStrict(settlementKey);

//
const { state: garrisonItems, execute: loadGarrisonItems } = useAsyncState(
  () => getSettlementGarrisonItems(Number(route.params.id)),
  [],
  {
    immediate: false,
    resetOnExecute: false,
  }
);

await loadGarrisonItems();
</script>

<template>
  <div>
    <div class="flex items-center gap-1.5">
      <OIcon icon="member" size="lg" class="text-content-100" />
      <span class="text-content-200">
        {{ settlement!.troops }}
      </span>
    </div>

    <div>
      <h2>Garrison items: TODO: split screen with D&N?</h2>
      <div class="grid grid-cols-4 gap-2">
        <div v-for="gi in garrisonItems">
          <ItemCard :item="gi.item" />
          Count:{{ gi.count }}
        </div>
      </div>
    </div>
  </div>
</template>

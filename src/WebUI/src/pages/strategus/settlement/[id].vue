<script setup lang="ts">
import { getSettlement } from '@/services/strategus-service/settlement';
import { settlementIconByType } from '@/services/strategus-service/settlement';
import { itemCultureToIcon } from '@/services/item-service'; // TODO: cultrue service
import { settlementKey } from '@/symbols/strategus/settlement';

definePage({
  meta: {
    layout: 'default',
    roles: ['User', 'Moderator', 'Admin'],
    noFooter: true,
    fullPage: true,
  },
});

const route = useRoute<'StrategusSettlementId'>();

const { state: settlement, execute: loadSettlement } = useAsyncState(
  () => getSettlement(Number(route.params.id)),
  null,
  {
    immediate: false,
  }
);

provide(settlementKey, settlement);

await loadSettlement();
</script>

<template>
  <div
    class="flex max-h-[95%] w-1/2 flex-col space-y-8 overflow-hidden rounded-3xl border border-solid border-border-200 bg-base-100 p-6 text-content-200"
  >
    <div class="flex items-center gap-5 border-b border-border-200 pb-2">
      <!-- TODO: leave from city logic, API -->
      <!-- Exit gate/door icon -->
      <OButton
        v-tooltip.bottom="`Leave`"
        tag="router-link"
        :to="{ name: 'Strategus' }"
        variant="secondary"
        size="lg"
        outlined
        rounded
        icon-left="arrow-left"
      />

      <div class="flex items-center gap-2.5">
        <OIcon
          :icon="settlementIconByType[settlement!.type].icon"
          size="2xl"
          class="self-baseline"
        />
        <div class="flex flex-col gap-1">
          <div class="flex items-center gap-2">
            <div class="text-lg">{{ settlement!.name }}</div>
            <div class="flex items-center gap-1.5">
              <SvgSpriteImg
                :name="itemCultureToIcon[settlement!.culture]"
                viewBox="0 0 48 48"
                class="w-4"
              />
              <div class="text-2xs text-content-300">
                {{ $t(`strategus.settlementType.${settlement!.type}`) }}
              </div>
            </div>
          </div>

          <div class="flex items-center gap-1.5">
            <div v-if="settlement?.owner">
              <UserMedia :user="settlement.owner" class="max-w-[12rem]" />
            </div>
          </div>
        </div>
      </div>

      <div class="flex items-center gap-2">
        <RouterLink
          :to="{ name: 'StrategusSettlementId', params: { id: route.params.id } }"
          v-slot="{ isExactActive }"
        >
          <OButton
            :variant="isExactActive ? 'transparent-active' : 'transparent'"
            size="lg"
            :label="`Info`"
          />
        </RouterLink>

        <RouterLink
          :to="{ name: 'StrategusSettlementIdGarrison', params: { id: route.params.id } }"
          v-slot="{ isExactActive }"
        >
          <OButton
            :variant="isExactActive ? 'transparent-active' : 'transparent'"
            size="lg"
            :label="`Garrison`"
          />
        </RouterLink>
      </div>
    </div>

    <div class="h-full overflow-y-auto overflow-x-hidden">
      <RouterView />
    </div>
  </div>
</template>

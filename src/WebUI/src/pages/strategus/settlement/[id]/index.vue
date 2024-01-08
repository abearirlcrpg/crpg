<script setup lang="ts">
import { settlementKey } from '@/symbols/strategus/settlement';

const settlement = injectStrict(settlementKey);
</script>

<template>
  <div>
    <!-- TODO: -->
    <!-- <OButton
          variant="primary"
          size="lg"
          :label="
            party!.status !== PartyStatus.RecruitingInSettlement
              ? 'Start recruiting troops'
              : 'Stop recruiting troops'
          "
          :loading="isTogglingRecruitTroops"
          :disabled="isTogglingRecruitTroops"
          @click="$emit('toggleRecruitTroops')"
        /> -->

    <pre>{{ settlement }}</pre>
  </div>
</template>

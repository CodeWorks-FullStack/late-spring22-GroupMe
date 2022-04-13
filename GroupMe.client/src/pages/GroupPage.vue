<template>
  <div class="component">
    <h1>{{ group.name }}</h1>
    <h2>Members</h2>
    <ul>
      <li v-for="gm in groupMembers" :key="gm.id">{{ gm.name }}</li>
    </ul>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { groupsService } from '../services/GroupsService'
import { AppState } from '../AppState'
export default {
  setup() {
    // TODO get the group data from the url
    const route = useRoute()
    onMounted(() => {
      groupsService.setActive(route.params.id)
      groupsService.getMembers(route.params.id)
    })
    return {
      group: computed(() => AppState.activeGroup),
      groupMembers: computed(() => AppState.groupMembers),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
<template>
  <div class="home">
    <h1>All Groups</h1>
    <div v-for="g in groups" :key="g.id">
      <router-link :to="{ name: 'Group', params: { id: g.id } }">{{
        g.name
      }}</router-link>
    </div>
    {{ myGroups }}
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { groupsService } from '../services/GroupsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    // TODO get all groups
    onMounted(() => {
      groupsService.getAll()
    })
    return {
      groups: computed(() => AppState.groups),
      myGroups: computed(() => AppState.myGroups)
    }
  }
}
</script>

<style scoped lang="scss">
</style>

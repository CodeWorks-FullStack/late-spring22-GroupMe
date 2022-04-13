import { AppState } from "../AppState"
import { api } from "./AxiosService"

class GroupsService {
  async getAll() {
    const res = await api.get('api/groups')
    AppState.groups = res.data
  }
  async setActive(id) {
    const res = await api.get('api/groups/' + id)
    AppState.activeGroup = res.data
  }

  async getMembers(id) {
    const res = await api.get(`api/groups/${id}/memberships`)
    AppState.groupMembers = res.data
  }

  async getMyGroups() {
    const res = await api.get('account/groups')
    AppState.myGroups = res.data
  }
}

export const groupsService = new GroupsService()
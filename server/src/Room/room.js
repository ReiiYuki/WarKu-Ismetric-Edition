class Room {
  constructor(id,type,remote) {
    this.id = id
    this.type = type
    this.remotes = []
    this.addPlayer(remote)
    remote.responseCreateRoomSuccess(type,id)
  }

  addPlayer(remote){
    this.remotes.push(remote)
  }

}

module.exports = Room

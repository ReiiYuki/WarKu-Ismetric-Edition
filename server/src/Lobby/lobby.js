let Room = require('../Room/room')
class Lobby {
  constructor(){
    this.remotes = []
    this.room = []
    this.roomCounter = 1;
  }

  addRemote(remote){
    this.remotes.push(remote)
    remote.responseLoginSuccess()
  }

  removeRemote(remote){
    this.remotes.splice(this.remotes.indexOf(remote), 1)
  }

  createRoom(remote,type){
    let room = new Room(this.roomCounter++,type,remote)
    remote.room = room
    this.room.push(remote)
    this.removeRemote(remote)
  }
  
}

module.exports = Lobby

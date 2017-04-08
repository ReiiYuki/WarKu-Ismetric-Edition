let Room = require('../Room/room')
class Lobby {
  constructor(){
    this.remotes = []
    this.room = []
    this.roomCounter = 1;
  }

  addRemote(remote){
    remote.isReady = false
    this.remotes.push(remote)
    remote.responseLoginSuccess()
  }

  removeRemote(remote){
    this.remotes.splice(this.remotes.indexOf(remote), 1)
  }

  removeRoom(remote){
    let room = this.room.find((room)=>(room.remotes.indexOf(remote)>=0))
    room.end()
    room.remotes.forEach((remote)=>{
      this.addRemote(remote)
      remote.notifyKickedToLobby()
      remote.room = null
    })
    this.room.splice(this.room.indexOf(room),1)
  }

  joinRoom(remote,type){
    let rooms = this.room.filter((room)=>(room.remotes.length<2))
    if (rooms.length==0) {
      let room = new Room(this.roomCounter++,type,remote)
      remote.room = room
      this.room.push(room)
      this.print(room)
    }else {
      remote.room = rooms[0]
      rooms[0].addPlayer(remote)
      this.print(rooms[0])
    }
    this.removeRemote(remote)
  }

  print(room){
    room.remotes.forEach((remote,index)=>{
      console.log(index+" "+remote.getPeerName())
    })
  }

}

module.exports = Lobby

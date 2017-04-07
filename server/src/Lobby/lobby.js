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

  removeRemote(remote){
    
  }
}

module.exports = Lobby

let Board = require('./board')
class Room {
  constructor(id,type,remote) {
    this.id = id
    this.type = type
    this.board = new Board()
    this.remotes = []
    this.addPlayer(remote)
    remote.responseCreateRoomSuccess(type,id)
  }

  addPlayer(remote){
    this.remotes.push(remote)
  }

  sendBoard(){
    this.remotes[0].updateBoard(this.board.formatFloors(),this.board.formatUnits())
  }

  spawnUnit(remote,x,y,type){
    return this.board.spawnUnit(remote,x,y,type)
  }
}

module.exports = Room

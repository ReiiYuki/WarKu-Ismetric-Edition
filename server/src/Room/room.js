let Board = require('./board')
class Room {
  constructor(id,type,remote) {
    this.id = id
    this.type = type
    this.remotes = []
    this.addPlayer(remote)
    this.board = new Board(this.remotes)
    remote.responseCreateRoomSuccess(type,id)
  }

  addPlayer(remote){
    this.remotes.push(remote)
  }

  sendBoard(){
    this.remotes[0].updateBoard(this.board.formatFloors(),this.board.formatUnits())
  }

  spawnUnit(remote,x,y,type){
    this.board.spawnUnit(remote,x,y,type)
  }

  moveUnit(remote,x,y,direction){
    this.board.moveUnit(x,y,direction)
  }

  updateUnit(x,y){
    this.board.updateUnit(x,y)
  }

  changeDirection(x,y,direction){
    this.board.changeDirection(x,y,direction)
  }
}

module.exports = Room

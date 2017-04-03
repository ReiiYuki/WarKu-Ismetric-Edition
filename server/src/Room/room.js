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
    this.remotes[0].updateBoard(this.board.formatFloors(0))
    this.remotes[1].updateBoard(this.board.formatFloors(1))
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

  build(x,y,targetX,targetY){
    this.board.build(x,y,targetX,targetY)
  }

  hide(x,y){
    this.board.hide(x,y)
  }
}

module.exports = Room

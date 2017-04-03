let Board = require('./board')
class Room {
  constructor(id,type,remote) {
    this.id = id
    this.remotes = []
    this.addPlayer(remote)
    this.board = new Board(this.remotes)
  }

  addPlayer(remote){
    this.remotes.push(remote)
    if (this.remotes.length == 2){
      this.remotes[0].responseCreateRoomSuccess(this.id)
      this.remotes[1].responseCreateRoomSuccess(this.id)
    }
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

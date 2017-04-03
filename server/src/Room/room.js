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
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.spawnUnit(remote,x,y,type)
  }

  moveUnit(remote,x,y,direction){
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.moveUnit(x,y,direction)
  }

  updateUnit(remote,x,y){
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.updateUnit(x,y)
  }

  changeDirection(remote,x,y,direction){
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.changeDirection(x,y,direction)
  }

  build(remote,x,y,targetX,targetY){
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.build(x,y,targetX,targetY)
  }

  hide(remote,x,y){
    if (this.remotes.indexOf(remote)==1){
      x = this.inversePosition(x)
      y = this.inversePosition(y)
    }
    this.board.hide(x,y)
  }

  inversePosition(x) {
    return (x-15)*-1
  }
}

module.exports = Room

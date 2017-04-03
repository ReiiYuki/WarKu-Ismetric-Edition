let server = require('dgt-net').server
let packet = require('./packet')
const Lobby = require('./Lobby/lobby')

let lobby = new Lobby()
class RemoteProxy extends server.RemoteProxy {

//<editor-fold> connection
  onConnected() {
    console.log("RemoteProxy There is a connection from " + this.getPeerName())
  }

  onDisconnected() {
    console.log("RemoteProxy Disconnected from " + this.getPeerName())
  }
//</editor-fold>

//<editor-fold> Login
  login(name){
    this.name = name
    lobby.addRemote(this)
  }

  responseLoginSuccess(){
    this.send(packet.responseLoginSuccess())
  }
//</editor-fold>

//<editor-fold> Room & Board
  joinRoom(type){
    lobby.joinRoom(this,type)
  }

  requestBoard(){
    this.room.sendBoard()
  }

  responseCreateRoomSuccess(type,id){
    this.send(packet.responseCreateRoomSuccess(type,id))
  }

  updateBoard(floors){
    this.send(packet.updateBoard(floors))
  }

//</editor-fold>

//<editor-fold> Unit

  spawnUnit(x,y,type){
    this.room.spawnUnit(this,x,y,type)
  }

  moveUnit(x,y,direction){
    this.room.moveUnit(x,y,direction)
  }

  updateUnit(x,y,changeX,changeY,unit){
    this.send(packet.updateUnit(x,y,changeX,changeY,unit))
  }

  updateUnitR(x,y){
    this.room.updateUnit(x,y)
  }

  changeDirection(x,y,direction){
    this.room.changeDirection(x,y,direction)
  }

  build(x,y,targetX,targetY){
    this.room.build(x,y,targetX,targetY)
  }

  updateTile(x,y,type){
    this.send(packet.updateTile(x,y,type))
  }

  hide(x,y){
    this.room.hide(x,y)
  }
  //</editor-fold>

}
module.exports = RemoteProxy

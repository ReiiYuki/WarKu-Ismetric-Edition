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
    lobby.removeRoom(this)
    lobby.removeRemote(this)
    console.log("RemoteProxy Disconnected from " + this.getPeerName())
  }

  notifyKickedToLobby(){
    this.send(packet.notifyKickedToLobby())
  }

  cancelFindRoom(){
    lobby.removeRoom(this)
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

  responseCreateRoomSuccess(id){
    this.send(packet.responseCreateRoomSuccess(id))
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
    this.room.moveUnit(this,x,y,direction)
  }

  updateUnit(x,y,changeX,changeY,unit){
    this.send(packet.updateUnit(x,y,changeX,changeY,unit,this))
  }

  updateUnitR(x,y){
    this.room.updateUnit(this,x,y)
  }

  changeDirection(x,y,direction){
    this.room.changeDirection(this,x,y,direction)
  }

  build(x,y,targetX,targetY){
    this.room.build(this,x,y,targetX,targetY)
  }

  updateTile(x,y,type){
    this.send(packet.updateTile(x,y,type))
  }

  hide(x,y){
    this.room.hide(this,x,y)
  }
  //</editor-fold>

//<editor-fold> End Condition
  updateHp(hp,hpOp){
    this.send(packet.updateHp(hp,hpOp))
  }
//</editor-fold>
}
module.exports = RemoteProxy

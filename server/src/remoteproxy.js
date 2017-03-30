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
  createRoom(type){
    lobby.createRoom(this,type)
  }

  requestBoard(){
    this.room.sendBoard()
  }

  responseCreateRoomSuccess(type,id){
    this.send(packet.responseCreateRoomSuccess(type,id))
  }

  updateBoard(floors,units){
    this.send(packet.updateBoard(floors,units))
  }

//</editor-fold>

//<editor-fold> Unit
  spawnUnit(x,y,type){
    if (!this.room.spawnUnit(this,x,y,type)) type = -1
    this.responseSpawnUnit(x,y,type)
  }

  responseSpawnUnit(x,y,type){
    this.send(packet.spawnUnitResponse(x,y,type))
  }
}
//</editor-fold>
module.exports = RemoteProxy

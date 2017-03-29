let server = require('dgt-net').server
let packet = require('./packet')
const Lobby = require('./Lobby/lobby')

let lobby = new Lobby()
class RemoteProxy extends server.RemoteProxy {

  onConnected() {
    console.log("RemoteProxy There is a connection from " + this.getPeerName())
  }

  onDisconnected() {
    console.log("RemoteProxy Disconnected from " + this.getPeerName())
  }

  login(name){
    this.name = name
    lobby.addRemote(this)
  }

  responseLoginSuccess(){
    this.send(packet.responseLoginSuccess())
  }

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

  spawnUnit(x,y,type){
    if (!this.room.spawnUnit(this,x,y,type)) type = -1
    this.responseSpawnUnit(x,y,type)
  }

  responseSpawnUnit(x,y,type){
    this.send(packet.spawnUnitResponse(x,y,type))
  }
}

module.exports = RemoteProxy

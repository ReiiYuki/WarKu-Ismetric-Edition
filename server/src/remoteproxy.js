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
}

module.exports = RemoteProxy

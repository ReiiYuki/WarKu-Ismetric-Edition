let server = require('dgt-net').server
let packet = require('./packet')

class RemoteProxy extends server.RemoteProxy {

  onConnected() {
    console.log("RemoteProxy There is a connection from " + this.getPeerName())
  }

  onDisconnected() {
    console.log("RemoteProxy Disconnected from " + this.getPeerName())
  }

}

module.exports = RemoteProxy

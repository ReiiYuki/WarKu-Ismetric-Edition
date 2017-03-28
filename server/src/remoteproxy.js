import {server} from 'dgt-net'
import Packet from './packet'

class RemoteProxy extends server.RemoteProxy {

  onConnected() {
    console.log("RemoteProxy There is a connection from " + this.getPeerName())
    room.addRemote(this)
  }

  onDisconnected() {
    console.log("RemoteProxy Disconnected from " + this.getPeerName())
    room.removeRemote(this)
  }

}

export default RemoteProxy

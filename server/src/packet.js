let packetWriter = require('dgt-net').packet_writer

let packet = {
  CLIENT_LOGIN : 10000,
  CLIENT_DISCONNECT : 10001
}

packet[packet.CLIENT_LOGIN] = function(remote,data) {
  let name = data.read_string()
  if (!data.completed()) return true
  remote.login(name)
}

module.exports = packet

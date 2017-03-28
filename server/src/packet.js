let packetWriter = require('dgt-net').packet_writer

let packet = {
  CLIENT_LOGIN : 10000,
  CLIENT_DISCONNECT : 10001,

  SERVER_LOGIN_SUCCESS : 20000
}

packet[packet.CLIENT_LOGIN] = (remote,data) => {
  let name = data.read_string()
  if (!data.completed()) return true
  remote.login(name)
}

packet.responseLoginSuccess = () => {
  let pw = new packetWriter(packet.SERVER_LOGIN_SUCCESS)
  pw.finish();
  return pw.buffer
}

module.exports = packet

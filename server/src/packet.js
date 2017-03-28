let packetWriter = require('dgt-net').packet_writer

let packet = {
  CLIENT_LOGIN : 10000,
  CLIENT_DISCONNECT : 10001,
  CLIENT_CREATE_ROOM : 10002,

  SERVER_LOGIN_SUCCESS : 20000,
  SERVER_CREATE_ROOM_SUCCESS : 20001
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


packet[packet.CLIENT_CREATE_ROOM] = (remote,data) =>{
  let type = data.read_uint8()
  if (!data.completed()) return true
  remote.createRoom(type)
}

packet.responseCreateRoomSuccess = (type,id) =>{
  let pw = new packetWriter(packet.SERVER_CREATE_ROOM_SUCCESS)
  pw.append_uint8(type)
  pw.append_uint32(id)
  pw.finish()
  return pw.buffer
}

module.exports = packet

let packetWriter = require('dgt-net').packet_writer

//<editor-fold> PACKET ID
let packet = {
  CLIENT_LOGIN : 10000,
  CLIENT_DISCONNECT : 10001,
  CLIENT_CREATE_ROOM : 10002,
  CLIENT_REQUEST_BOARD : 10003,
  CLIENT_SPAWN_UNIT : 10004,
  CLIENT_UPDATE_UNIT : 10005,
  CLIENT_CHANGE_UNIT_DIRECTION : 10006,

  SERVER_LOGIN_SUCCESS : 20000,
  SERVER_CREATE_ROOM_SUCCESS : 20001,
  SERVER_UPDATE_BOARD : 20002,
  SERVER_UPDATE_UNIT : 20003,
}
//</editor-fold>

//<editor-fold> LOGIN
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
//</editor-fold>

//<editor-fold> Room
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
//</editor-fold>

//<editor-fold> Board
packet.updateBoard = (floors)=>{
  let pw = new packetWriter(packet.SERVER_UPDATE_BOARD)
  pw.append_string(floors)
  pw.finish()
  return pw.buffer
}

packet[packet.CLIENT_REQUEST_BOARD] = (remote,data) =>{
  remote.requestBoard()
}
//</editor-fold>

//<editor-fold> Unit
packet[packet.CLIENT_SPAWN_UNIT] = (remote,data) => {
  let x = data.read_uint8()
  let y = data.read_uint8()
  let type = data.read_uint8()
  remote.spawnUnit(x,y,type)
}

packet[packet.CLIENT_UPDATE_UNIT] = (remote,data)=>{
  let x = data.read_uint8()
  let y = data.read_uint8()
  remote.updateUnitR(x,y)
}

packet.updateUnit = (x,y,changeX,changeY,unit) => {
  let pw = new packetWriter(packet.SERVER_UPDATE_UNIT)

  pw.append_uint8(x)
  pw.append_uint8(y)
  pw.append_uint8(changeX)
  pw.append_uint8(changeY)
  if (unit){
    pw.append_int8(unit.type)
    pw.append_uint8(unit.direction)
  }else {
    pw.append_int8(-1)
  }
  pw.finish()
  return pw.buffer
}
packet[packet.CLIENT_CHANGE_UNIT_DIRECTION] = (remote,data) => {

}
//</editor-fold>
module.exports = packet

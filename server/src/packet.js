let packetWriter = require('dgt-net').packet_writer

//<editor-fold> PACKET ID
let packet = {
  CLIENT_PING: 1000,
  SERVER_PING_SUCCESS : 2000,

  CLIENT_LOGIN : 10000,
  CLIENT_DISCONNECT : 10001,
  CLIENT_JOIN_ROOM : 10002,
  CLIENT_REQUEST_BOARD : 10003,
  CLIENT_SPAWN_UNIT : 10004,
  CLIENT_UPDATE_UNIT : 10005,
  CLIENT_CHANGE_UNIT_DIRECTION : 10006,
  CLIENT_WORKER_UNIT_BUILD : 10007,
  CLIENT_UNIT_HIDE : 10008,
  CLIENT_CANCEL_WAITING_QUEUE : 10009,
  CLIENT_READY : 10010,

  SERVER_LOGIN_SUCCESS : 20000,
  SERVER_JOIN_ROOM_SUCCESS : 20001,
  SERVER_UPDATE_BOARD : 20002,
  SERVER_UPDATE_UNIT : 20003,
  SERVER_UPDATE_TILE : 20004,
  SERVER_NOTIFY_KICK_ROOM : 20005,
  SERVER_UPDATE_HP : 20006,
  SERVER_UPDATE_TIME : 20007,
  SERVER_NOTIFY_START : 20008,
  SERVER_SHOW_RESULT : 20009
}
//</editor-fold>

//<editor-fold> PING
packet[packet.CLIENT_PING] = function (remoteProxy, data) {
  var pingTime = data.read_uint8();
  if (!data.completed()) return true;
  remoteProxy.ping(pingTime);
}
packet.make_ping_success = function (ping_time) {
  var o = new packet_writer(packet.SERVER_PING_SUCCESS);
  o.append_uint8(ping_time);
  o.finish();
  return o.buffer;
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
packet[packet.CLIENT_JOIN_ROOM] = (remote,data) =>{
  let type = data.read_uint8()
  if (!data.completed()) return true
  remote.joinRoom(type)
}

packet.responseCreateRoomSuccess = (id) =>{
  let pw = new packetWriter(packet.SERVER_JOIN_ROOM_SUCCESS)
  pw.append_uint32(id)
  pw.finish()
  return pw.buffer
}

packet[packet.CLIENT_CANCEL_WAITING_QUEUE] = (remote,data) => {
  remote.cancelFindRoom()
}

packet.notifyKickedToLobby = ()=>{
  let pw = new packetWriter(packet.SERVER_NOTIFY_KICK_ROOM)
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

packet.updateUnit = (x,y,changeX,changeY,unit,remote,status) => {
  let pw = new packetWriter(packet.SERVER_UPDATE_UNIT)
  pw.append_uint8(x)
  pw.append_uint8(y)
  pw.append_uint8(changeX)
  pw.append_uint8(changeY)
  if (unit){
    pw.append_int8(unit.type)
    pw.append_uint8(unit.getDirection(remote))
    pw.append_float(unit.hp)
    if (unit.isHide) pw.append_uint8(1)
    else pw.append_uint8(0)
    if (unit.isOwner(remote)) pw.append_uint8(1)
    else pw.append_uint8(0)
    pw.append_uint8(status)
  }else {
    pw.append_int8(-1)
    pw.append_uint8(status)
  }
  pw.finish()
  return pw.buffer
}
packet[packet.CLIENT_CHANGE_UNIT_DIRECTION] = (remote,data) => {
  let x = data.read_uint8()
  let y = data.read_uint8()
  let direction = data.read_uint8()
  remote.changeDirection(x,y,direction)
}

packet[packet.CLIENT_WORKER_UNIT_BUILD] = (remote,data) => {
  let x = data.read_uint8()
  let y = data.read_uint8()
  let targetX = data.read_uint8()
  let targetY = data.read_uint8()
  remote.build(x,y,targetX,targetY)
}
packet.updateTile = (x,y,type) => {
  let pw = new packetWriter(packet.SERVER_UPDATE_TILE)
  pw.append_uint8(x)
  pw.append_uint8(y)
  pw.append_uint8(type)
  pw.finish()
  return pw.buffer
}

packet[packet.CLIENT_UNIT_HIDE] = (remote,data) => {
  let x = data.read_uint8()
  let y = data.read_uint8()
  remote.hide(x,y)
}
//</editor-fold>

//<editor-fold> Exit Condition
packet.updateHp = (hp,opHp)=>{
  let pw = new packetWriter(packet.SERVER_UPDATE_HP)
  pw.append_float(hp)
  pw.append_float(opHp)
  pw.finish();
  return pw.buffer
}
packet.updateTime = (time) => {
  let pw = new packetWriter(packet.SERVER_UPDATE_TIME)
  pw.append_uint8(time)
  pw.finish()
  return pw.buffer
}
packet[packet.CLIENT_READY] = (remote,data) =>{
  remote.ready()
}
packet.notifyStart = ()=>{
  let pw = new packetWriter(packet.SERVER_NOTIFY_START)
  pw.finish()
  return pw.buffer
}

packet.showResult = (result) => {
  let pw = new packetWriter(packet.SERVER_SHOW_RESULT)
  pw.append_uint8(result)
  pw.finish()
  return pw.buffer
}
//</editor-fold>
module.exports = packet

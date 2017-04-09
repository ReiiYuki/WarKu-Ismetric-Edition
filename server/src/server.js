let server = require('dgt-net').server
let packet = require('./packet')
let remoteProxy = require('./remoteproxy')

const PORT = 1111

server.setRemoteProxyClass(remoteProxy)
server.setPacketObject(packet)
server.listen(PORT)

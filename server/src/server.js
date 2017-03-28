import {server} from 'dgt-net'
import Packet from './packet'
import RemoteProxy from './remoteproxy'

const PORT = 1111

server.setRemoteProxyClass(RemoteProxy)
server.setPacketObject(Packet)
server.listen(PORT)

export default server

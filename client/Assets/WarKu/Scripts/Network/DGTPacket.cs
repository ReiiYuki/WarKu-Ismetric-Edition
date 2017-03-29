using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGTPacket : PacketManager {

    #region config
    public class Config
    {
        public string host;
        public int port;

        public Config (string host,int port)
        {
            this.host = host;
            this.port = port;
        }
    }
    #endregion

    #region id
    private enum PacketID
    {
        CLIENT_LOGIN = 10000,
        CLIENT_DISCONNECT = 10001,
        CLIENT_CREATE_ROOM = 10002,
        CLIENT_REQUEST_BOARD = 10003,
        CLIENT_SPAWN_UNIT = 10004,

        SERVER_LOGIN_SUCCESS = 20000,
        SERVER_CREATE_ROOM_SUCCESS = 20001,
        SERVER_UPDATE_BOARD = 20002,
        SEREVER_SPAWN_UNIT_RESPONSE = 20003,
        SERVER_UPDATE_UNIT = 20004
    }
    #endregion

    #region initialize
    private DGTProxyRemote remote;

    public DGTPacket (DGTProxyRemote remote) : base()
    {
        this.remote = remote;
        PacketMapper();
    }
    #endregion

    #region connection
    protected override void OnConnected()
    {
        remote.OnConnected();
    }

    protected override void OnDisconnected()
    {
        remote.OnDisconnected();
    }

    protected override void OnFailed()
    {
        remote.OnFailed();
    }
    #endregion

    #region packet mapper
    private void PacketMapper()
    {
        _Mapper[(int)PacketID.SERVER_LOGIN_SUCCESS] = ReceiveLoggedInResponse;
        _Mapper[(int)PacketID.SERVER_CREATE_ROOM_SUCCESS] = ReceiveCreatedRoomResponse;
        _Mapper[(int)PacketID.SERVER_UPDATE_BOARD] = UpdateBoard;
        _Mapper[(int)PacketID.SEREVER_SPAWN_UNIT_RESPONSE] = OnSpawn;
    }
    #endregion

    #region login/logout
    public void Login(string name)
    {
        PacketWriter packetWriter = BeginSend((int)PacketID.CLIENT_LOGIN);
        packetWriter.WriteString(name);
        EndSend();
    }
    private void ReceiveLoggedInResponse(int packet_id,PacketReader pr)
    {
        DGTProxyRemote.GetInstance().OnLoggedInSuccess();
    }
    #endregion

    #region room
    public void CreateRoom(int type)
    {
        PacketWriter packetWriter = BeginSend((int)PacketID.CLIENT_CREATE_ROOM);
        packetWriter.WriteUInt8(type);
        EndSend();
    }
    public void ReceiveCreatedRoomResponse(int packet_id, PacketReader pr)
    {
        int type = pr.ReadUInt8();
        int id = pr.ReadUInt32();
        DGTProxyRemote.GetInstance().OnCreatedRoom(id, type);
    }
    #endregion

    #region board
    public void UpdateBoard(int packed_id,PacketReader pr)
    {
        string boardFloorsStr = pr.ReadString();
        string boardUnitsStr = pr.ReadString();
        DGTProxyRemote.GetInstance().OnUpdateBoard(boardFloorsStr, boardUnitsStr);
    }
    public void RequestBoard()
    {
        PacketWriter packetWriter = BeginSend((int)PacketID.CLIENT_REQUEST_BOARD);
        EndSend();
    }
    #endregion

    #region unit
    public void SpawnUnitRequest(int x,int y,int type)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_SPAWN_UNIT);
        pw.WriteUInt8(x);
        pw.WriteUInt8(y);
        pw.WriteUInt8(type);
        EndSend();
    }

    public void OnSpawn(int packed_id, PacketReader pr)
    {
        int x = pr.ReadUInt8();
        int y = pr.ReadUInt8();
        int type = pr.ReadInt8();
        DGTProxyRemote.GetInstance().OnSpawnUnit(x, y, type);
    }
    #endregion 
}

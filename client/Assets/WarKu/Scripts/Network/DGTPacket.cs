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
        CLIENT_PING = 1000,
        SERVER_PING_SUCCESS = 2000,

        CLIENT_LOGIN = 10000,
        CLIENT_DISCONNECT = 10001,
        CLIENT_CREATE_ROOM = 10002,
        CLIENT_REQUEST_BOARD = 10003,
        CLIENT_SPAWN_UNIT = 10004,
        CLIENT_UPDATE_UNIT = 10005,
        CLIENT_CHANGE_UNIT_DIRECTION = 10006,
        CLIENT_WORKER_UNIT_BUILD = 10007,
        CLIENT_UNIT_HIDE = 10008,
        CLIENT_CANCEL_WAITING_QUEUE = 10009,
        CLIENT_READY = 10010,

        SERVER_LOGIN_SUCCESS = 20000,
        SERVER_CREATE_ROOM_SUCCESS = 20001,
        SERVER_UPDATE_BOARD = 20002,
        SERVER_UPDATE_UNIT = 20003,
        SERVER_UPDATE_TILE = 20004,
        SERVER_NOTIFY_KICK_ROOM = 20005,
        SERVER_UPDATE_HP = 20006,
        SERVER_UPDATE_TIME = 20007,
        SERVER_NOTIFY_START = 20008,
        SERVER_SHOW_RESULT = 20009

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
        _Mapper[(int)PacketID.SERVER_UPDATE_UNIT] = OnUpdateUnit;
        _Mapper[(int)PacketID.SERVER_UPDATE_TILE] = OnUpdateTile;
        _Mapper[(int)PacketID.SERVER_NOTIFY_KICK_ROOM] = OnCancelRoom;
        _Mapper[(int)PacketID.SERVER_UPDATE_HP] = UpdateHP;
        _Mapper[(int)PacketID.SERVER_UPDATE_TIME] = UpdateTime;
        _Mapper[(int)PacketID.SERVER_NOTIFY_START] = NotifyStart;
        _Mapper[(int)PacketID.SERVER_SHOW_RESULT] = OnResult;
    }
    #endregion

    #region Room Update Status
    public void Ready()
    {
        PacketWriter packetWriter = BeginSend((int)PacketID.CLIENT_READY);
        EndSend();
    }

    public void UpdateHP(int packet_id, PacketReader pr)
    {
        int hp = pr.ReadUInt8();
        int opHp = pr.ReadInt8();
        DGTProxyRemote.GetInstance().UpdateHP(hp, opHp);
    }

    public void UpdateTime(int packet_id, PacketReader pr)
    {
        int time = pr.ReadUInt8();
        DGTProxyRemote.GetInstance().UpdateTime(time);
    }

    public void NotifyStart(int packet_id, PacketReader pr)
    {
        DGTProxyRemote.GetInstance().NotifyStart();
    }

    public void OnResult(int packet_id, PacketReader pr)
    {
        int result = pr.ReadUInt8();
        DGTProxyRemote.GetInstance().OnResult(result);
    }
    #endregion
    #region ping
    public void RequestPing(int pingTime)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_PING);
        pw.WriteInt8(pingTime);
        EndSend();
    }

    private void RecvPingSuccess(int packet_id, PacketReader pr)
    {
        int pingTime = pr.ReadUInt8();
        Debug.Log("ping : " + pingTime);
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
        int id = pr.ReadUInt32();
        DGTProxyRemote.GetInstance().OnCreatedRoom(id);
    }

    public void CancelRoom()
    {
        PacketWriter packetWriter = BeginSend((int)PacketID.CLIENT_CANCEL_WAITING_QUEUE);
        EndSend();
    }
    public void OnCancelRoom(int packet_id, PacketReader pr)
    {
        DGTProxyRemote.GetInstance().OnCancelRoom();
    }
    #endregion

    #region board
    public void UpdateBoard(int packed_id,PacketReader pr)
    {
        string boardFloorsStr = pr.ReadString();
        DGTProxyRemote.GetInstance().OnUpdateBoard(boardFloorsStr);
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

    public void OnUpdateUnit(int packed_id, PacketReader pr)
    {
        int x = pr.ReadUInt8(); 
        int y = pr.ReadUInt8();
        int changeX = pr.ReadUInt8();
        int changeY = pr.ReadUInt8();
        int type = pr.ReadInt8();
        int status = 0;
        if (type != -1)
        {
            int direction = pr.ReadUInt8();
            float hp = pr.ReadFloat();
            bool isHide = pr.ReadUInt8() == 1;
            bool isOwner = pr.ReadUInt8() == 1;
            status = pr.ReadUInt8();
            DGTProxyRemote.GetInstance().OnUpdateUnit(x, y,changeX,changeY ,type,direction,hp,isHide,isOwner,status);
            return;
        }else
        {
            status = pr.ReadUInt8();
        }
        DGTProxyRemote.GetInstance().OnUpdateUnit(x, y,changeX,changeY, type,0,0,false,false,status);
    }

    public void UpdateUnitRequest(int x,int y)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_UPDATE_UNIT);
        pw.WriteUInt8(x);
        pw.WriteUInt8(y);
        EndSend();
    }

    public void ChangeDirectionRequest(int x,int y,int direction)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_CHANGE_UNIT_DIRECTION);
        pw.WriteUInt8(x);
        pw.WriteUInt8(y);
        pw.WriteUInt8(direction);
        EndSend();
    }
    #endregion

    #region worker unit
    public void BuildRequest(int x,int y,int targetX,int targetY)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_WORKER_UNIT_BUILD);
        pw.WriteUInt8(x);
        pw.WriteUInt8(y);
        pw.WriteUInt8(targetX);
        pw.WriteUInt8(targetY);
        EndSend();
    }

    public void OnUpdateTile(int packed_id, PacketReader pr)
    {
        int x = pr.ReadUInt8();
        int y = pr.ReadUInt8();
        int type = pr.ReadUInt8();
        DGTProxyRemote.GetInstance().OnUpdateTile(x, y, type);
    }

    public void Hide(int x,int y)
    {
        PacketWriter pw = BeginSend((int)PacketID.CLIENT_UNIT_HIDE);
        pw.WriteUInt8(x);
        pw.WriteUInt8(y);
        EndSend();
    }
    #endregion
}

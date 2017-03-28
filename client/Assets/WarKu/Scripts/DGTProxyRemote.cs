using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGTProxyRemote : MonoBehaviour {

    #region state
    private enum State
    {
        DISCONNECTED = 0,
        DISCONNECTING,
        CONNECTED,
        CONNECTING
    }

    void SetState(State state)
    {
        this.state = state;
    }
    #endregion

    #region attribute
    private State state;
    private DGTPacket packet;
    private static DGTProxyRemote instance;
    #endregion

    #region singleton
    public static DGTProxyRemote GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<DGTProxyRemote>();
            DontDestroyOnLoad(instance.gameObject);
        }
        return instance;
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            packet = new DGTPacket(this);
            SetState(State.DISCONNECTED);
            DontDestroyOnLoad(this);
        }else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }
    #endregion

    #region Connection
    public void Connect(string host,int port)
    {
        if (state != State.DISCONNECTED) return;
        SetState(State.CONNECTING);
        packet.Connect(host, port);
    }

    public void Disconnect()
    {
        if (state != State.CONNECTED) return;
        SetState(State.DISCONNECTING);
        packet.Disconnect();
    }

    public void OnConnected()
    {
        SetState(State.CONNECTED);
    }

    public void OnDisconnected()
    {
        if (state != State.DISCONNECTED) return;
        SetState(State.DISCONNECTED);
    }

    public void OnFailed()
    {
        if (state != State.DISCONNECTED) return;
        SetState(State.DISCONNECTED);
    }

    public bool IsConnected()
    {
        return packet.Connected && state == State.CONNECTED;
    }

    public bool IsConnectionFailed()
    {
        return packet.Failed;
    }

    public void ProcessEvents()
    {
        packet.ProcessEvents();
    }
    #endregion

    #region login/logout
    public void Login(string name)
    {
        packet.Login(name);
    }
    #endregion
}

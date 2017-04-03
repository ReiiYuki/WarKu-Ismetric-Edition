//#define ENABLE_ENCRYPT_PACKET
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

public class PacketConnection
{
	private Socket _Socket = null;
	private PacketListener _PacketListener = null;
	private InternalPacketWriter _PacketWriter = null;
	private const int RECV_BUFF_SIZE = 4096;
	private const int SEND_BUFF_SIZE = 4096;
	private const int HEAD_BUFF_SIZE = 4;
	private byte[] _RecvBuff = new byte [RECV_BUFF_SIZE];
	private byte[] _SendBuff = new byte [SEND_BUFF_SIZE];
	private IAsyncResult _SendAsyncResult = null;
	private int _SendSize = 0;
	private byte[] _SendHeadBuff = new byte [HEAD_BUFF_SIZE];
	private byte[] _RecvHeadBuff = new byte [HEAD_BUFF_SIZE];
	private int _RecvHeadSize = 0;
	private byte[] _PacketBuff = null;
	private int _PacketSize = 0;
	private uint _SendKey, _RecvKey;
	private const uint SEND_SEED = 0x7F;
	private const uint SEND_K1 = 3;
	private const uint SEND_K2 = 7;
	private const uint RECV_SEED = 0x7F;
	private const uint RECV_K1 = 1;
	private const uint RECV_K2 = 1;

	private enum State
	{
		DISCONNECTED,
		CONNECTING,
		CONNECTED,
		DISCONNECTING,
	}
	private volatile State _State = State.DISCONNECTED;
	private volatile bool _Failed = false;
	private Queue<Task> _EvenQueue = new Queue<Task> ();

	private delegate void EventCallback ();

	private delegate void DataCallback (int packet_id,byte[] content);

	private interface Task
	{
		void Run ();
	}

	private class EventTask : Task
	{
		public EventTask (EventCallback cb)
		{
			_Callback = cb;
		}

		public void Run ()
		{
			_Callback ();
		}

		private EventCallback _Callback;
	}

	private class DataTask : Task
	{
		private DataCallback _Callback;
		private int _PacketId;
		private byte[] _Content;

		public DataTask (DataCallback cb, int id, byte[] data)
		{
			_Callback = cb;
			_PacketId = id;
			_Content = data;
		}

		public void Run ()
		{
			_Callback (_PacketId, _Content);
		}
	}

	private class InternalPacketWriter : PacketWriter
	{
		private byte[] _Buffer;
		private int _Length;

		public byte[] GetBuffer ()
		{
			return _Buffer;
		}

		public int GetLength ()
		{
			return _Length;
		}

		public InternalPacketWriter ()
		{
			_Buffer = null;
			_Length = 0;
		}

		private void CheckSize (int l)
		{
			int req = _Length + l;
			if (_Buffer == null || req > _Buffer.Length) {
				Array.Resize (ref _Buffer, req);
			}
		}

		public void WriteUInt (uint v, int l)
		{
			CheckSize (l);
			for (int i = 0; i < l; ++i) {
				_Buffer [_Length++] = (byte)v;
				v >>= 8;
			}
		}

		public void WriteULong (ulong v)
		{
			int l = 8;
			CheckSize (l);
			for (int i = 0; i < l; ++i) {
				_Buffer [_Length++] = (byte)v;
				v >>= 8;
			}
		}

		public void WriteInt (int v, int l)
		{
			CheckSize (l);
			for (int i = 0; i < l; ++i) {
				_Buffer [_Length++] = (byte)v;
				v >>= 8;
			}
		}

		public void WriteLong (long v)
		{
			int l = 8;
			CheckSize (l);
			for (int i = 0; i < l; ++i) {
				_Buffer [_Length++] = (byte)v;
				v >>= 8;
			}
		}

		public void WriteString (string v)
		{
			WriteString (v, -1);
		}

		public void WriteString (string v, int length)
		{
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding ("utf-8");
			byte[] content = enc.GetBytes (v);

			int size = content.Length;
			if (length >= 0 && content.Length > length) {
				size = length;
			}

			WriteData (content, size);
			WriteUInt (0, 1);
		}

		public void WriteData (byte[] v, int ofs, int len)
		{
			CheckSize (len);
			Buffer.BlockCopy (v, ofs, _Buffer, _Length, len);
			_Length += len;
		}

		public void WriteData (byte[] v, int len)
		{
			WriteData (v, 0, len);
		}

		public void Clear ()
		{
			_Length = 0;
		}

		public void WriteUInt8 (int v)
		{
			WriteUInt ((uint)v, 1);
		}

		public void WriteUInt16 (int v)
		{
			WriteUInt ((uint)v, 2);
		}

		public void WriteUInt32 (int v)
		{
			WriteUInt ((uint)v, 4);
		}

		public void WriteUInt64 (long v)
		{
			WriteULong ((ulong)v);
		}

		public void WriteInt8 (int v)
		{
			WriteInt (v, 1);
		}

		public void WriteInt16 (int v)
		{
			WriteInt (v, 2);
		}

		public void WriteInt32 (int v)
		{
			WriteInt (v, 4);
		}

		public void WriteInt64 (long v)
		{
			WriteLong (v);
		}

		public void WriteFloat(float v)
		{
			CheckSize (4);
			byte[] bytes = BitConverter.GetBytes(v);
			if (!BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			_Buffer[_Length++] = bytes[0];
			_Buffer[_Length++] = bytes[1];
			_Buffer[_Length++] = bytes[2];
			_Buffer[_Length++] = bytes[3];
		}

		public void WriteDouble(double v)
		{
			CheckSize (8);
			byte[] bytes = BitConverter.GetBytes(v);
			if (!BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			_Buffer[_Length++] = bytes[0];
			_Buffer[_Length++] = bytes[1];
			_Buffer[_Length++] = bytes[2];
			_Buffer[_Length++] = bytes[3];
			_Buffer[_Length++] = bytes[4];
			_Buffer[_Length++] = bytes[5];
			_Buffer[_Length++] = bytes[6];
			_Buffer[_Length++] = bytes[7];
		}
	}

	public PacketConnection ()
	{
	}

	public void Connect (string host, int port)
	{
		if (_State != State.DISCONNECTED)
			throw new InvalidOperationException ();

		try {
			_Failed = false;
			_State = State.CONNECTING;
			_Socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_Socket.BeginConnect (host, port, new AsyncCallback (ConnectCallback), this);

			#if ENABLE_ENCRYPT_PACKET
        _SendKey = SEND_SEED;
        _RecvKey = RECV_SEED;
#endif
		} catch (Exception) {
			_Failed = true;
			_State = State.DISCONNECTED;
			SaveEvent (EventConnectionFailed);
		}
	}

	public void Disconnect ()
	{
		while (_State == State.CONNECTING)
			Thread.Sleep (100);

		if (_State == State.CONNECTED) {
			try {
				_Socket.Shutdown (SocketShutdown.Send);
			} catch (Exception) {
				_Failed = true;
				_State = State.DISCONNECTED;
				SaveEvent (EventConnectionFailed);
			}
      
			PostDisconnect ();
		}
	}

	public void ProcessEvents ()
	{
		/*
    while (true)
    {
      Task t;
      lock (_EvenQueue)
      {
        if (_EvenQueue.Count > 0)
          t = _EvenQueue.Dequeue();
        else
          break;
      }
      t.Run();
    }
    */
		lock (_EvenQueue) {
			if (_EvenQueue.Count > 0) {
				Task t = _EvenQueue.Dequeue ();
				t.Run ();
			}
		}
	
	}

	public void SendPacket (int packet_id, byte[] content)
	{
		SendPacket (packet_id, content, 0, content.Length);
	}

	public void SendPacket (int packet_id, byte[] content, int offset, int len)
	{
		lock (_SendBuff) {
			CheckSend (HEAD_BUFF_SIZE + len);

			_SendHeadBuff [0] = (byte)(len);
			_SendHeadBuff [1] = (byte)(len >> 8);
			_SendHeadBuff [2] = (byte)(packet_id);
			_SendHeadBuff [3] = (byte)(packet_id >> 8);
			AppendSend (_SendHeadBuff, 0, HEAD_BUFF_SIZE);

			AppendSend (content, offset, len);

			Send ();
		}
	}

	public PacketWriter BeginSend (int packet_id)
	{
		if (_PacketWriter == null)
			_PacketWriter = new InternalPacketWriter ();

		_SendHeadBuff [2] = (byte)(packet_id);
		_SendHeadBuff [3] = (byte)(packet_id >> 8);

		return _PacketWriter;
	}

	public void EndSend ()
	{
		if (_PacketWriter != null) {
			int len = _PacketWriter.GetLength ();

			lock (_SendBuff) {
				CheckSend (HEAD_BUFF_SIZE + len);

				_SendHeadBuff [0] = (byte)(len);
				_SendHeadBuff [1] = (byte)(len >> 8);
				AppendSend (_SendHeadBuff, 0, HEAD_BUFF_SIZE);

				if (_PacketWriter.GetBuffer () != null) 
				{
					AppendSend (_PacketWriter.GetBuffer (), 0, len);
				}

				Send ();
			}

			_PacketWriter.Clear ();
		}
	}

	private void CheckSend (int l)
	{
		int req_size = _SendSize + l;
		if (req_size > _SendBuff.Length) {
			int ns = _SendBuff.Length;
			while (req_size > ns)
				ns += SEND_BUFF_SIZE;
			Array.Resize<byte> (ref _SendBuff, ns);
		}
	}

	private void AppendSend (byte[] content, int ofs, int len)
	{
		Buffer.BlockCopy (content, ofs, _SendBuff, _SendSize, len);
		// encrypt send buffer
#if ENABLE_ENCRYPT_PACKET
    for (int i = 0; i < len; ++i)
    {
      _SendKey = _SendKey * SEND_K1 + SEND_K2;
      _SendBuff[_SendSize + i] ^= (byte)_SendKey;
    }
#endif
		_SendSize += len;
	}

	private void Send ()
	{
		try {
			if (_SendAsyncResult == null) {  
				_SendAsyncResult = _Socket.BeginSend (_SendBuff, 0, _SendSize, SocketFlags.None, new AsyncCallback (SendCallback), this);
				bool success = _SendAsyncResult.AsyncWaitHandle.WaitOne (5000, false);
				if (!success)
					throw new SocketException (10060);
			}
		} catch (Exception) {
			UnityEngine.Debug.Log ("---------------------------------- send fail");
			SaveEvent (EventConnectionLost);
			_State = State.DISCONNECTED;
			PostDisconnect ();
		}
	}

	private void ConnectCallback (IAsyncResult ar)
	{
		try {
			_Socket.EndConnect (ar);
			SaveEvent (EventConnectionMade);
			PostReceive ();
			_State = State.CONNECTED;
		} catch (Exception) {
			_Failed = true;
			_State = State.DISCONNECTED;
			SaveEvent (EventConnectionFailed);
		}
	}

	private void SendCallback (IAsyncResult ar)
	{
		try {
			lock (_SendBuff) {
				_SendAsyncResult = null;
				int r = _Socket.EndSend (ar);
				_SendSize -= r;
				if (_SendSize > 0) {
					Buffer.BlockCopy (_SendBuff, r, _SendBuff, 0, _SendSize);
					_SendAsyncResult = _Socket.BeginSend (_SendBuff, 0, _SendSize, SocketFlags.None, new AsyncCallback (SendCallback), this);
				}
			}
		} catch (Exception) {
			_State = State.DISCONNECTED;
			SaveEvent (EventConnectionLost);
			_Socket.Shutdown (SocketShutdown.Send);
			PostDisconnect ();
		}
	}

	private void ReceiveCallback (IAsyncResult ar)
	{
		try {
			int r = _Socket.EndReceive (ar);
			if (r > 0) {
				int r_off = 0;
				int r_len = r;
				// decrypt recv buffer
				#if ENABLE_ENCRYPT_PACKET
			        for (int i = 0; i < r_len; ++i)
			        {
			          _RecvKey = _RecvKey * RECV_K1 + RECV_K2;
			          _RecvBuff[i] ^= (byte)_RecvKey;
			        }
				#endif

				while (true) {
					if (_RecvHeadSize < HEAD_BUFF_SIZE) {
						int head_needed = HEAD_BUFF_SIZE - _RecvHeadSize;
						if (r_len < head_needed)
							head_needed = r_len;
						Buffer.BlockCopy (_RecvBuff, r_off, _RecvHeadBuff, _RecvHeadSize, head_needed);
						_RecvHeadSize += head_needed;
						r_off += head_needed;
						r_len -= head_needed;
						if (_RecvHeadSize == HEAD_BUFF_SIZE) {
							int size = _RecvHeadBuff [0] | (_RecvHeadBuff [1] << 8);
							_PacketBuff = new byte[size];
							_PacketSize = 0;
						} else {
							break;
						}
					}

					int len = _PacketBuff.Length;
					int packet_needed = len - _PacketSize;
					if (r_len < packet_needed)
						packet_needed = r_len;
					Buffer.BlockCopy (_RecvBuff, r_off, _PacketBuff, _PacketSize, packet_needed);
					_PacketSize += packet_needed;
					r_off += packet_needed;
					r_len -= packet_needed;

					if (_PacketSize == len) {
						int packet_id = _RecvHeadBuff [2] | (_RecvHeadBuff [3] << 8);
						SaveData (EventPacketReceived, packet_id, _PacketBuff);
						_RecvHeadSize = 0;
						_PacketBuff = null;
						_PacketSize = 0;
					}
					if (r_len == 0) 
						break;
					Thread.Sleep (100);
				}

				PostReceive ();
			} else {
				UnityEngine.Debug.Log ("ReceiveCallback Else");
				SaveEvent (EventConnectionLost);
				_State = State.DISCONNECTED;
				_Socket.Shutdown (SocketShutdown.Send);
				PostDisconnect ();
			}
		} catch (Exception) {
			UnityEngine.Debug.Log ("ReceiveCallback catch");
			SaveEvent (EventConnectionLost);
			_State = State.DISCONNECTED;
			_Socket.Shutdown (SocketShutdown.Send);
			PostDisconnect ();
		}
	}

	private void DisconnectCallback (IAsyncResult ar)
	{
		try {
			_State = State.DISCONNECTED;
			_Socket.EndDisconnect (ar);
			_Socket.Close ();
		} catch (Exception) {
			_State = State.DISCONNECTED;
		}
	}

	private void PostReceive ()
	{
		_Socket.BeginReceive (_RecvBuff, 0, _RecvBuff.Length, SocketFlags.None, new AsyncCallback (ReceiveCallback), this);
	}

	private void PostDisconnect ()
	{
		if (_State == State.CONNECTED) {
			_State = State.DISCONNECTING;
			_Socket.BeginDisconnect (false, new AsyncCallback (DisconnectCallback), this);
		}
//	else
//	{
//		EventConnectionLost();
//	}
	}

	private void SaveEvent (EventCallback cb)
	{
		lock (_EvenQueue)
			_EvenQueue.Enqueue (new EventTask (cb));
	}

	private void SaveData (DataCallback cb, int id, byte[] data)
	{
		lock (_EvenQueue)
			_EvenQueue.Enqueue (new DataTask (cb, id, data));
	}

	private void EventConnectionMade ()
	{
		if (_PacketListener != null)
			_PacketListener.ConnectionMade (this);
	}

	private void EventConnectionFailed ()
	{
		if (_PacketListener != null)
			_PacketListener.ConnectionFailed (this);
	}

	private void EventConnectionLost ()
	{

		UnityEngine.Debug.Log ("--------------------------------- EventConnectionLost ");
		if (_PacketListener != null)
			_PacketListener.ConnectionLost (this);
	}

	private void EventPacketReceived (int id, byte[] data)
	{
		if (_PacketListener != null)
			_PacketListener.PacketReceived (this, id, data);
	}

	public PacketListener listener {
		get { return _PacketListener; }
		set { _PacketListener = value; }
	}

	public bool Connected {
		get { return _State == State.CONNECTED && (_Socket != null && _Socket.Connected); }
	}

	public bool Failed {
		get { return _Failed; }
	}

	/// <summary>
	/// check connect try to connect to server if not success disconnected from server
	/// </summary>
	/// <param name="host">Host.</param>
	/// <param name="port">Port.</param>
	public void checkConnected (string host, int port)
	{
		try {
			Socket nSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			nSocket.Connect (host, port);
			if (nSocket.Connected) {
				nSocket.Shutdown (SocketShutdown.Send);
				nSocket.Close ();
				return;
			} else {
				SaveEvent (EventConnectionLost);
				_State = State.DISCONNECTED;
				PostDisconnect ();
				EventConnectionLost ();
			}
		} catch (SocketException) {
			SaveEvent (EventConnectionLost);
			_State = State.DISCONNECTED;
			PostDisconnect ();
			EventConnectionLost ();
		}
	}
}

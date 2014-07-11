using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
namespace Server
{
    public class Server_User
    {
        public Socket mSocket;
        List<string> mSendMsgList = new List<string>();
        List<JsonData> mReceiveMsgList = new List<JsonData>();
        private byte[] byteMessage;
        Thread mReceive = null;
        protected List<byte> Msg = new List<byte>();
        public string ID;

        bool mIsStop = true;
        public Server_User()
        {

        }

        public void Init(Socket Target, int BufferLength)
        {
            mSocket = Target;
            byteMessage = new byte[BufferLength];
        }

        public void Close()
        {
            mIsStop = true;
            mSocket.Close();
            mReceive.Abort();
        }

        public void ClearMessage()
        {
            mSendMsgList.Clear();
            mReceiveMsgList.Clear();
        }

        protected void PullSendMessage(string Msg)
        {
            mSendMsgList.Add(Msg);
        }

        public string GetSendMessage()
        {
            if (!mIsStop&&mSendMsgList.Count > 0)
                return mSendMsgList[0];

            return "";
        }

        public void PushSendMessage()
        {
            if (mSendMsgList.Count > 0)
                mSendMsgList.RemoveAt(0);
        }

        public void StartReceive()
        {
            mReceive = new Thread(OnReceive);
            mReceive.Start();

            mIsStop = false;
        }

        void OnReceive()
        {
            while (true)
            {
                if (!mSocket.Connected)
                {
                    Facade.Singleton().SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE, this);
                    Thread.Sleep(1);
                    continue;
                }

                try
                {
                    int byteMessageLength = mSocket.Receive(byteMessage);
                    byte[] RealMessage = new byte[byteMessageLength];
                    Array.Copy(byteMessage, 0, RealMessage, 0, byteMessageLength);
                    Msg.AddRange(RealMessage);

                    //Debug.Log (Msg.Count+" "+byteMessageLength+" "+System.BitConverter.ToInt32(Msg.ToArray(),0));
                    while (Msg.Count > 0 && System.BitConverter.ToInt32(Msg.ToArray(), 0) <= Msg.Count)
                    {
                        int ReceiveCount = System.BitConverter.ToInt32(Msg.ToArray(), 0);
                        string tmpReceive = Encoding.Default.GetString(Msg.ToArray(), 4, ReceiveCount - 4);
                        PullReceiveMessage(tmpReceive);
                        Debug.Log("User:" + mSocket.RemoteEndPoint.AddressFamily.ToString() + " RECEIVE: " + tmpReceive);
                        Msg.RemoveRange(0, ReceiveCount);
                    }
                }
                catch (SocketException er)
                {
                    Debug.Log("User:" + mSocket.RemoteEndPoint.AddressFamily.ToString() + " Error: " + er.ToString());
                    Facade.Singleton().SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE, this);
                    continue;
                }
            }
        }

        public void PullReceiveMessage(string Msg)
        {
            JsonData tmpJson = (JsonData)MiniJSON.Json.Deserialize(Msg);
            if ((uint)tmpJson["event"] == GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN)
            {
                ID = (string)tmpJson["result"]["udid"];
                Facade.Singleton().SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, this);
                return;
            }

            mReceiveMsgList.Add(tmpJson);
        }

        public JsonData GetReceiveMessage()
        {
            if (mReceiveMsgList.Count > 0)
            {
                JsonData tmp = mReceiveMsgList[0];
                mReceiveMsgList.RemoveAt(0);
                return tmp;
            }

            return null;
        }
    }
}

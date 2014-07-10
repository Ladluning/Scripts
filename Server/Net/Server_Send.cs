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
    public class Server_Send : Controller
    {

        Server_Manager mServer;
        Thread mSend;
        // Use this for initialization
        void Awake()
        {
            mServer = Server_Manager.Singleton();
        }

        public void StartSend()
        {
            mSend = new Thread(OnSend);
            mSend.Start();
        }

        public void Close()
        {
            mSend.Abort();
        }

        // Update is called once per frame
        void OnSend()
        {
            while (true)
            {
                for (int i = 0; i < mServer.mUserList.Count; i++)
                {
                    if (!mServer.mUserList[i].mSocket.Connected)
                    {
                        Facade.Singleton().SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE, mServer.mUserList[i]);
                        continue;
                    }

                    string tmpSend;
                    if ((tmpSend = mServer.mUserList[i].GetSendMessage()) != "")
                    {
                        try
                        {
                            byte[] Bytes2 = Encoding.ASCII.GetBytes(tmpSend);
                            List<byte> tmp = new List<byte>(4 + Bytes2.Length);
                            tmp.AddRange(System.BitConverter.GetBytes(4 + Bytes2.Length));
                            tmp.AddRange(Bytes2);
                            mServer.mUserList[i].mSocket.Send(tmp.ToArray());
                            Debug.Log("SEND：" + mServer.mUserList[i].mSocket.AddressFamily.ToString() + " " + tmpSend);
                        }
                        catch (SocketException er)
                        {
                            Debug.Log("Send Error:" + mServer.mUserList[i].mSocket.AddressFamily.ToString() + " " + er.ToString() + " :::: \n" + tmpSend);
                            continue;
                        }
                    }
                }
                Thread.Sleep(2);

            }
        }
    }
}

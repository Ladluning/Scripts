using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server_Listen : Controller
    {

        protected TcpListener mListener;
        protected Thread mListenThread;
        protected Server_Manager mFather;

        void Awake()
        {
            mFather = Server_Manager.Singleton();
        }

        public void Init(int port, int space)
        {
            mListener = new TcpListener(IPAddress.Any, port);
            mListener.Start(space);

            mListenThread = new Thread(OnListen);
            mListenThread.Start();
        }

        public void Close()
        {
            mListener.Stop();
            mListenThread.Abort();
        }

        void OnListen()
        {
            while (true)
            {
                if (mListener.Pending())
                {
                    this.SendEvent(GameEvent.WebEvent.EVENT_WEB_NEW_REQUST, mListener.AcceptSocket());
                }

                Thread.Sleep(1);
            }
        }
    }
}

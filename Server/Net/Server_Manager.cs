using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public class Server_Manager : Controller
    {
        private static Server_Manager m_pInterface;
        public static Server_Manager Singleton()
        {
            return m_pInterface;
        }
        Server_Manager()
        {
            m_pInterface = this;
        }

        Server_Listen mListen;
        Server_Send mSend;
        void StartServer()
        {
            mListen = gameObject.AddComponent<Server_Listen>();
            mListen.Init(6000, 2);

            mSend = gameObject.AddComponent<Server_Send>();
            mSend.StartSend();
        }

        void OnEnable()
        {
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_NEW_REQUST, OnHandleNewRequest);
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, OnHandleLogin);
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE, OnHandleDisconnect);
        }

        void OnDisable()
        {
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_NEW_REQUST, OnHandleNewRequest);
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, OnHandleLogin);
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE, OnHandleDisconnect);
        }

        void OnDestroy()
        {
            if(mListen!=null)
            mListen.Close();

            if (mSend != null)
            mSend.Close();

            for (int i = 0; i < mUserList.Count; i++)
            {
                mUserList[i].Close();
            }
            mUserList.Clear();
            mPlayerList.Clear();
        }


        public List<Server_User> mUserList = new List<Server_User>();
        object OnHandleNewRequest(object pSender)
        {
            Server_User tmpUser = new Server_User();
            tmpUser.Init((Socket)pSender, 512);
            tmpUser.StartReceive();
            mUserList.Add(tmpUser);
            return null;
        }

        public List<Server_Game_User> mPlayerList = new List<Server_Game_User>();
        object OnHandleLogin(object pSender)
        {
            Server_User tmpUser = (Server_User)pSender;
            for (int i = 0; i < mPlayerList.Count; i++)
            {
                if (mPlayerList[i].ID == tmpUser.ID)
                {
                    mPlayerList[i].SetServer(tmpUser);
                    return null;
                }
            }

            Server_Game_User tmpPlayer = (Instantiate(Resources.Load("Server/Player/Point")) as GameObject).GetComponent<Server_Game_User>();
            tmpPlayer.SetServer(tmpUser);
            tmpPlayer.InitWithID(tmpUser.ID);
            tmpPlayer.gameObject.name = "Point_" + tmpUser.ID.ToString();
            //tmpPlayer.transform.parent = transform;
            mPlayerList.Add(tmpPlayer);
            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER, tmpPlayer);

            return null;
        }

        object OnHandleDisconnect(object pSender)
        {
            Server_User tmpUser = (Server_User)pSender;
            tmpUser.Close();
            mUserList.Remove(tmpUser);

            for (int i = 0; i < mPlayerList.Count; i++)
            {
                if (tmpUser == mPlayerList[i].mServer)
                {
                    mPlayerList[i].StartDestroy();
                    mPlayerList[i].mServer = null;
                    return null;
                }
            }

            return null;
        }

        public void DestroyPlayer(Server_Game_User Target)
        {
            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, Target);
            mPlayerList.Remove(Target);
            Destroy(Target);
        }
    }
}

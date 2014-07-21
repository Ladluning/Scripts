using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_User : Server_Game_User_Package
    {
        
        protected override void Awake()
        {
            base.Awake();

            SetSelf(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS,OnHandleUpdatePos);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS, OnHandleUpdatePos);
        }

        public void AddVisiblePlayer(Server_Game_User Target)
        {
            mVisiblePlayerList.Add(Target);
        }

        public void AddVisibleEnemy(Server_Game_Enemy Target)
        {
            mVisibleEnemyList.Add(Target);
        }

        public void InitWithID(string Target)
        {
            if (ID == null || ID != Target)
            {
                LoadData(Target);
            }
            ID = Target;
            mCurrentTransform.position = mDataInfo.WorldPos;
            RequireLoginData();
        }

        void OnDestroy()
        {
            SaveData();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            RequireVisibleData();

            mVisibleEnemyList.Clear();
            mVisiblePlayerList.Clear();
        }


        object OnHandleUpdatePos(object pSender)
        {
            Debug.Log(MiniJSON.Json.Serialize(pSender));
            JsonData tmpJson = new JsonData(pSender);
            mCurrentTransform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));

            SetChanged();
            RequirePosData();

            return null;
        }

        protected override void UpdateBoard()
        {
            
        }
    }
}

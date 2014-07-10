using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Serialize : Server_Game_User_Package
    {
        public string RequireLoginData()
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN);

            ((Dictionary<string, object>)tmpSend["results"]).Add("hp", mDataInfo.HP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mp", mDataInfo.MP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("exp", mDataInfo.EXP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mDataInfo.WorldPos.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mDataInfo.WorldPos.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mDataInfo.WorldPos.z);
            ((Dictionary<string, object>)tmpSend["results"]).Add("scene", mDataInfo.SceneID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mesh", mDataInfo.MeshID);

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, tmpSend);

            return MiniJSON.Json.Serialize(tmpSend);
        }

        public string RequireUserData()
        {
            Dictionary<string,object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA);

            ((Dictionary<string, object>)tmpSend["results"]).Add("hp",mDataInfo.HP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mp", mDataInfo.MP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("exp", mDataInfo.EXP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mDataInfo.WorldPos.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mDataInfo.WorldPos.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mDataInfo.WorldPos.z);
            ((Dictionary<string, object>)tmpSend["results"]).Add("scene", mDataInfo.SceneID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mesh", mDataInfo.MeshID);

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA, tmpSend);

            return MiniJSON.Json.Serialize(tmpSend);
        }

        public string RequireStorageData()
        { 
            //mDataInfo.mItemList
            return "";
        }

        public string RequireTaskData()
        {
            return "";
        }

        public string RequireChatData()
        {
            return "";
        }

        public string RequireTalentData()
        {
            return "";
        }

        protected override void UpdateMessage()
        {
            JsonData tmp = mServer.GetReceiveMessage();
            if (tmp != null)
            {
                //this.SendEvent(uint.Parse((string)tmp["event"]), tmp);
            }
        }
    }
}

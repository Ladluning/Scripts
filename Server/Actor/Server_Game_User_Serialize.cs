using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Serialize : Server_Game_User_Data
    {
        public Dictionary<string, object> RequireLoginData()
        {
            Dictionary<string, object> tmpSend = SerializeLoginData();

            ((Dictionary<string, object>)tmpSend["results"]).Add("id", mID);

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, tmpSend);
            return tmpSend;
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

        Dictionary<string, object> SerializeLoginData()
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mDataInfo.WorldPos.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mDataInfo.WorldPos.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mDataInfo.WorldPos.z);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_x", mDataInfo.WorldRotate.x);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_y", mDataInfo.WorldRotate.y);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_z", mDataInfo.WorldRotate.z);
            ((Dictionary<string, object>)tmpSend["results"]).Add("scene", mDataInfo.SceneID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mesh", mDataInfo.MeshID);
            return tmpSend;
        }

    }
}

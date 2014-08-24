using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Scene_NPC_Manager : Controller
    {
        Dictionary<string, Server_Game_NPC_Base> mNPCList = new Dictionary<string, Server_Game_NPC_Base>();
        void OnEnable()
        {
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_SCENE_DATA, OnHandleInitSceneData);
			//this.RegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC,OnHandleNewNPC);
		}

        void OnDisable()
        {
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_SCENE_DATA, OnHandleInitSceneData);
			//this.UnRegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC,OnHandleNewNPC);
		}

        void Awake()
        { 
            
        }

        public void RegistNPC(Server_Game_NPC_Base Target)
        {
            if (!mNPCList.ContainsKey(Target.name))
                mNPCList.Add(Target.name, Target);
            else
                Debug.LogError("Already Add NPC:"+Target.name);
        }

		object OnHandleNewNPC(object pSender)
		{

			return null;		
		}

        object OnHandleInitSceneData(object pSender)
        {
            JsonData tmpJson = new JsonData(pSender);
            if((string)tmpJson["results"]["target"]!=gameObject.name)
                return null;

            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_NPC);
            List<object> tmpNPCList = new List<object>();
            foreach (string key in mNPCList.Keys)
            {
                Dictionary<string, object> tmpNode = new Dictionary<string, object>();

                tmpNode.Add("id", mNPCList[key].name);
                tmpNode.Add("active",mNPCList[key].mIsActive);
				tmpNode.Add("pos_x", mNPCList[key].transform.localPosition.x);
                tmpNode.Add("pos_y", mNPCList[key].transform.localPosition.y);
                tmpNode.Add("pos_z", mNPCList[key].transform.localPosition.z);
                tmpNode.Add("rotate_x", mNPCList[key].transform.localEulerAngles.x);
                tmpNode.Add("rotate_y", mNPCList[key].transform.localEulerAngles.y);
                tmpNode.Add("rotate_z", mNPCList[key].transform.localEulerAngles.z);
				tmpNPCList.Add(tmpNode);
            }
            ((Dictionary<string, object>)tmpSend["results"]).Add("id", (string)tmpJson["results"]["id"]);
            ((Dictionary<string, object>)tmpSend["results"]).Add("npc", tmpNPCList);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_NPC,tmpSend);
			return null;
        }
    }
}

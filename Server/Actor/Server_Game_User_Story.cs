using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_User_Story : Server_Game_User_Component
    {
        [HideInInspector]
        public List<string> mActiveStoryList = new List<string>();
        void OnEnable()
        {
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_ACTIVE_STORY, OnHandleActiveStory);
        }

        void OnDisable()
        {
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_ACTIVE_STORY, OnHandleActiveStory);
        }

		public override void Init (Server_Game_User Father)
		{
			base.Init (Father);
			
			mActiveStoryList = Father.mDataInfo.mStoryList;
		}

        object OnHandleActiveStory(object pSender)
        {
            JsonData tmpJson = new JsonData(pSender);

            if ((string)tmpJson["results"]["id"] != mUser.mID)
                return null;

            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_ACTIVE_STORY);
            ((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("target", (string)tmpJson["results"]["target"]);

            if (!mActiveStoryList.Contains((string)tmpJson["results"]["target"]))
            {
                mActiveStoryList.Add((string)tmpJson["results"]["target"]);

                ((Dictionary<string, object>)tmpSend["results"]).Add("success", true);
                this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_ACTIVE_STORY, tmpSend);
            }
            else
            {
                ((Dictionary<string, object>)tmpSend["results"]).Add("success", false);
                this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_ACTIVE_STORY, tmpSend);
            }

            return null;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_Task_Main_0001 : Server_Game_Task_Base
    {
        public override void OnEnable()
        {
            //this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_CONNECT_NPC,OnHandleConnectNPC);
        }

        public override void OnDisable()
        {
            //this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_CONNECT_NPC, OnHandleConnectNPC);
        }

        public override bool GetIsTaskActive(Server_Game_User Target)
        {
            return true;
        }

        public override void TaskActive(Server_Game_User Target)
        {
            Target.GetTask().SetTaskActive(mTaskID);
        }

        public override void TaskFinish(Server_Game_User Target)
        {
            Target.GetTask().SetTaskFinish(mTaskID);
        }

        public override void TaskOver(Server_Game_User Target)
        {
            Target.GetTask().SetTaskOver(mTaskID);
        }

        public override void TaskFailed(Server_Game_User Target)
        {
            Target.GetTask().SetTaskFailed(mTaskID);
        }

        public override void UpdateTask(Server_Game_User Target)
        {

        }

        object OnHandleConnectNPC(object pSender)
        {
            JsonData tmpJson = new JsonData(pSender);
            if ((string)tmpJson["results"]["target"] == "NPC_0001_0003")
            {
                OnHandleConnectNPC_0001_0003((string)tmpJson["results"]["id"]);
            }
            else if ((string)tmpJson["results"]["target"] == "NPC_0001_0002")
            {
                OnHandleConnectNPC_0001_0002((string)tmpJson["results"]["id"]);
            }
            return null;
        }

        void OnHandleConnectNPC_0001_0003(string TargetID)
        {
            Server_Game_User tmpUser = Server_Game_Manager.Singleton().GetServerUserWithID(TargetID);
            if (tmpUser == null)
                return;

            if (tmpUser.GetTask().GetTaskStateWithID(mTaskID) == 2)//--Finish
            {
                //this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_FINISH_TASK,"");
                TaskOver(tmpUser);
            }
        }

        void OnHandleConnectNPC_0001_0002(string TargetID)
        {
            Server_Game_User tmpUser = Server_Game_Manager.Singleton().GetServerUserWithID(TargetID);
            if (tmpUser == null)
                return;

            if (tmpUser.GetTask().GetTaskStateWithID(mTaskID) == 0)//--No Task
            {
                TaskActive(tmpUser);
                TaskFinish(tmpUser);
            }
        }
    }
}

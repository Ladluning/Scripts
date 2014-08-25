using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_Task_Base : Controller
    {
        public string mTaskID;

        public virtual bool GetIsTaskActive(Server_Game_User Target)
        {
            return false;
        }

        public virtual void TaskActive(Server_Game_User Target)
        {

        }

        public virtual void TaskFinish(Server_Game_User Target)
        {

        }

        public virtual void TaskOver(Server_Game_User Target)
        { 
        
        }

        public virtual void TaskFailed(Server_Game_User Target)
        {

        }

        public virtual void UpdateTask(Server_Game_User Target)
        {

        }

        public virtual void OnEnable()
        {
        }
        public virtual void OnDisable()
        {
        }
    }
}

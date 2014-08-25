using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_Task_Manager : MonoBehaviour
    {
        public List<Server_Game_Task_Base> mTaskList = new List<Server_Game_Task_Base>();

        private static Server_Game_Task_Manager m_Interface;
        public static Server_Game_Task_Manager Singleton()
        {
            return m_Interface;
        }
        void Awake()
        {
            m_Interface = this;
        }

        public void RegistTask(Server_Game_Task_Base Target)
        {
            mTaskList.Add(Target);
        }

    }
}

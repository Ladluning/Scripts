using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Struct_Task_Info
    {
        public string mTaskID;
        public int mTaskState;//1--Active 2--Finish 3--Failed 4--Over
    }

    public class Server_Game_User_Task : MonoBehaviour
    {
        public List<Server_Struct_Task_Info> mTaskList;


        public void SetTaskActive(string TaskID)
        {
            Server_Struct_Task_Info tmpInfo = new Server_Struct_Task_Info();
            tmpInfo.mTaskID = TaskID;
            tmpInfo.mTaskState = 1;
        }

        public int GetTaskStateWithID(string TaskID)
        {
            for (int i = 0; i < mTaskList.Count; ++i)
            {
                if (mTaskList[i].mTaskID == TaskID)
                {
                    return mTaskList[i].mTaskState;
                }
            }

            return 0;//No Target Task
        }

        public void SetTaskFinish(string TaskID)
        {
            for (int i = 0; i < mTaskList.Count; ++i)
            {
                if (mTaskList[i].mTaskID == TaskID)
                {
                    mTaskList[i].mTaskState = 2;
                    return;
                }
            }
        }

        public void SetTaskFailed(string TaskID)
        {
            for (int i = 0; i < mTaskList.Count; ++i)
            {
                if (mTaskList[i].mTaskID == TaskID)
                {
                    mTaskList[i].mTaskState = 3;
                    return;
                }
            }
        }

        public void SetTaskOver(string TaskID)
        {
            for (int i = 0; i < mTaskList.Count; ++i)
            {
                if (mTaskList[i].mTaskID == TaskID)
                {
                    mTaskList[i].mTaskState = 4;
                    return;
                }
            }
        }
    }
}

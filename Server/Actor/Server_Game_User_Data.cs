using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_User_Data : Server_Game_User_Base
    {

        Server_Data_Manager mDataManager;
        protected Server_Game_User_Data()
        {
            mDataManager = Server_Data_Manager.Singleton();
        }

        protected virtual void LoadData(string TargetID)
        {
            mDataInfo = mDataManager.GetUserDataWithID(TargetID);
        }

        protected virtual void SaveData()
        {

        }
    }
}

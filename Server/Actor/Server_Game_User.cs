using UnityEngine;
using System.Collections;
namespace Server
{
    public class Server_Game_User : Server_Game_User_Serialize
    {
        protected override void Awake()
        {
            base.Awake();

            SetSelf(this);
        }

        public void InitWithID(string Target)
        {
            if (ID == null || ID != Target)
            {
                LoadData(Target);
            }
            ID = Target;

            RequireLoginData();
        }

        void OnDestroy()
        {
            SaveData();
        }
    }
}

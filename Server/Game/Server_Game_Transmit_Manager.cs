using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_Transmit_Manager : MonoBehaviour
    {
        List<Server_Game_Transmit_Point> mTransmitList = new List<Server_Game_Transmit_Point>();

        public void InitTransmitManager()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                mTransmitList.Add(transform.GetChild(i).GetComponent<Server_Game_Transmit_Point>());
            }
        }

        public Server_Game_Transmit_Point GetTransmitWithID(string ID)
        {
            for (int i = 0; i < mTransmitList.Count; i++)
            {
                if (ID == mTransmitList[i].name)
                {
                    return mTransmitList[i];
                }
            }
            return null;
        }

    }
}

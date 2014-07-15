using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Transmit_Point : MonoBehaviour
    {

        public string mTransmitTarget;
        public bool IsAvailable;
        public bool IsShow;

        public string GetTargetTransmit()
        {
            return mTransmitTarget;
        }
    }
}

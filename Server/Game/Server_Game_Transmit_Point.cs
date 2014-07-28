using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Transmit_Point : MonoBehaviour
    {

        public string mTransmitTarget;
        public bool IsAvailable;
        public bool IsShow;
        [HideInInspector]
        public Server_Game_Scene_Manager mFather;

        public void Init()
        {
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
        }

        public string GetTargetTransmit()
        {
            return mTransmitTarget;
        }

        public void OnDrawGizmos()
        {
            if (IsAvailable) Gizmos.color = Color.green; else Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position,0.5f);
            Gizmos.color = Color.white;
        }
    }
}

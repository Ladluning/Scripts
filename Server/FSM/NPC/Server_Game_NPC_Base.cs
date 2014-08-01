using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_NPC_Base : Controller
    {
        [HideInInspector]
        public string mNPCID;
        [HideInInspector]
        public string mSceneID;

        protected Server_Game_FSM_NPC_Base_Controller mController;
        protected Server_Game_Spawn_Point_NPC mFather;
        protected Server_Game_Scene_Manager mManager;
        protected Transform mCurrentTransform;
        protected bool mMarkAsChanged = true;
		protected Server_Game_Manager mGameManager;

		public virtual void Init()
		{

		}

        protected void InitNPC()
        {
            mCurrentTransform = transform;
			mGameManager = Server_Game_Manager.Singleton ();
            mManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Spawn_Point_NPC>(transform);
        }

        public void SetChanged()
        {
            mMarkAsChanged = true;
        }

        public bool GetIsChanged()
        {
            return mMarkAsChanged;
        }

        public Int2 GetMapPos()
        {
            return mManager.ConvertWorldPosToMapPos(transform.position);
        }

        public Server_Game_Scene_Manager GetManager()
        {
            return mManager;
        }

        public Server_Game_Spawn_Point_NPC GetSpawnPoint()
        {
            return mFather;
        }

        protected virtual void FixedUpdate()
        {
            UpdateChanged();
        }

        protected virtual void UpdateChanged()
        {
            if (!mMarkAsChanged)
            {
                return;
            }

            mMarkAsChanged = false;
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	[System.Serializable]
	public enum E_Actor_Type
	{
		None = 0,
		Player = 1 << 1,
		Enemy = 1 << 2,
		NPC = 1 << 3,
	}

    public class Server_Game_NPC_Base : Controller
    {
        [HideInInspector]
        public string mID;
        [HideInInspector]
        public string mSceneID;
      
        public bool mIsActive = true;
		[HideInInspector]
		public E_Actor_Type mActorType = E_Actor_Type.None;
		[HideInInspector]
		public Server_Game_FSM_NPC_Base_Controller mController;
		[HideInInspector]
		public Server_Game_Spawn_Point_Base mFather;
		[HideInInspector]
		public Server_Game_Scene_Manager mManager;
		[HideInInspector]
		public Server_Game_Manager mGameManager;
        protected Transform mCurrentTransform;
        protected bool mMarkAsChanged = true;

		protected Dictionary<Type, object> mDataComponentList = new Dictionary<Type, object>();
		public virtual void Init()
		{
			InitNPC();

			foreach(Type tmp in mDataComponentList.Keys)
			{
				((Server_Game_NPC_Data_Base)mDataComponentList[tmp]).Init(this);
			}
		}

		public T GetController<T>()
		{
			return (T)(object)mController;
		}

		public Server_Game_FSM_NPC_Base_Controller GetController()
		{
			return mController;
		}

		public T GetDataComponent<T>()
		{
			if (mDataComponentList.ContainsKey(typeof(T)))
				return (T)mDataComponentList[typeof(T)];
			return default(T);
		}

        protected void InitNPC()
        {
            mCurrentTransform = transform;
			mGameManager = Server_Game_Manager.Singleton ();
            mManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Spawn_Point_Base>(transform);
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

        public Server_Game_Spawn_Point_Base GetSpawnPoint()
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

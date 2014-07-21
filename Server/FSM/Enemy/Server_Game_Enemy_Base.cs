using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public enum E_Actor_Type : uint
    {
        None = 0,
        Player = 1 << 1,
        Enemy = 1 << 2,
        NPC = 1 << 3,
    }
    [System.Serializable]
    public class EnemyConfigInfo
    {
        public string MeshID;
        public int HP;
        public int MaxHP;
        public int MP;
        public int MaxMP;
        public float IdleStandTime;
        public float IdleMoveSpeed;
        public float MoveSpeed;
        public int Attack;
        public int Defend;
        public float AttackRange;
        public int EscapeHP;
        public int EXP;
    }
    public class Server_Game_Enemy_Base : Controller
    {
        public string mEnemyID;
        [HideInInspector]
        public string mSceneID;

        public EnemyConfigInfo mConfigInfo;
        protected Server_Game_FSM_Enemy_General_Controller mController;
        protected Server_Game_Spawn_Point_Enemy mFather;
        protected Server_Game_Scene_Manager mManager;
        protected Transform mCurrentTransform;
        protected bool mMarkAsChanged = true;

        public virtual void Init()
        {
            mCurrentTransform = transform;
            mManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Spawn_Point_Enemy>(transform);
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

        public Server_Game_Spawn_Point_Enemy GetSpawnPoint()
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

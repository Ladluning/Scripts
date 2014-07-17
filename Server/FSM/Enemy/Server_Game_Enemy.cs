using UnityEngine;
using System.Collections;

namespace Server
{
    public enum E_Actor_Type:uint
    { 
        None = 0,
        Player = 1<<1,
        Enemy = 1<<2,
        NPC = 1<<3,
    }
    [System.Serializable]
    public class EnemyConfigInfo
    {
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
    public class Server_Game_Enemy : Controller
    {
        public string EnemyID;
        [HideInInspector]
        public string SceneID;

        public EnemyConfigInfo mConfigInfo;
        private Server_Game_FSM_Enemy_General_Controller mController;
        private Server_Game_Spawn_Point_Enemy mFather;
        private Server_Game_Scene_Manager mManager;
        void Awake()
        {

        }

        public void Init()
        {
            mManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Spawn_Point_Enemy>(transform);
            mController = gameObject.AddComponent<Server_Game_FSM_Enemy_General_Controller>();
            mController.mEnemyInfo = mConfigInfo;
            mController.Init(this);

            //if (mManager.GetPointIsInMap(transform.position))
            //    transform.position = mManager.ConvertMapPosToWorldPos(GetMapPos());
            //else
            //    Debug.LogError("Error Init Pos");
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
    }
}

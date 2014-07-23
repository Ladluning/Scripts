using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Spawn_Point_NPC : MonoBehaviour
    {

        public GameObject mRefreshObject;
        public Vector3 SpawnPos;
        public bool IsShow = true;
        public bool IsAvailable = true;
        private Transform mCurrentTransform;
        private Server_Game_Scene_Manager mFather;
        void Awake()
        {
            mCurrentTransform = transform;
        }

        public void Init()
        {
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);

            //Server_Game_Enemy tmpEnemy = (Instantiate(TargetObject) as GameObject).GetComponent<Server_Game_Enemy>();
            //tmpEnemy.transform.parent = transform;
            //tmpEnemy.mSceneID = mFather.mSceneID;
            //tmpEnemy.name = name + "_Enemy_" + (mRefreshID++);
            //tmpEnemy.mEnemyID = tmpEnemy.name;
            //tmpEnemy.transform.localPosition = RefreshPoint;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.TransformPoint(SpawnPos), 0.5f);
            Gizmos.color = Color.white;
        }
    }
}

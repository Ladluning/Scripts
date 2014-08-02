using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Spawn_Point_NPC : Server_Game_Spawn_Point_Base
    {
        public GameObject mRefreshObject;

        void Awake()
        {
            mCurrentTransform = transform;
        }

        public override void Init()
        {
			base.Init ();
            

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

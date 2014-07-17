using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Spawn_Point_Enemy : MonoBehaviour 
	{
		public Server_Struct_Spawn_Info[] mRefreshInfo;
		public float mPointRange;
		public float mRefreshSpaceTime;
		public int mRefreshCount;

		private Transform mCurrentTransform;
		private float mRefreshTimer;
		private float mNextRefreshTime;
        private Server_Game_Scene_Manager mFather;
        private uint mRefreshID = 0;
        void Awake()
        {
            mCurrentTransform = transform;
        }

        public void Init()
        {
            mFather = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);

            Debug.Log(mFather);
            if (mFather == null)
                return;

            for (int i = 0; i < mRefreshCount; ++i)
            {
                Vector3 RefreshPoint = mFather.ConvertMapPosToWorldPos(GetEmptyRandomPos());
                GameObject TargetObject = GetCreateTarget();
                if (TargetObject == null)
                    return;

                Server_Game_Enemy tmpEnemy = (Instantiate(TargetObject) as GameObject).GetComponent<Server_Game_Enemy>();
                tmpEnemy.transform.parent = transform;
                tmpEnemy.SceneID = mFather.mSceneID;
                tmpEnemy.name = name + "_Enemy_" + (++mRefreshID);
                tmpEnemy.transform.position = RefreshPoint;
                tmpEnemy.Init();
            }
        }

		void Update()
		{
			if (mCurrentTransform.childCount >= mRefreshCount)
				return;

			mRefreshTimer += Time.deltaTime;
			if (mRefreshTimer > mNextRefreshTime) {

				mNextRefreshTime += mRefreshSpaceTime;
			
			}
		}

		void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere (transform.position,mPointRange);
		}

        GameObject GetCreateTarget()
        {
            int randomValue = Random.Range(0,100);
            for (int i = 0; i < mRefreshInfo.Length; ++i)
            {
                if (randomValue < mRefreshInfo[i].mRefreshProbability)
                    return mRefreshInfo[i].mRefreshObject;
            }
            return null;
        }

        public Int2 GetEmptyRandomPos()
        {
            while (true)
            {
                Vector3 RefreshPoint = new Vector3(Mathf.Sin(Random.Range(-3.15f, 3.15f)) * Random.Range(mPointRange / 2, mPointRange), 0, Mathf.Cos(Random.Range(-3.15f, 3.15f)) * Random.Range(mPointRange / 2, mPointRange));
                if (mFather.GetPointIsInMap(transform.TransformPoint(RefreshPoint)))
                {
                    return mFather.ConvertWorldPosToMapPos(transform.TransformPoint(RefreshPoint));
                }
            }
        }
	}
}

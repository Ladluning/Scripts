using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_Spawn_Point_Enemy : Server_Game_Spawn_Point_Base 
	{
		public Server_Struct_Spawn_Info[] mRefreshInfo;
		public float mPointRange;
		public float mRefreshSpaceTime;
		public int mRefreshCount;

		private float mRefreshTimer;
		private float mNextRefreshTime;
        private uint mRefreshID = 0;


        public override void Init()
        {
			base.Init();

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
                tmpEnemy.mSceneID = mFather.mSceneID;
                tmpEnemy.name = name + "_Enemy_" + (mRefreshID++);
				tmpEnemy.mID = tmpEnemy.name;
                tmpEnemy.transform.localPosition = RefreshPoint;
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
			Gizmos.DrawWireSphere (transform.TransformPoint(SpawnPos),mPointRange);
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

        public override Int2 GetEmptyRandomPos()
        {
			int i = 0;
            while (true)
            {
				i++;
                Vector3 RefreshPoint = new Vector3(Mathf.Sin(Random.Range(-3.15f, 3.15f)) * Random.Range(mPointRange / 2, mPointRange), 0, Mathf.Cos(Random.Range(-3.15f, 3.15f)) * Random.Range(mPointRange / 2, mPointRange));
				if (mFather.GetPointIsInMap(SpawnPos+RefreshPoint))
                {
					return mFather.ConvertWorldPosToMapPos(SpawnPos+RefreshPoint);
                }

				if(i>1000)
				{	
					Debug.Log("Cant Find Empty Pos");
					return new Int2(0,0);
				}
            }
        }
	}
}

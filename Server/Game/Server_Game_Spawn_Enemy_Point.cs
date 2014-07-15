using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Game_Spawn_Enemy_Point : MonoBehaviour 
	{
		public Server_Struct_Spawn_Info[] mRefreshInfo;
		public float mPointRange;
		public float mRefreshSpaceTime;
		public int mRefreshCount;

		private Transform mCurrentTransform;
		private float mRefreshTimer;
		private float mNextRefreshTime;

        public void Init()
        {
            for (int i = 0; i < mRefreshCount; i++)
            { 
                
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
	}
}

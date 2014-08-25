using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Object_SearchTarget : Server_Game_Object_Base
    {
        private uint mSearchType;
		private List<string> mSearchList = new List<string>();
		private Server_Game_Scene_Manager mManager;
		private float mSearchDistance;
        public void InitSearchType(uint SearchType,float SearchDistance)
        {
            mSearchType = SearchType;
			mSearchDistance = SearchDistance*SearchDistance;
			mManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
        }

		public void Update()
		{
	
		}

        public List<string> GetTargetList()
        {
			mSearchList.Clear();
			
			for(int i=0;i<mManager.mEnemyList.Count;++i)
			{
				if(GetIsSearch(mManager.mEnemyList[i].mActorType)&&GetIsInRange(mManager.mEnemyList[i].transform))
				{
					mSearchList.Add(mManager.mEnemyList[i].mID);
				}
			}
			for(int i=0;i<mManager.mPlayerList.Count;++i)
			{
				if(GetIsSearch(E_Actor_Type.Player)&&GetIsInRange(mManager.mPlayerList[i].transform))
				{
					mSearchList.Add(mManager.mPlayerList[i].mID);
				}
			}	

            return mSearchList;
        }

		public string GetClosestTarget()
		{
			string tmpCurrentID = "";
			float  tmpCurrentDistance = 99999f;

			for(int i=0;i<mManager.mEnemyList.Count;++i)
			{
				float tmpDistance = (mCurrentTransform.position-mManager.mEnemyList[i].transform.position).sqrMagnitude;
				if(GetIsSearch(mManager.mEnemyList[i].mActorType)&&tmpDistance<mSearchDistance&&tmpDistance<tmpCurrentDistance)
				{
					tmpCurrentID = mManager.mEnemyList[i].mID;
					tmpCurrentDistance = tmpDistance;
				}
			}
			for(int i=0;i<mManager.mPlayerList.Count;++i)
			{
				float tmpDistance = (mCurrentTransform.position-mManager.mPlayerList[i].transform.position).sqrMagnitude;
				if(GetIsSearch(E_Actor_Type.Player)&&tmpDistance<mSearchDistance&&tmpDistance<tmpCurrentDistance)
				{
					tmpCurrentID = mManager.mPlayerList[i].mID;
					tmpCurrentDistance = tmpDistance;
				}
			}

			return tmpCurrentID;
		}

        bool GetIsSearch(E_Actor_Type TargetType)
        {
            return (mSearchType & (uint)TargetType)!=0;
        }

		bool GetIsInRange(Transform Target)
		{
			return (mCurrentTransform.position-Target.position).sqrMagnitude<mSearchDistance;
		}
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Object_SearchTarget : Server_Game_Object_Base
    {
        private uint mSearchType;
        private List<GameObject> mSearchList = new List<GameObject>();
        public void InitSearchType(uint SearchType)
        {
            mSearchType = SearchType;
        }

        public List<GameObject> GetTargetList()
        {
            mSearchList.Clear();

            return mSearchList;
        }

        bool GetIsSearch(E_Actor_Type TargetType)
        {
            return (mSearchType & (uint)TargetType)!=0;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_User : Server_Game_User_Serialize
    {
		private Server_Game_User_Property mProperty;
		private Server_Game_User_Transform mTransform;
		private Server_Game_User_Package mPackage;
		private Server_Game_User_Visible mVisible;
		private Server_Game_User_Talent mTalent;
		public Server_Game_User_Property GetProperty()
		{
			return mProperty;
		}
		public Server_Game_User_Transform GetTransform()
		{
			return mTransform;
		}
		public Server_Game_User_Package GetPackage()
		{
			return mPackage;
		}
		public Server_Game_User_Visible GetVisible()
		{
			return mVisible;
		}

        protected override void Awake()
        {
            base.Awake();

            SetSelf(this);
        }

        public void InitWithID(string Target)
        {
            if (mID == null || mID != Target)
            {
                LoadData(Target);
            }
            mID = Target;
			mTransform = gameObject.AddComponent<Server_Game_User_Transform>();
			mComponentList.Add(mTransform);
			mPackage = gameObject.AddComponent<Server_Game_User_Package>();
			mComponentList.Add(mPackage);
			mVisible = gameObject.AddComponent<Server_Game_User_Visible>();
			mComponentList.Add(mVisible);
			mTalent = gameObject.AddComponent<Server_Game_User_Talent>();
			mComponentList.Add(mTalent);
			mProperty = gameObject.AddComponent<Server_Game_User_Property>();
			mComponentList.Add(mProperty);

			for(int i=0;i<mComponentList.Count;++i)
			{
				mComponentList[i].Init(this);
			}
            //
            RequireLoginData();
			mPackage.RequireStorageData();
			mProperty.RequireUserData ();
        }

        void OnDestroy()
        {
			for(int i=0;i<mComponentList.Count;++i)
			{
				mComponentList[i].UpdateData();
			}
            SaveData();
        }

		public bool GetIsChanged()
		{
			for(int i=0;i<mComponentList.Count;++i)
			{
				if(mComponentList[i].GetIsChanged())
					return true;
			}
			return false;
		}

		public void GetVisibleData(ref Dictionary<string,object> Target)
		{
			for(int i=0;i<mComponentList.Count;++i)
			{

			}
		}
    }
}

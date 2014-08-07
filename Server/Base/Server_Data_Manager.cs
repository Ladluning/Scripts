using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{

    public class Server_Data_Manager : Controller
    {
        private static Server_Data_Manager m_pInterface;
        public static Server_Data_Manager Singleton()
        {
            return m_pInterface;
        }
        Server_Data_Manager()
        {
            m_pInterface = this;
        }

        Struct_Game_User mData;
        //Server_Data_Map mMap;
        static string UserDataPath = "/User_Info.xml";
        void Awake()
        {
            object tmp = null;
			Server_Data_IO.Singleton().LoadData(Application.persistentDataPath + UserDataPath, ref tmp, typeof(Struct_Game_User));
			Debug.Log (Application.persistentDataPath + UserDataPath);
            if (tmp != null)
            {
                mData = (Struct_Game_User)tmp;
            }
            else
            {
                mData = new Struct_Game_User();
            }
        }

        void OnDestroy()
        {
			Server_Data_IO.Singleton().SaveData(Application.persistentDataPath + UserDataPath, mData);
        }

        public Struct_Game_User_Info GetUserDataWithID(string ID)
        {
            for (int i = 0; i < mData.mUserList.Count; i++)
            {
                if (mData.mUserList[i].ID == ID)
                    return mData.mUserList[i];
            }

            Struct_Game_User_Info tmpNewUser = new Struct_Game_User_Info();
            tmpNewUser.ID = ID;
			tmpNewUser.LosePropertyCount = 5;
			tmpNewUser.PropertyAdd_Strength = 5;
			tmpNewUser.PropertyAdd_Constitution = 5;
			tmpNewUser.PropertyAdd_Agility = 5;
			tmpNewUser.PropertyAdd_Intelligence = 5;
			tmpNewUser.MaxLevel = 60;
            tmpNewUser.EXP = 0;
			tmpNewUser.mStorageSlotCount = 60;
			tmpNewUser.mStorageSlotMaxCount = 60;
			tmpNewUser.MeshID = "Blade_Girl_M_Base_All";
            tmpNewUser.SceneID = "Scene_0001";
            tmpNewUser.WorldPos = new Vector3(185.5f,6.95f, 90);
			tmpNewUser.WorldRotate = new Vector3 (0,-105,0);
            tmpNewUser.mItemList.Add(Server_Item_Factory.RandomItem(100));
            tmpNewUser.mItemList.Add(Server_Item_Factory.RandomItem(101));
            tmpNewUser.mEquipList.Add(Server_Item_Factory.RandomEquip(102));
            tmpNewUser.mEquipList.Add(Server_Item_Factory.RandomEquip(1));
			tmpNewUser.mTalent = Server_Data_Talent.Singleton().GetOriginData();

            mData.mUserList.Add(tmpNewUser);
            return tmpNewUser;
        }


        public void LoadMapDataWithID(string ID)
        {
            Server_Data_Map.Load(Application.dataPath + "/Resources/MapData/" + ID + ".date", ID);
        }

    }
}

﻿using UnityEngine;
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
        Server_Data_IO mIO;

        static string Path = "/User_Info.xml";
        void Awake()
        {
            mIO = new Server_Data_IO();
            object tmp = null;
            mIO.LoadData(Application.persistentDataPath + Path, ref tmp, typeof(Struct_Game_User));

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
            mIO.SaveData(Application.persistentDataPath + Path, mData);
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
            tmpNewUser.HP = 100;
            tmpNewUser.MP = 100;
            tmpNewUser.EXP = 0;
            tmpNewUser.MeshID = "Mesh_Girl_0001";
            tmpNewUser.SceneID = "Scene_0001";
            tmpNewUser.WorldPos = new Vector3(160, 0, 80);
            tmpNewUser.mItemList.Add(Server_Item_Factory.RandomItem(100));
            tmpNewUser.mItemList.Add(Server_Item_Factory.RandomItem(101));
            tmpNewUser.mEquipList.Add(Server_Item_Factory.RandomEquip(102));
            tmpNewUser.mEquipList.Add(Server_Item_Factory.RandomEquip(103));
            mData.mUserList.Add(tmpNewUser);
            return tmpNewUser;
        }


    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    [System.Serializable]
    public class Struct_Game_User_Info
    {
        public string ID;
        public ulong EXP;
		public int MaxLevel;
		public int LosePropertyCount;
		public int PropertyAdd_Strength;
		public int PropertyAdd_Intelligence;
		public int PropertyAdd_Agility;
		public int PropertyAdd_Constitution;

        public int mStorageSlotMaxCount;
        public int mStorageSlotCount;

        public Vector3 WorldPos;
		public Vector3 WorldRotate;

        public string SceneID;
        public string MeshID;

		public List<string> mStoryList = new List<string> ();
        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
        public List<Struct_Item_Equip> mEquipList = new List<Struct_Item_Equip>();
		public Struct_Game_User_Talent mTalent;
    }
    [System.Serializable]
    public class Struct_Game_User
    {
        public List<Struct_Game_User_Info> mUserList = new List<Struct_Game_User_Info>();
    }
}
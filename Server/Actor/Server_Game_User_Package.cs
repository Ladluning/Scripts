using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Package : Server_Game_User_Data
    {
        List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
        public void InitPackage()
        {
            for (int i = 0; i < mDataInfo.mItemList.Count; i++)
            {
                mItemList.Add(mDataInfo.mItemList[i]);
            }
            for (int i = 0; i < mDataInfo.mEquipList.Count; i++)
            {
                mItemList.Add(mDataInfo.mEquipList[i]);
            }
        }


        public List<Struct_Item_Base> GetItemList()
        {
            return mItemList;
        }


        public void InsertItemWithInStorage(Struct_Item_Base Target)
        {
            int TmpPos = GetEmptySlotWithStorage();
            if (TmpPos == -1)
            {
                Debug.LogError("Server:Package Is Full");
                return;
            }

            Target.mItemPosID = TmpPos;
            mItemList.Add(Target);
            AddStorageData(Target);
        }

        public void InsertItemWithInStorage(Struct_Item_Base Target, int pos)
        {
            if (GetItemWithSlotPos(pos) != null)
            {
                Debug.LogError("Server:Cant Insert Item With Pos:" + pos);
                return;
            }

            Target.mItemPosID = pos;
            mItemList.Add(Target);
            AddStorageData(Target);
        }

        public virtual void RemoveItemWithStorage(Struct_Item_Base Target)
        {
            mItemList.Remove(Target);
            RemoveStorageData(Target);
        }

        public Struct_Item_Base GetItemWithSlotPos(int Pos)
        {
            for (int i = 0; i < mItemList.Count; ++i)
            {
                if (mItemList[i].mItemPosID == Pos)
                    return mItemList[i];
            }

            return null;
        }

        protected virtual int GetEmptySlotWithStorage()
        {
            for (int key = 100; key < mDataInfo.mStorageSlotMaxCount; key++)
            {
                if (GetItemWithSlotPos(key) == null)
                    return key;
            }
            return -1;
        }

        void AddStorageData(Struct_Item_Base Target)
        {
            if (Target.GetType() == typeof(Struct_Item_Base))
                mDataInfo.mItemList.Add(Target);
            else if (Target.GetType() == typeof(Struct_Item_Equip))
                mDataInfo.mEquipList.Add((Struct_Item_Equip)Target);
        }

        void RemoveStorageData(Struct_Item_Base Target)
        {
            if (Target.GetType() == typeof(Struct_Item_Base))
                mDataInfo.mItemList.Remove(Target);
            else if (Target.GetType() == typeof(Struct_Item_Equip))
                mDataInfo.mEquipList.Remove((Struct_Item_Equip)Target);
        }

        protected bool SwipStorageSlot(int PosA,int PosB)
        {
            Struct_Item_Base TmpA = GetItemWithSlotPos(PosA);
            Struct_Item_Base TmpB = GetItemWithSlotPos(PosB);

            if (TmpA != TmpB && TmpA != null && TmpB != null)
            {
                TmpA.mItemPosID = PosB;
                TmpB.mItemPosID = PosA;
                return true;
            }

            return true;
        }

        void ConvertItemToJson(Struct_Item_Base Target)
        { 
            
        }

        //void ConvertPotion
    }
}

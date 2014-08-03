﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Package : Server_Game_User_Serialize
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

            InsertItemWithInStorage(Target, TmpPos);
        }

        public void InsertItemWithInStorage(Struct_Item_Base Target, int pos)
        {
            if (GetItemWithSlotPos(pos) != null)
            {
                Debug.LogError("Server:Cant Insert Item With Pos:" + pos);
                return;
            }

            Struct_Item_Base tmpItem = GetSameItemWithType(Target.mItemMainType, Target.mItemSubType, Target.mCurrentCount);
            if (tmpItem == null)
            {
                Target.mItemPosID = pos;
                mItemList.Add(Target);
                AddStorageData(Target);
            }
            else
            {
                tmpItem.mCurrentCount += Target.mCurrentCount;
                AddStorageData(tmpItem);
            }
        }

        public virtual void RemoveItemWithStorage(Struct_Item_Base Target)
        {
            mItemList.Remove(Target);
            RemoveStorageData(Target);


        }

        public Struct_Item_Base GetSameItemWithType(E_Main_Item_Type MainType, int SubType, int Count)
        {
            for (int i = 0; i < mItemList.Count; ++i)
            {
                if (mItemList[i].mItemMainType == MainType && mItemList[i].mItemSubType == SubType && Count <= (mItemList[i].mMaxCount - mItemList[i].mCurrentCount))
                    return mItemList[i];
            }
            return null;
        }

        public Struct_Item_Base GetSameItemWithType(E_Main_Item_Type MainType, int SubType)
        {
            for (int i = 0; i < mItemList.Count; ++i)
            {
                if (mItemList[i].mItemMainType == MainType && mItemList[i].mItemSubType == SubType)
                    return mItemList[i];
            }
            return null;
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

        public virtual int GetEmptySlotWithStorage()
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

			Dictionary<string, object> tmpSend = SerializeAddItemData (Target);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_SEND_Add_USER_STORAGE,tmpSend);
        }

        void RemoveStorageData(Struct_Item_Base Target)
        {
            if (Target.GetType() == typeof(Struct_Item_Base))
                mDataInfo.mItemList.Remove(Target);
            else if (Target.GetType() == typeof(Struct_Item_Equip))
                mDataInfo.mEquipList.Remove((Struct_Item_Equip)Target);

			Dictionary<string, object> tmpSend = SerializeRemoveItemData (Target);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_SEND_REMOVE_USER_STORAGE,tmpSend);
        }

        protected bool SwipStorageSlot(int PosA,int PosB)
        {
            Struct_Item_Base TmpA = GetItemWithSlotPos(PosA);
            Struct_Item_Base TmpB = GetItemWithSlotPos(PosB);

            if(TmpA == TmpB)
                return false;

            if (TmpA != null)
            {
                TmpA.mItemPosID = PosB;
            }

            if (TmpB != null)
            {
                TmpB.mItemPosID = PosA;
            }

            return true;
        }
    }
}

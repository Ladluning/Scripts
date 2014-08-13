using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Package : Server_Game_User_Component
    {
        List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
		[HideInInspector] 
		public int mPackageSize;
		[HideInInspector] 
		public int mPackageMaxSize;

		void OnEnable()
		{
			this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_SEND_SWIP_PSTORAGE_ITEM,OnHandleSwipItem);
		}

		void OnDisable()
		{
			this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_SEND_SWIP_PSTORAGE_ITEM,OnHandleSwipItem);
		}

		public override void Init (Server_Game_User Father)
		{
			base.Init (Father);

			mPackageSize = Father.mDataInfo.mStorageSlotCount;
			mPackageMaxSize = Father.mDataInfo.mStorageSlotMaxCount;
			InitPackage();
		}

        public void InitPackage()
        {
            for (int i = 0; i < mUser.mDataInfo.mItemList.Count; i++)
            {
				mItemList.Add(mUser.mDataInfo.mItemList[i]);
            }
			for (int i = 0; i < mUser.mDataInfo.mEquipList.Count; i++)
            {
				mItemList.Add(mUser.mDataInfo.mEquipList[i]);
            }
        }

		public void ApplyEquipment()
		{
			for(int i=0;i<mItemList.Count;++i)
			{
				if (mItemList[i].GetType() == typeof(Struct_Item_Equip)&&mItemList[i].mItemPosID<100)
				{
					((Struct_Item_Equip)mItemList[i]).ApplyProperty(mUser.GetProperty().mOriginProperty,ref mUser.GetProperty().mCurrentProperty);
				}
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

		public virtual void RemoveItemWithStorage(Struct_Item_Base Target,int RemoveCount)
		{
			if(RemoveCount>=Target.mCurrentCount)
			{
				RemoveItemWithStorage(Target);
			}
			else
			{
				Target.mCurrentCount -= RemoveCount;

			}
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

		public Struct_Item_Base GetItemWithID(long ID)
		{
			for (int i = 0; i < mItemList.Count; ++i)
			{
				if (mItemList[i].mItemID == ID)
					return mItemList[i];
			}
			
			return null;
		}

        public virtual int GetEmptySlotWithStorage()
        {
			for (int key = 100; key < mUser.mDataInfo.mStorageSlotMaxCount; key++)
            {
                if (GetItemWithSlotPos(key) == null)
                    return key;
            }
            return -1;
        }

        void AddStorageData(Struct_Item_Base Target)
        {
			Dictionary<string, object> tmpSend = SerializeItemData (Target,GameEvent.WebEvent.EVENT_WEB_SEND_Add_USER_STORAGE);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_SEND_Add_USER_STORAGE,tmpSend);
        }

        void RemoveStorageData(Struct_Item_Base Target)
        {
			Dictionary<string, object> tmpSend = SerializeItemData (Target,GameEvent.WebEvent.EVENT_WEB_SEND_REMOVE_USER_STORAGE);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_SEND_REMOVE_USER_STORAGE,tmpSend);
        }

		void UpdateStorageData(Struct_Item_Base Target)
		{
			Dictionary<string, object> tmpSend = SerializeItemData (Target,GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_USER_STORAGE);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_USER_STORAGE,tmpSend);
		}

		object OnHandleSwipItem(object pSender)
		{
			Debug.Log (MiniJSON.Json.Serialize(pSender));
			JsonData tmpJson = new JsonData(pSender);
			if((string)tmpJson["results"]["id"]!=mUser.mID)
				return null;

			SwipStorageSlot((int)tmpJson["results"]["current"],(int)(int)tmpJson["results"]["target"]);

			return null;
		}

        protected bool SwipStorageSlot(int PosA,int PosB)
        {
            Struct_Item_Base TmpA = GetItemWithSlotPos(PosA);
            Struct_Item_Base TmpB = GetItemWithSlotPos(PosB);

            if(TmpA == TmpB)
                return false;
			List<Struct_Item_Base> tmpList = new List<Struct_Item_Base> ();
            if (TmpA != null)
            {
                TmpA.mItemPosID = PosB;
				tmpList.Add(TmpA);
            }

            if (TmpB != null)
            {
                TmpB.mItemPosID = PosA;
				tmpList.Add(TmpB);
            }
			Dictionary<string,object> tmpSend = SerializeItemData (tmpList.ToArray(),GameEvent.WebEvent.EVENT_WEB_RECEIVE_SWIP_PSTORAGE_ITEM);
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_USER_STORAGE,tmpSend);


            return true;
        }

		public override void UpdateData ()
		{
			base.UpdateData ();

			mUser.mDataInfo.mItemList.Clear();
			mUser.mDataInfo.mEquipList.Clear();

			for(int i=0;i<mItemList.Count;++i)
			{
				if (mItemList[i].GetType() == typeof(Struct_Item_Base))
					mUser.mDataInfo.mItemList.Add(mItemList[i]);
				else if (mItemList[i].GetType() == typeof(Struct_Item_Equip))
					mUser.mDataInfo.mEquipList.Add((Struct_Item_Equip)mItemList[i]);
			}

		}

		public Dictionary<string, object> RequireStorageData()
		{
			Dictionary<string, object> tmpSend = SerializeStorageData();
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("size",mPackageSize);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxSize",mPackageMaxSize);
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_STORAGE, tmpSend);
			return tmpSend;
		}

		Dictionary<string, object> SerializeStorageData()
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_STORAGE);
			List<object> tmpItemData = new List<object>();
			for (int i = 0; i < mItemList.Count; ++i)
			{
				tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(mItemList[i]));
			}
			((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);
			
			return tmpSend;
		}

		public Dictionary<string, object> SerializeItemData(Struct_Item_Base Target,uint EventID)
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(EventID);
			List<object> tmpItemData = new List<object>();
			for (int i = 0; i < 1; ++i)
			{
				tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(Target));
			}
			((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);
			
			return tmpSend;
		}

		public Dictionary<string, object> SerializeItemData(Struct_Item_Base[] Target,uint EventID)
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(EventID);
			List<object> tmpItemData = new List<object>();
			for (int i = 0; i < Target.Length; ++i)
			{
				tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(Target[i]));
			}
			((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);
			
			return tmpSend;
		}
    }
}

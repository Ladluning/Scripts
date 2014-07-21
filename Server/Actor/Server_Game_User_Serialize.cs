using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Serialize : Server_Game_User_Data
    {
        public Dictionary<string, object> RequireLoginData()
        {
            Dictionary<string, object> tmpSend = SerializeUserData();
            Dictionary<string, object> tmpStorage = SerializeStorageData();

            ((Dictionary<string, object>)tmpSend["results"]).Add("id", ID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", ((Dictionary<string, object>)(tmpStorage["results"]))["packages"]);

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, tmpSend);
            return tmpSend;
        }

        public Dictionary<string, object> RequireUserData()
        {
            Dictionary<string, object> tmpSend = SerializeUserData();

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA, tmpSend);
            return tmpSend;
        }

        public Dictionary<string, object> RequireStorageData()
        {
            Dictionary<string, object> tmpSend = SerializeStorageData();

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_STORAGE, tmpSend);
            return tmpSend;
        }

        public string RequireTaskData()
        {
            return "";
        }

        public string RequireChatData()
        {
            return "";
        }

        public string RequireTalentData()
        {
            return "";
        }

        public string RequirePosData()
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_POS);
            ((Dictionary<string, object>)tmpSend["results"]).Add("id",ID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mCurrentTransform.localPosition.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mCurrentTransform.localPosition.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mCurrentTransform.localPosition.z);
            ((Dictionary<string, object>)tmpSend["results"]).Add("rotate_x", mCurrentTransform.localEulerAngles.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("rotate_y", mCurrentTransform.localEulerAngles.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("rotate_z", mCurrentTransform.localEulerAngles.z);
            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_POS, tmpSend);

            return MiniJSON.Json.Serialize(tmpSend);
        }

        public string RequireVisibleData()
        {
            if (mVisibleEnemyList.Count <= 0&&mVisiblePlayerList.Count <= 0)
                return "";

            Dictionary<string, object> tmpSend = SerializeVisibleData();

            if (tmpSend == null)
                return "";

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA, tmpSend);
            return MiniJSON.Json.Serialize(tmpSend);
        }

        Dictionary<string, object> SerializeUserData()
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA);

            ((Dictionary<string, object>)tmpSend["results"]).Add("hp", mDataInfo.HP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mp", mDataInfo.MP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("exp", mDataInfo.EXP);
            ((Dictionary<string, object>)tmpSend["results"]).Add("package", mDataInfo.mStorageSlotCount);
            ((Dictionary<string, object>)tmpSend["results"]).Add("packagemax", mDataInfo.mStorageSlotMaxCount);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mDataInfo.WorldPos.x);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mDataInfo.WorldPos.y);
            ((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mDataInfo.WorldPos.z);
            ((Dictionary<string, object>)tmpSend["results"]).Add("scene", mDataInfo.SceneID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("mesh", mDataInfo.MeshID);

            return tmpSend;
        }

        Dictionary<string, object> SerializeStorageData()
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_STORAGE);
            List<object> tmpItemData = new List<object>();
            for (int i = 0; i < mDataInfo.mItemList.Count; i++)
            {
                tmpItemData.Add(ConvertItemToJson(mDataInfo.mItemList[i]));
            }
            for (int i = 0; i < mDataInfo.mEquipList.Count; i++)
            {
                tmpItemData.Add(ConvertItemToJson(mDataInfo.mEquipList[i]));
            }
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);

            return tmpSend;
        }

        Dictionary<string, object> SerializeAddItemData(Struct_Item_Base[] Target)
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE);
            List<object> tmpItemData = new List<object>();
            for (int i = 0; i < Target.Length; i++)
            {
                tmpItemData.Add(ConvertItemToJson(Target[i]));
            }
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);

            return tmpSend;
        }

        Dictionary<string, object> SerializeRemoveItemData(Struct_Item_Base[] Target)
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REMOVE_USER_STORAGE);
            List<object> tmpItemData = new List<object>();
            for (int i = 0; i < Target.Length; i++)
            {
                tmpItemData.Add(Target[i].mItemID);
            }
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);

            return tmpSend;
        }

        Dictionary<string, object> SerializeVisibleData()
        {
			if (mVisiblePlayerList.Count <= 0&&mVisibleEnemyList.Count<=0)
                return null;

            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA);
            List<object> tmpVisiblePlayerData = new List<object>();
            for (int i = 0; i < mVisiblePlayerList.Count; i++)
            {
                Dictionary<string, object> tmpUser = new Dictionary<string, object>();
                tmpUser.Add("id", mVisiblePlayerList[i].mDataInfo.ID);
                tmpUser.Add("hp", mVisiblePlayerList[i].mDataInfo.HP);
                tmpUser.Add("mp", mVisiblePlayerList[i].mDataInfo.MP);
                tmpUser.Add("exp", mVisiblePlayerList[i].mDataInfo.EXP);
                tmpUser.Add("pos_x", mVisiblePlayerList[i].mDataInfo.WorldPos.x);
                tmpUser.Add("pos_y", mVisiblePlayerList[i].mDataInfo.WorldPos.y);
                tmpUser.Add("pos_z", mVisiblePlayerList[i].mDataInfo.WorldPos.z);
                tmpUser.Add("mesh", mVisiblePlayerList[i].mDataInfo.MeshID);
                tmpVisiblePlayerData.Add(tmpUser);
            }
            List<object> tmpVisibleEnemyData = new List<object>();
            for (int i = 0; i < mVisibleEnemyList.Count; i++)
            {
                tmpVisibleEnemyData.Add(mVisibleEnemyList[i].GetSerializeData());
            }
            ((Dictionary<string, object>)tmpSend["results"]).Add("id", mDataInfo.ID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("visi_player", tmpVisiblePlayerData);
            ((Dictionary<string, object>)tmpSend["results"]).Add("visi_enemy", tmpVisibleEnemyData);
            return tmpSend;
        }

        protected override void UpdateMessage()
        {
            JsonData tmp = mServer.GetReceiveMessage();
            if (tmp != null)
            {
                //this.SendEvent(uint.Parse((string)tmp["event"]), tmp);
            }
        }

        Dictionary<string, object> ConvertItemToJson(Struct_Item_Base Target)
        {
            if (Target.GetType() == typeof(Struct_Item_Equip))
                return ConvertEquipToJson(Target as Struct_Item_Equip);
            else
                return ConvertBaseToJson(Target);
        }

        Dictionary<string, object> ConvertBaseToJson(Struct_Item_Base Target)
        {
            Dictionary<string, object> tmpItem = new Dictionary<string, object>();
            tmpItem.Add("type", (int)Target.mItemMainType);
            tmpItem.Add("subtype", Target.mItemSubType);
            tmpItem.Add("id", Target.mItemID);
            tmpItem.Add("pos", Target.mItemPosID);
            tmpItem.Add("num", Target.mCurrentCount);
            tmpItem.Add("max", Target.mMaxCount);
            tmpItem.Add("use", Target.mUse);
            return tmpItem;
        }

        //Dictionary<string,object> ConvertPotion()

        Dictionary<string, object> ConvertEquipToJson(Struct_Item_Equip Target)
        {
            Dictionary<string, object> tmpItem = ConvertBaseToJson(Target);
            tmpItem.Add("level", Target.mEquipLevel);
            tmpItem.Add("maxlevel", Target.mEquipMaxLevel);

            List<Dictionary<string, object>> tmpStats = new List<Dictionary<string, object>>();
            for (int i = 0; i < Target.mStats.Count; i++)
            {
                Dictionary<string, object> tmpStat = new Dictionary<string, object>();
                tmpStat.Add("id", Target.mStats[i].mIdentifier);
                tmpStat.Add("modifier", Target.mStats[i].mModifier);
                tmpStat.Add("amount", Target.mStats[i].mAmount);
                tmpStats.Add(tmpStat);
            }
            tmpItem.Add("stats", tmpStats.ToArray());
            return tmpItem;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Game_Storage_Manager_Base : Controller
{

    public int mStorageSlotMaxCount;
    public int mStorageSlotCount;

    public List<Game_Item_Base> mItemList = new List<Game_Item_Base>();

    protected virtual void Awake()
    {

    }

    public List<Game_Item_Base> GetItemList()
    {
        return mItemList;
    }


    void InitItemWithInStorage()
    {

    }

    public void InsertItemWithInStorage(Game_Item_Base Target)
    {
        Game_Item_Base TmpPos = GetItemWithSlotPos(Target.mItemPosID);
        if (TmpPos != null)
        {
            Debug.LogError("ID:" + Target .mItemID + "Insert Pos Not Empty:" + Target.mItemPosID);
            return;
        }

        mItemList.Add(Target);
    }

    public virtual void RemoveItemWithStorage(Game_Item_Base Target)
    {
        mItemList.Remove(Target);
    }

    public virtual void RemoveItemWithStorage(int pos)
    {
        Game_Item_Base TmpPos = GetItemWithSlotPos(pos);
        if (TmpPos != null)
        {
            return;
        }

        mItemList.Remove(TmpPos);
    }

    public virtual void RemoveItemWithStorage(long ID)
    {
        Game_Item_Base TmpPos = GetItemWithID(ID);
        if (TmpPos != null)
        {
            return;
        }

        mItemList.Remove(TmpPos);
    }

    public Game_Item_Base GetItemWithSlotPos(int Pos)
    {
        for (int i = 0; i < mItemList.Count; ++i)
        {
            if (mItemList[i].mItemPosID == Pos)
                return mItemList[i];
        }

        return null;
    }

    public Game_Item_Base GetItemWithID(long id)
    {
        for (int i = 0; i < mItemList.Count; ++i)
        {
            if (mItemList[i].mItemID == id)
                return mItemList[i];
        }

        return null;
    }

    protected virtual int GetEmptySlotWithStorage()
    {
        for (int key = 0; key < mStorageSlotMaxCount; key++)
        {
            if (GetItemWithSlotPos(key) == null)
                return key;
        }
        return -1;
    }
}

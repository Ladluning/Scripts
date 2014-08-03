using UnityEngine;
using System.Collections;
public enum E_Main_Item_Type
{
    None = 0,
    Potion,
    Equip,
}

public enum E_Use_Item_Type
{
    None = 0,
    Sell,
    Task,
    Equip,
    Unlock,
    Normal,
}

[System.Serializable]
public class Game_Item_Base 
{
	public int mUse;
	public string mItemOwner;
    public int mItemMainType;//User For Class of Object
    public int mItemSubType;//User For Icon // Use for Description
    public int mMaxCount = 5;
    public int mCurrentCount = 1;
    public long mItemID;//uniform ID
	public int mItemPosID;
	public string mItemDescription;

	public Color mItemColor = Color.white;

	protected Game_Actor_Property_Base mActorProperty;

    public bool GetIsCouldUseTo(E_Use_Item_Type TargetUse)
	{
		return (mUse & (1 << (int)TargetUse))>0;
	}

	public bool GetIsOwn(string Target)
	{
		return Target == mItemOwner;
	}

    public void SetOwner(string Target)
	{
		mItemOwner = Target;
	}

	public void SetUseTo(int UseTo)
	{
		mUse = UseTo;
	}

	public string GetName()
	{
        return mItemMainType.ToString();
	}

	public string GetTypeDescription()
	{
        return mItemSubType.ToString();
	}

	protected virtual void Awake()
	{

	}

	public virtual string GetItemDescription ()
	{
		return GetTypeDescription();
	}

	public virtual void SetUse ()
	{

	}

	public virtual void UnUse()
	{

	}

}

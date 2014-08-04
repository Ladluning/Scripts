using UnityEngine;
using System.Collections;

public class UI_Slot_Base : Controller 
{
    protected UISprite mSlotBackground;
	[HideInInspector]
	public UITexture mItemSprite;
	[HideInInspector]
	public object mOwner;

    public int  mSlotType = -1;
	public int  mSlotPosID;
    public bool mIsAvailable = false;
    
    protected virtual void Awake()
    {
        mSlotBackground = transform.FindChild("Background").GetComponent<UISprite>();
        mItemSprite = transform.FindChild("Item").GetComponent<UITexture>();
    }

	public virtual void Init(object Father,int SlotID)
	{
		mOwner = Father;
	}

    public virtual bool GetIsPlaceWithItemType(int TargetType)
    {
        if (!mIsAvailable)
            return false;
        else if (mSlotType == -1)
            return true;
        else if (mSlotType == TargetType)
            return true;

        return false;
    }

    public void SetIsAvailable(bool IsAvailable)
    {
        mIsAvailable = IsAvailable;
    }

    protected virtual void OnClick()
    {

    }

    protected virtual void OnDoubleClick()
    {

    }

    protected virtual void OnDrag()
    {

    }

    protected virtual void OnDrop(GameObject go)
    {

    }
}

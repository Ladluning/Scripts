﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Character_Package_Page : MonoBehaviour
{

	public int mStoragePageMaxCount = 36;
	public int mStartSlotID = -1;

	public Transform mInstanceNode;
	public GameObject mSlotPrefab;
    Game_Storage_Manager_Base mFather;
    UICenterOnChild mCurrentCenter;
    bool IsCloseChild = false;
	void Awake()
	{

	}

    void Start()
    {
        FindPageCenterInParent(transform);
    }

	public void InitWithStartSlotID(Game_Storage_Manager_Base Father, int StartSlotID,int PageSlotCount)
	{
		mStartSlotID = StartSlotID;
        mFather = Father;
		for(int i=StartSlotID;i<PageSlotCount+StartSlotID;i++)
		{
            UI_Package_Slot_Base tmpSolt = (Instantiate(mSlotPrefab) as GameObject).GetComponent<UI_Package_Slot_Base>();
			tmpSolt.transform.parent = mInstanceNode;
			tmpSolt.name = mSlotPrefab.name+i;
			tmpSolt.transform.localScale = Vector3.one;
			tmpSolt.transform.localEulerAngles = Vector3.zero;
            tmpSolt.InitWithID(mFather,i);
            tmpSolt.SetIsAvailable(true);
			mSlotList.Add(i,tmpSolt);
		}

		mInstanceNode.GetComponent<UITable> ().repositionNow = true;
	}

    private Dictionary<int, UI_Package_Slot_Base> mSlotList = new Dictionary<int, UI_Package_Slot_Base>();

    public UI_Package_Slot_Base GetSlotWithPos(int Pos)
	{
		if (mSlotList.ContainsKey (Pos))
			return mSlotList [Pos];
		
		return null;
	}

	public void InsertPageWithItem(int Pos,Game_Item_Base Target)
	{
		if (!mSlotList.ContainsKey (Pos))
			return;

		mSlotList [Pos].SetItem (Target);

	}

    void FindPageCenterInParent(Transform Target)
    {
        mCurrentCenter = Target.GetComponent<UICenterOnChild>();
        if (mCurrentCenter != null || Target.parent==null)
            return;

        FindPageCenterInParent(Target.parent);
    }

    void Update()
    {
        if (mCurrentCenter == null)
            return;
        Debug.Log(mCurrentCenter.centeredObject.name);
        if (mCurrentCenter.centeredObject == gameObject)
            SetChildrenIsCouldTouch(true);
        else
            SetChildrenIsCouldTouch(false);
    }

    void SetChildrenIsCouldTouch(bool IsTouch)
    {
        if (IsTouch == !IsCloseChild)
            return;

        IsCloseChild = !IsTouch;
		foreach (int i in mSlotList.Keys)
        {
            mSlotList[i].collider.enabled = IsTouch;
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Character_Info : Controller {

    private Dictionary<int, UI_Package_Slot_Base> mSlotList = new Dictionary<int, UI_Package_Slot_Base>();

	public Transform mInstanceNode;

	public UILabel mHP_Label;
	public UILabel mMP_Label;
	public UILabel mAttack_Label;
	public UILabel mDefend_Label;
	public UILabel mExp_Label;
	public UILabel mHit_Label;
	public UILabel mCrit_Label;
	public UILabel mLuck_Label;

	private Client_Property mProperty;
	void OnEnable()
	{
		this.RegistListen (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA,OnHandleListenUpdateData);
		this.RegistListen (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA,OnHandleListenUpdateData);
	}

	void OnDisable()
	{
		this.UnRegistListen (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA,OnHandleListenUpdateData);
		this.UnRegistListen (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA,OnHandleListenUpdateData);
	}


    void Start()
    {
		//Invoke ("InitPackage",5f);
		InitPackage ();
    }

    public UI_Package_Slot_Base GetSlotWithPos(int Pos)
    {
        if (mSlotList.ContainsKey(Pos))
            return mSlotList[Pos];

        return null;
    }

    public void InitPackage()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
			UI_Package_Slot_Base tmpSlot = mInstanceNode.GetChild(i).GetComponent<UI_Package_Slot_Base>();
            mSlotList.Add(tmpSlot.mSlotPosID, tmpSlot);

			tmpSlot.Init(Client_User.Singleton().GetPackage(),i);
			tmpSlot.name = "Slot_"+i;
        }
    }

	object OnHandleListenUpdateData(object pSender)
	{
		mProperty = Client_User.Singleton ().GetProperty ();
		mHP_Label.text = mProperty.mHP + "/" + mProperty.mMaxHP;
		mMP_Label.text = mProperty.mMP + "/" + mProperty.mMaxMP;
		mAttack_Label.text = mProperty.mMP.ToString();
		mDefend_Label.text = mProperty.mDefend.ToString();
		mExp_Label.text = mProperty.mExp.ToString();
		mHit_Label.text = mProperty.mHit.ToString();
		mCrit_Label.text = mProperty.mCrit.ToString();
		mLuck_Label.text = mProperty.mLuck.ToString();

		return null;
	}
}

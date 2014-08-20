using UnityEngine;
using System.Collections;

public class UI_Talent_Node : MonoBehaviour {

    public string mTalentID;
    [HideInInspector]
    public int mTalentCount;
    [HideInInspector]
    public bool mTalentActive;
    [HideInInspector]
    public UITexture mTalentIcon;
    public string mTalentName;
    public string mTalentIntroduce;

    Struct_Data_Talent_Node mCurrentData;
    void Start()
    {
        mCurrentData = Client_User.Singleton().GetTalent().GetTalentNodeWithID(mTalentID);

        mTalentIcon = gameObject.GetComponentInChildren<UITexture>();
    }

    public void Update()
    { 
        if(mCurrentData == null)
            return;

        mTalentActive = mCurrentData.Avaliable;
        mTalentCount = mCurrentData.CurrentCount;
    }
}

using UnityEngine;
using System.Collections;

public class UI_Talent_Introduce : MonoBehaviour {

    public UITexture mIntroduce_Icon;
    public UILabel mIntroduce_Name;
    public UILabel mIntroduce_Info;

    public void SetShowTalentInfo(UI_Talent_Node Target)
    {
        mIntroduce_Icon.material = Target.mTalentIcon.material;
        mIntroduce_Name.text = Localization.Get(Target.mTalentName);
        mIntroduce_Info.text = Localization.Get(Target.mTalentIntroduce);
    }

    public void ClearTalentInfo()
    {
        mIntroduce_Icon.material = null;
        mIntroduce_Name.text = "";
        mIntroduce_Info.text = "";
    }
}

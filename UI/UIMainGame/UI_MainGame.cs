using UnityEngine;
using System.Collections;

public class UI_MainGame : UIBase {

    public UISlider mHPSlider;
    public UISlider mMPSlider;
    public UISlider mEXPSlider;
    public UILabel mEXPLabel;

	public void OnHandleClickHead()
	{
		Debug.Log ("AAAAAAAAAAAAA");
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"UICharacterInfo");
	}

	public void OnHandleClickTalent()
	{

		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"UITalentInfo");
	}
}

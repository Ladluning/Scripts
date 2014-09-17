using UnityEngine;
using System.Collections;

public class UI_Character_Package_Cursor_Cancel : Controller {

	void OnClick()
	{
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_HIDE_UI,"UICharacterInfo");
	}
    void OnDrop()
    {
        UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
    }
}

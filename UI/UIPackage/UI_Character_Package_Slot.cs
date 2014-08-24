using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UI_Character_Package_Slot : UI_Package_Slot_Base 
{

    protected override void OnClick()
    {
        Debug.Log("Click");

        if (UI_Character_Package_Cursor.Singleton().GetCurrentCursor() != null)
        {
            UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
        }
        else
        {
            //Show Info
        }
    }

    protected override void OnDoubleClick()
    {

    }

    protected override void OnDrag()
    {
        if (mCurrentItem != null)
        {
			//Debug.Log(UICamera.currentTouch.clickNotification.GetHashCode()+" :"+UICamera.currentTouch.clickNotification);
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.Always;
            UI_Character_Package_Cursor.Singleton().SetCurrentCursor(this);
            SetItem(null);
        }
    }

    protected override void OnDrop(GameObject go)
    {
        if (UI_Character_Package_Cursor.Singleton().GetCurrentCursor() == null)
            return;

        if (UI_Character_Package_Cursor.Singleton().GetCurrentCursor().mOwner!=Client_User.Singleton().GetPackage())
        {
            UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
            return;
        }

        UI_Character_Package_Cursor.Singleton().ReplaceCurrentCursor(this);



        //UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
    }
}

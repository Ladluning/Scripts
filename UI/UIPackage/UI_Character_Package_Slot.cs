using UnityEngine;
using System.Collections;

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
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
            UI_Character_Package_Cursor.Singleton().SetCurrentCursor(this);
            SetItem(null);
        }
    }

    protected override void OnDrop(GameObject go)
    {
        if (UI_Character_Package_Cursor.Singleton().GetCurrentCursor() == null)
            return;

        //if (!UI_Character_Package_Cursor.Singleton().GetCurrentCursor().GetItem().GetIsOwn(Client_User.Singleton().GetID()))
        //{
        //    UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
        //    return;
        //}

        UI_Character_Package_Cursor.Singleton().ReplaceCurrentCursor(this);

        UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
    }
}

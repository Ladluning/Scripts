using UnityEngine;
using System.Collections;

public class UI_Character_Package_Cursor_Cancel : MonoBehaviour {


    void OnDrop()
    {
        UI_Character_Package_Cursor.Singleton().CancelCurrentCursor();
    }
}

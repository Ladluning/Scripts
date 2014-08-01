using UnityEngine;
using System.Collections;
public enum E_UI_Show_Package_State
{ 
    Both = 0,
    Info,
    Package,
}
public class UI_Package : UIBase
{
    public Transform mLeft_Anchor;
    public Transform mRight_Anchor;
    public UI_Character_Info mCharacterInfo;
    public UI_Character_Package mCharacterPackage;

    void Awake()
    {
        mCharacterPackage = gameObject.GetComponentInChildren<UI_Character_Package>();
        mCharacterInfo = gameObject.GetComponentInChildren<UI_Character_Info>();

        m_pInterface = this;
    }

    public override void ShowUI(object pSender)
    {
        base.ShowUI(pSender);


    }

    private static UI_Package m_pInterface;
    public static UI_Package Singleton()
    {
        return m_pInterface;
    }


    public UI_Package_Slot_Base GetSlotWithPos(int Pos)
    {
        if (Pos < 100)
            return mCharacterInfo.GetSlotWithPos(Pos);
        else
            return mCharacterPackage.GetSlotWithPos(Pos);
    }
}

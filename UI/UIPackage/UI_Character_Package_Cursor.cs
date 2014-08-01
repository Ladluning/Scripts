using UnityEngine;
using System.Collections;

public class UI_Character_Package_Cursor : MonoBehaviour {

    static private UI_Character_Package_Cursor m_pInterface;
    static public UI_Character_Package_Cursor Singleton()
	{
		return m_pInterface;
	}
	void Awake()
	{
		m_pInterface = this;

		if (uiCamera == null)
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
	}
	private Camera uiCamera;
    private UI_Package_Slot_Base mCurrentCursor;
    public void SetCurrentCursor(UI_Package_Slot_Base Target)
	{
		mCurrentCursor = Target;

	}

	public void CancelCurrentCursor()
	{
		mCurrentCursor = null;
	}

    public void ReplaceCurrentCursor(UI_Package_Slot_Base Target)
	{
        if (Target.GetIsPlaceWithItemType(mCurrentCursor.GetItem().mItemMainType)
                        && mCurrentCursor.GetIsPlaceWithItemType(Target.GetItem().mItemMainType))
        {
            //Debug.Log("Swip");
        }
	}

	public bool GetIsInCursor()
	{
		return mCurrentCursor != null;
	}

    public UI_Package_Slot_Base GetCurrentCursor()
	{
		return mCurrentCursor;
	}

	public UITexture mItemIconSprite;
	void Update()
	{
		if (mCurrentCursor == null) 
		{
			mItemIconSprite.material = null;
			return;
		}
        else if (mItemIconSprite.material == null || mItemIconSprite.material.name != mCurrentCursor.GetItem().GetName()) 
		{
            mItemIconSprite.material = Resources.Load("Item/" + mCurrentCursor.GetItem().GetName(), typeof(Material)) as Material;
		}

		UdpateIconSpritePos ();
	}

	void ClearIconSprite()
	{
		if (mItemIconSprite.material != null)
			mItemIconSprite.material = null;
	}

	void UpdateIconSprite()
	{

	}


	void UdpateIconSpritePos()
	{

		Vector3 pos;
		if (Input.touchCount > 0) 
		{
			pos = Input.GetTouch (0).position;
		}
		else 
		{
			pos = Input.mousePosition;
		}

		if (uiCamera != null) 
		{
			pos.x = Mathf.Clamp01(pos.x / Screen.width);
			pos.y = Mathf.Clamp01(pos.y / Screen.height);
			transform.position = uiCamera.ViewportToWorldPoint(pos);
						
			// For pixel-perfect results
			if (uiCamera.isOrthoGraphic)
			{
				Vector3 lp = transform.localPosition;
				lp.x = Mathf.Round(lp.x);
				lp.y = Mathf.Round(lp.y);
				transform.localPosition = lp;
			}
		}
	}
}

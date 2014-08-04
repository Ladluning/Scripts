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

		mItemSprite = gameObject.GetComponent<UITexture>();
	}
	private Camera uiCamera;
    private UI_Slot_Base mCurrentCursor;
	private UITexture mItemSprite;
    public void SetCurrentCursor(UI_Slot_Base Target)
	{
		mCurrentCursor = Target;
		mItemSprite.material = Target.mItemSprite.material;
	}

	public void CancelCurrentCursor()
	{
		mCurrentCursor = null;
	}

    public void ReplaceCurrentCursor(UI_Slot_Base Target)
	{
		CancelCurrentCursor();
	}

	public bool GetIsInCursor()
	{
		return mCurrentCursor != null;
	}

    public UI_Slot_Base GetCurrentCursor()
	{
		return mCurrentCursor;
	}


	void Update()
	{
		if (mCurrentCursor == null) 
		{
			mItemSprite.material = null;
			return;
		}
		UdpateIconSpritePos ();
	}

	void ClearIconSprite()
	{
		if (mItemSprite.material != null)
			mItemSprite.material = null;
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

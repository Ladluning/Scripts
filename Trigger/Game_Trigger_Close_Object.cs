using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SphereCollider))]
public class Game_Trigger_Close_Object : Controller {


	SphereCollider mCollider;
	bool IsEnable = false;

	public bool IsAutoTrigger = false;

	void Awake()
	{
		mCollider = gameObject.GetComponent<SphereCollider> ();
	}

	void OnEnable()
	{
		this.RegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_NPC,OnHandleClickNPC);
	}

	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_NPC,OnHandleClickNPC);
	}

	object OnHandleClickNPC(object pSender)
	{
		return null;

		if(IsEnable)
			GameObject.Find("CameraManager").SendMessage("DoFade");
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.tag == "Player") 
		{
			if(IsAutoTrigger)
				GameObject.Find("CameraManager").SendMessage("DoFade");
			IsEnable = true;
		}
	}

	void OnTriggerExit(Collider Col)
	{
		IsEnable = false;
	}

	void OnDrawGizmos()
	{
		if (mCollider == null)
			Awake ();
		Gizmos.color = new Color (Color.blue.r,Color.blue.g,Color.blue.b,0.3f);
		Gizmos.DrawSphere (transform.position,transform.lossyScale.x*mCollider.radius);
	}
}

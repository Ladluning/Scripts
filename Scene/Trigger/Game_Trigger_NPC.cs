using UnityEngine;
using System.Collections;

public class Game_Trigger_NPC : Controller {

	SphereCollider mCollider;
	bool IsEnable = false;

	public GameObject mFather;

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
		
		if (IsEnable) 
		{
			IsEnable = false;
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_TRIGGER_ENTER_NPC,mFather);	
		}
	}
	
	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.tag == "Player") 
		{
			IsEnable = true;
		}
	}
	
	void OnTriggerExit(Collider Col)
	{
		IsEnable = false;
		this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_TRIGGER_EXIT_NPC,mFather);	
	}
	
	void OnDrawGizmos()
	{
		if (mCollider == null)
			Awake ();
		Gizmos.color = new Color (Color.blue.r,Color.blue.g,Color.blue.b,0.3f);
		Gizmos.DrawSphere (transform.position,transform.lossyScale.x*mCollider.radius);
	}
}

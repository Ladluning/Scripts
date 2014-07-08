using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NetMobile
{
	
public class NetBase : Controller {
	
	public float m_nTimeoutDuration = 30f;
	public float m_nTimeoutDurationCarrier = 30f;
	public float m_nTimeoutDurationWifi = 15f;
	protected float m_nTimer;
	
	protected bool m_bTimeOut = false;
	protected bool m_bIsStop = false;
	protected bool m_bOnHandle = false;
	
	protected bool IsInConnect = false;
	protected List<byte> Msg = new List<byte>();

	//Handle Msg
	protected virtual void Start()
	{
		MakeTimeOutDuration();
	}
	
	public bool GetIsOnHandle()
	{
		return m_bOnHandle;
	}
	
	public virtual void SetPostIsStop(bool bStop)
	{
		m_bIsStop = bStop;
	}
	
	protected void MakeTimeOutDuration()
	{
		//Debug.Log(Application.internetReachability);
		m_bTimeOut = false;
		if ( Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork )
        {
            m_nTimeoutDuration = m_nTimeoutDurationCarrier;
        }
        else if ( Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork )
        {
            m_nTimeoutDuration = m_nTimeoutDurationWifi;
        }
		else
		{
			LogManager.LogError(" New Work Cant Reachable!");
			SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE,null);
		}
	}
	//Post Queue

	protected PostStruct GetTopMessage()
	{				
		if(NetPostQueue.sendList.Count>0)
		{
			return NetPostQueue.sendList[0];
		}
		return null;
	}
	
	protected bool PullMessage(PostStruct TmpStruct)
	{
		if(NetPostQueue.sendList.Remove(TmpStruct))
			return true;
		return false;
	}
	
	protected JsonData SerializeData(string ReceiveData)
    {
		return new JsonData(MiniJSON.Json.Deserialize(ReceiveData));
    }
}
	
}
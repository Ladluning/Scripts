using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventCapture {

	// Use this for initialization
	Dictionary<uint,RegistFunction> m_listenMap = new Dictionary<uint, RegistFunction>();
	public void RegistEvent(uint EventID,RegistFunction pFunction)
	{	
		if(!m_listenMap.ContainsKey(EventID))
		{	
			m_listenMap.Add(EventID,pFunction);
		}
		else
		{
			m_listenMap[EventID] += pFunction;
		}
	}
	public void UnRegistEvent(uint EventID,RegistFunction pFunction)
	{
		if(m_listenMap.ContainsKey(EventID))
		{	
			m_listenMap[EventID] -= pFunction;
		}
	}
	public void ListenEvent(uint EventID,object pSender)
	{
		if(m_listenMap.ContainsKey(EventID)&&m_listenMap[EventID]!=null)
			m_listenMap[EventID](pSender);
	}
}

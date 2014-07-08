using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventDispathcer {

	// Use this for initialization
	Dictionary<uint,RegistFunction> m_dispathcerMap = new Dictionary<uint, RegistFunction>();
	public void RegistEvent(uint EventID,RegistFunction pFunction)
	{	
		if(!m_dispathcerMap.ContainsKey(EventID))
		{	
			m_dispathcerMap.Add(EventID,pFunction);
		}
		else
		{
			m_dispathcerMap[EventID] += pFunction;
		}
	}
	public void UnRegistEvent(uint EventID,RegistFunction pFunction)
	{
		if(m_dispathcerMap.ContainsKey(EventID))
		{	
			m_dispathcerMap[EventID] -= pFunction;
		}
	}
	public object HandleEvent(uint EventID,object pSender)
	{
		if(m_dispathcerMap.ContainsKey(EventID)&&m_dispathcerMap[EventID]!=null)
		{	
			return m_dispathcerMap[EventID](pSender);
		}
		
		return null;
	}
}
//http://192.168.18.152:8080/index.php/user/login
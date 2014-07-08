using UnityEngine;
using System.Collections;
public delegate object RegistFunction(object pSender);
public class Facade : MonoBehaviour {

	// Use this for initialization
	EventCapture     m_pEventListener = new EventCapture();
	EventDispathcer  m_pEventDispathcer = new EventDispathcer();
	
	static Facade m_Interface;
	static public Facade Singleton()
	{
		return m_Interface;
	}
	Facade()
	{
		m_Interface = this;
	}

	public void RegistListen(uint EventID,RegistFunction pFunction)
	{
		m_pEventListener.RegistEvent(EventID,pFunction);
	}
	public void UnRegistListen(uint EventID,RegistFunction pFunction)
	{
		m_pEventListener.UnRegistEvent(EventID,pFunction);
	}
	public void RegistEvent(uint EventID,RegistFunction pFunction)
	{	
		m_pEventDispathcer.RegistEvent(EventID,pFunction);
	}
	public void UnRegistEvent(uint EventID,RegistFunction pFunction)
	{
		m_pEventDispathcer.UnRegistEvent(EventID,pFunction);
	}
	public object SendEvent(uint EventID,object pSender)
	{
		object Tmp = m_pEventDispathcer.HandleEvent(EventID,pSender);
		m_pEventListener.ListenEvent(EventID,pSender);
		return Tmp;
	}
}

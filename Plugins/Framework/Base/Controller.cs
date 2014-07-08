using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private Facade m_pFacade;
	protected Controller()
	{
		m_pFacade = Facade.Singleton ();
	}
	protected void RegistListen(uint EventID,RegistFunction pFunction)
	{
		m_pFacade.RegistListen(EventID,pFunction);
	}
	protected void UnRegistListen(uint EventID,RegistFunction pFunction)
	{
		m_pFacade.UnRegistListen(EventID,pFunction);
	}
	protected void RegistEvent(uint EventID,RegistFunction pFunction)
	{	
		m_pFacade.RegistEvent(EventID,pFunction);
	}
	protected void UnRegistEvent(uint EventID,RegistFunction pFunction)
	{
		m_pFacade.UnRegistEvent(EventID,pFunction);
	}
	protected object SendEvent(uint EventID,object pSender)
	{
		return m_pFacade.SendEvent(EventID,pSender);
	}
}

using UnityEngine;
using System.Collections;

public class Counter  {

	public int m_nCounter = 0;
	public void Add()
	{
		
		m_nCounter++;
		//Debug.Log ("Count"+m_nCounter);
	}
	public bool Remove()
	{
		m_nCounter--;
		//Debug.Log ("Count"+m_nCounter);
		if(m_nCounter<=0)
		{
			return true;
		}
		return false;
	}
	public bool IsRetain()
	{
		return m_nCounter>0;
	}
}

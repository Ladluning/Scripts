using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace NetMobile
{
	
public class CSharpHtmlInterface : NetBase {
	
	private Counter m_pHandleCount = new Counter();
	
	//Handle Msg
	protected override void Start()
	{
		base.Start();
		//MakeTimeOutDuration();
	}
	void FixedUpdate ()
	{
		//	Debug.Log ("DoPost"+m_bTimeOut+" "+m_bOnHandle+" "+GetTopMessage()+" "+m_bIsStop);
		CheckIfTimeOut();
		if(!m_bTimeOut&&!m_bOnHandle&&GetTopMessage()!=null&&GetTopMessage().m_eNetType == ENetType.Html&&!m_bIsStop)
		{
			StartCoroutine( "DoPost" );
		}
	}

	void CheckIfTimeOut()
	{
		if(m_bOnHandle)
		{
			m_nTimer+= Time.deltaTime/Time.timeScale;
			if(m_nTimer>m_nTimeoutDuration)
			{
				Debug.Log("TimeOut");
				m_bIsStop = true;
				SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE,null);
				
				if(m_pHandleCount.IsRetain())
					EndPost();
				
				StopDoPost();
			}
		}
	}
	public void StopDoPost()
	{
		m_bOnHandle = false;
		StopCoroutine("DoPost");
	}
	void BeginPost()
	{
		m_pHandleCount.Add();
		SendEvent(GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_TRANSPARENT,null);
		m_bOnHandle = true;
		m_nTimer = 0f;
	}
	void EndPost()
	{
		m_pHandleCount.Remove();
		SendEvent(GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_TRANSPARENT,null);
		m_bOnHandle = false;
		m_nTimer = 0f;
	}
	IEnumerator DoPost()
	{
		PostStruct tmpData = GetTopMessage();
		
		if ( string.IsNullOrEmpty( tmpData.m_url ) )
        {
            LogManager.LogError("Empty URL-- :"+tmpData.m_pSenderString);
			PullMessage(tmpData);
            yield break;
        }

		BeginPost();
		WWW tmpWWW;
		
		if(tmpData.m_pSender!=null)
		{
			Debug.Log ("SERVER_REQUEST: 	[ "+tmpData.m_url+" ] "+tmpData.m_pSenderString);
			tmpWWW = new WWW(tmpData.m_url,Encoding.UTF8.GetBytes(tmpData.m_pSenderString));
		}
		else
		{
			LogManager.Log("Send NULL");
			m_bOnHandle = false;
			yield break;
		}
		yield return tmpWWW;
		
		if(m_bIsStop)
			yield break;
		
		if(tmpWWW.error != null)
		{
			Debug.Log("Net Error:"+tmpWWW.error);
			EndPost();
			
			m_bIsStop = true;

			SendEvent(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE," "+tmpWWW.error);
			
			m_bOnHandle = false;
			yield break;
		}
		
		if(tmpWWW.text == null)
		{
			LogManager.Log("Receive NULL");
			EndPost();
			m_bOnHandle = false;
			yield break;
		}
		//Debug.Log(tmpWWW.text);
		JsonData ReceiveData;
		try
		{
			Debug.Log("SERVER_RESPONSE:	"+tmpWWW.text);
			ReceiveData = SerializeData(tmpWWW.text);
			
			//LogManager.Log(MiniJSON.Json.Serialize(ReceiveData.Dictionary));//["results"][0].Dictionary));
			if((Int32)ReceiveData["success"]==0)
			{
				//LogManager.LogError("Receive Failed ErrorID"+ReceiveData["code"]);
				m_bOnHandle = false;
				
				int ErrorResult = (int)this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR,ReceiveData);
				if(ErrorResult==0)//ignore
				{
					PullMessage(tmpData);
					EndPost();
				}
				else if(ErrorResult == 1)//ReSend
				{
					
				}
				else if(ErrorResult == 2)//Stop
				{
					m_bIsStop = true;
				}	
				EndPost();
				yield break;
			}
			
			if(PullMessage(tmpData))
				NetPostQueue.receiveList.Add(tmpWWW.text);
		}
		catch( UnityException ex)
		{
			LogManager.Log("Serialize ReceiveData Error"+ex);
			EndPost();
			yield break;
		}		
		catch(SystemException ex)
		{
			LogManager.Log("Serialize ReceiveData Error"+ex);
			EndPost();
			yield break;			
		}
		
		EndPost();
	}

}

}
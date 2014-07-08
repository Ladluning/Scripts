using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ENetType
{
	Socket = 0,
	Html,
}

public class PostStruct
{
	public object m_pSender;
	public string m_url;
	public uint   m_nLayer;
	public ENetType m_eNetType;
	public string m_pSenderString;
	public long   m_nSendID;
	public PostStruct(ENetType eNetType,string URL,object pSender,uint nLayer)
	{
		m_eNetType = eNetType;
		m_pSender = pSender;
		m_url = URL;
		m_nLayer = nLayer;
	}
	
	public void MakeMD5(long SendID)
	{
		m_nSendID = SendID;
		
		if(((Dictionary<string,object>)m_pSender).ContainsKey("sid"))
			((Dictionary<string,object>)m_pSender)["sid"] = GlobalValueManager.SID;
		if(((Dictionary<string,object>)((Dictionary<string,object>)m_pSender)["results"]).ContainsKey("uid"))
			((Dictionary<string,object>)((Dictionary<string,object>)m_pSender)["results"])["uid"] = GlobalValueManager.ID.ToString();
		string tmp = MiniJSON.Json.Serialize((m_pSender as Dictionary<string,object>)["results"]);
		string TmpMD5Send = NetUtilHelper.Md5Sum(GlobalValueManager.MD5_Key + tmp +((m_pSender as Dictionary<string,object>)["results"]as Dictionary<string,object>)["time"]);
		((Dictionary<string,object>)((Dictionary<string,object>)m_pSender)["results"])["keycode"] = TmpMD5Send.ToUpperInvariant();
		m_pSenderString = MiniJSON.Json.Serialize(m_pSender as Dictionary<string,object>);		
	}
};
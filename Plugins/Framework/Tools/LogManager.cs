using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Net;
public class LogManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	static StreamWriter writer;
    static FileStream fInfo;
	static string datapath ;
	static System.Text.StringBuilder LogText = new System.Text.StringBuilder();
	static public void InitLog()
	{
		/*
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			datapath = Application.persistentDataPath+"/"+GlobalValueManager.ID.ToString()+"_BraveCard_Log.txt";
		}
		else
		{
			datapath = Application.dataPath+"/"+GlobalValueManager.ID.ToString()+"_BraveCard_Log.txt";
		}
		
		try
		{
			
			//fInfo = new FileStream(datapath,FileMode.Append,FileAccess.ReadWrite);
			
            //writer = fInfo.();
		}
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
		*/
	}
	static bool m_bIsUpdate = false;
	static public void UpLoadLog()
	{
		m_bIsUpdate = true;
		fInfo = new FileStream(datapath,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
		writer = new StreamWriter(fInfo);
		writer.Write(LogText);
		writer.Flush();
		fInfo.Flush();
		writer.Close();
		fInfo.Close();
		WebClient Tmp = new WebClient();
		Tmp.UploadFile("http://bbs.enveesoft.com:84/brave/index.php/site/logwrite",datapath);
		Debug.Log ("UpLoad_Log");
		m_bIsUpdate = false;
	}
	
	static public void Log(string message)
	{
		Debug.Log(message);
//		Debug.LogWarning("AtSet Log");
//		Debug.LogWarning(string.Format("AtSeaTestReceiveTime [{0}]", NetUtilHelper.GetLongFromTime()));
		
		//WriterLog(message);
	}
	static public void LogError(string message)
	{
		Debug.LogError(Time.time+": "+message);
//		Debug.LogWarning(string.Format("AtSeaTestErrorTime [{0}]", NetUtilHelper.GetLongFromTime()));
	}
	
	public static void WriterLog(string sInfo)
	{
		try
		{
            String _data = DateTime.Now.ToString()+" "+sInfo+"\n";
			LogText.Append(_data);
			
		}catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
	}

}

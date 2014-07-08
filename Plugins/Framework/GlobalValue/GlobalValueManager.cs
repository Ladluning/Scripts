using UnityEngine;
using System.Collections;

public class GlobalValueManager : MonoBehaviour {
	
	public const string CONFIGURE_SID = "SID";
	
	public const string CONFIGURE_IP = "IP";
	public const string CONFIGURE_PORT = "Port";
	public const string CONFIGURE_VERSION = "VERSION";
	public const string CONFIGURE_SERVER = "SERVER";
	public const string CONFIGURE_STAGE = "BEGINNERSGUIDESTAGE";	
	public const string CONFIGURE_UpdateVersonUrl = "UpdateVersonUrl";
	public const string CONFIGURE_FIRSTINSTALL = "FirstInstall";
	public const string CONFIGURE_WORKINGITEM = "WorkingItem";
	public const string CONFIGURE_GIFTCOUNT = "GiftCount";
	public const string CONFIGURE_PHYSICAL = "Physical";
	public const string CONFIGURE_LAST_PHYSICAL = "PhysicalTime";
	public const string CONFIGURE_IS_FIGHT_AUTO = "IsFightAuto";
	public const string CONFIGURE_IS_FIGHT_CHANGE = "IsFightChange";
	public const string CONFIGURE_IS_FIGHT_TIME_SCALE = "IsFightTimeScale";
	public const string CONFIGURE_FIGHT_STORYDATA = "FightStoryData";
	public const string CONFIGURE_UI_STORYDATA = "UIStoryData";
	public const string CONFIGURE_GREENHAND_DATA = "GreenHandData";	
	public const string CONFIGURE_BACKVOLUME_DATA = "BackVolume";
	public const string CONFIGURE_EFFECTVOLUME_DATA = "EffectVolume";
//	public static string SID = "8e9d3ac5e674a70e3428844646001877";
//	public static int ID = 23;
	
	public static string SID = "asfasd";
	public static int ID = 213;
	public static string MD5_Key = "A55FC7E57F596C430009F898F9494767";
	public static string UDID = "134578913";
	
	public static string IP;
	public static int Port;
	public static string Version;
	public static string Server;	
	public static int BeginnersGuideStage;
    public static string UpdateVersonUrl;

	public static string Http_Address = "http://192.168.18.152:8100/api";
	public static string Socket_Address = "192.168.18.152";
	public static int Socket_Port = 8200;

    public static string VolumeValue;
	
	public static JsonData GoodsList;
	public static JsonData ProductList;
	void Start () {
		LoadDatasFromPlayerPrefs();
	}
	

	public static void SaveDatasToPlayerPrefs ()
	{
		SetIP(IP);
		SetPort(Port);
		SetVersion(Version);
		SetServer(Server);
		SetStage(BeginnersGuideStage);
		SetUrl(UpdateVersonUrl);
	}
	
	public static void LoadDatasFromPlayerPrefs ()
	{
		IP = GetIP();
		Port = GetPort();
		Version = GetVersion();
		Server = GetServer();
		BeginnersGuideStage = GetStage();
		UpdateVersonUrl = GetUrl();
	}
	
	public static void DeleteDatasFromPlayerPrefs ()
	{
		DeleteIP();
		DeletePort();
		DeleteVersion();
		DeleteServer();
		DeleteStage();
		DeleteUrl();
	}

	public static void SetGameSID (string vSID)
	{
		SID = vSID;
	}
	public static void SetSocketAddress (string vAddress)
	{
		Socket_Address = vAddress;
	}
	public static void SetSocketPort (int vPort)
	{
		Socket_Port = vPort;
	}
	
	
	public static void SetFirstInstall(int IsFirst)
	{
		PlayerPrefs.SetInt(CONFIGURE_FIRSTINSTALL,IsFirst);
	}
	public static void SetIP (string vIP)
	{
		PlayerPrefs.SetString(CONFIGURE_IP, vIP);
	}
	public static void SetPort (int vPort)
	{
		PlayerPrefs.SetInt(CONFIGURE_PORT, vPort);
	}
	public static void SetVersion (string vVersion)
	{
		PlayerPrefs.SetString(CONFIGURE_VERSION, vVersion);
	}
	public static void SetServer (string vServer)
	{
		PlayerPrefs.SetString(CONFIGURE_SERVER, vServer);
	}
	public static void SetStage (int vStage)
	{
		PlayerPrefs.SetInt(CONFIGURE_STAGE, vStage);
	}
    public static void SetUrl(string url)
    {
        PlayerPrefs.SetString(CONFIGURE_UpdateVersonUrl, url);
    }
	public static void SetWorkingItem(int nItem)
    {
        PlayerPrefs.SetInt(CONFIGURE_WORKINGITEM, nItem);
    }
	public static void SetGiftCount(int nCount)
    {
        PlayerPrefs.SetInt(CONFIGURE_GIFTCOUNT, nCount);
    }
	public static void SetPhysical(int nPhysical)
    {
		PlayerPrefs.SetInt(CONFIGURE_PHYSICAL,nPhysical);
    }	
	public static void SetLastPhysicalTime(long nTime)
	{
		PlayerPrefs.SetString(CONFIGURE_LAST_PHYSICAL,nTime.ToString());
	}
	public static void SetFightIsAuto(bool IsAuto)
	{
		PlayerPrefs.SetInt(CONFIGURE_IS_FIGHT_AUTO,IsAuto==true?1:0);
	}
	public static void SetFightCameraChange(bool IsChange)
	{
		PlayerPrefs.SetInt(CONFIGURE_IS_FIGHT_CHANGE,IsChange==true?1:0);
	}
	public static void SetFightTimeIsScale(bool IsTimeScale)
	{
		PlayerPrefs.SetInt(CONFIGURE_IS_FIGHT_TIME_SCALE,IsTimeScale==true?1:0);
	}
    public static void SetVolumeValue(float volumeValue)
    {
        PlayerPrefs.SetFloat(VolumeValue, volumeValue);
    }
	public static void SetFightStoryData(string pText)
	{
		 PlayerPrefs.SetString(CONFIGURE_FIGHT_STORYDATA, pText);
	}
	public static void SetUIStoryData(string pText)
	{
		 PlayerPrefs.SetString(CONFIGURE_UI_STORYDATA, pText);
	}
	public static void SetGreenHandData(bool bIsOver)
	{
		 PlayerPrefs.SetInt(CONFIGURE_GREENHAND_DATA, bIsOver?1:0);
	}
	public static void SetBackVolumeData(float nValue)
	{
		 PlayerPrefs.SetFloat(CONFIGURE_BACKVOLUME_DATA,nValue);
	}
	public static void SetEffectVolumeData(float nValue)
	{
		 PlayerPrefs.SetFloat(CONFIGURE_EFFECTVOLUME_DATA,nValue);
	}

    public static float GetVolumeValue()
    {
        return PlayerPrefs.GetFloat(VolumeValue);
    }
	public static int GetFirstInstall()
	{
		return PlayerPrefs.GetInt(CONFIGURE_FIRSTINSTALL);
	}
	public static string GetIP ()
	{
		return PlayerPrefs.GetString(CONFIGURE_IP);
	}
	public static int GetPort ()
	{
		return PlayerPrefs.GetInt(CONFIGURE_PORT);
	}
	public static string GetVersion ()
	{
		return PlayerPrefs.GetString(CONFIGURE_VERSION);
	}
	public static string GetServer ()
	{
		return PlayerPrefs.GetString(CONFIGURE_SERVER);
	}
	public static int GetStage ()
	{
		return PlayerPrefs.GetInt(CONFIGURE_STAGE);
	}
	public static string GetUrl()
    {
        return PlayerPrefs.GetString(CONFIGURE_UpdateVersonUrl);
    }
	public static int GetWorkingItem()
    {
        return PlayerPrefs.GetInt(CONFIGURE_WORKINGITEM);
    }
	public static int GetGiftCount()
    {
        return PlayerPrefs.GetInt(CONFIGURE_GIFTCOUNT);
    }
	public static int GetPhysical()
    {
		if(PlayerPrefs.HasKey(CONFIGURE_PHYSICAL))
			return PlayerPrefs.GetInt(CONFIGURE_PHYSICAL);
		return 0;
    }	
	public static long GetLastPhysicalTime()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_LAST_PHYSICAL))
			return long.Parse(PlayerPrefs.GetString(CONFIGURE_LAST_PHYSICAL));
		
		return 0;
	}
	public static bool GetFightIsAuto()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_IS_FIGHT_AUTO))
			return PlayerPrefs.GetInt(CONFIGURE_IS_FIGHT_AUTO)==1;
		return false;
	}
	public static bool GetFightCameraChange()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_IS_FIGHT_CHANGE))
			return PlayerPrefs.GetInt(CONFIGURE_IS_FIGHT_CHANGE)==1;
		return false;
	}
	public static bool GetFightTimeIsScale()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_IS_FIGHT_TIME_SCALE))
			return PlayerPrefs.GetInt(CONFIGURE_IS_FIGHT_TIME_SCALE)==1;
		return false;
	}
	public static string GetFightStoryData()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_FIGHT_STORYDATA))
			return PlayerPrefs.GetString(CONFIGURE_FIGHT_STORYDATA);
		return "";
	}
	public static string GetUIStoryData()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_UI_STORYDATA))
			return PlayerPrefs.GetString(CONFIGURE_UI_STORYDATA);
		return "";
	}
	public static bool GetGreenHandData()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_GREENHAND_DATA))
			return PlayerPrefs.GetInt(CONFIGURE_GREENHAND_DATA)==1;
		return false;
	}
	public static float GetBackVolumeData()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_BACKVOLUME_DATA))
			return PlayerPrefs.GetFloat(CONFIGURE_BACKVOLUME_DATA);
		return 0.35f;
	}
	public static float GetEffectVolumeData()
	{
		if(PlayerPrefs.HasKey(CONFIGURE_EFFECTVOLUME_DATA))
			return PlayerPrefs.GetFloat(CONFIGURE_EFFECTVOLUME_DATA);
		return 1f;
	}

	
	
	public static void DeleteFirstInstall()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_FIRSTINSTALL))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_FIRSTINSTALL);		
	}
	public static void DeleteIP ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_IP))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_IP);
	}
	public static void DeletePort ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_PORT))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_PORT);
	}
	public static void DeleteVersion ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_VERSION))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_VERSION);
	}
	public static void DeleteServer ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_SERVER))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_SERVER);
	}
	public static void DeleteStage ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_STAGE))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_STAGE);
	}
	public static void DeleteUrl ()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_UpdateVersonUrl))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_UpdateVersonUrl);
	}
	public static void DeleteWorkingItem()
    {
		if (!PlayerPrefs.HasKey(CONFIGURE_WORKINGITEM))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_WORKINGITEM);
    }
	public static void DeleteGiftCount()
    {
		if (!PlayerPrefs.HasKey(CONFIGURE_GIFTCOUNT))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_GIFTCOUNT);
    }	
	public static void DeletePhysical()
    {
		if (!PlayerPrefs.HasKey(CONFIGURE_PHYSICAL))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_PHYSICAL);
    }	
	public static void DeleteLastPhysicalTime()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_LAST_PHYSICAL))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_LAST_PHYSICAL);		
	}
	public static void DeleteFightStoryData()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_FIGHT_STORYDATA))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_FIGHT_STORYDATA);	
	}
	public static void DeleteUIStoryData()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_UI_STORYDATA))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_UI_STORYDATA);	
	}
	public static void DeleteGreenHandData()
	{
		if (!PlayerPrefs.HasKey(CONFIGURE_GREENHAND_DATA))
			return ;
		PlayerPrefs.DeleteKey(CONFIGURE_GREENHAND_DATA);
	}
}

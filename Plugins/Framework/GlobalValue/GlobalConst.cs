using UnityEngine;
using System.Collections;

public static class GlobalConst
{
	//----------------------------------------界面名字------------------------------------//
	public const string UINAME_UPDATEMANAGER = "UI_UpdateManager";
	public const string UINAME_UPDATEPROGRESS = "UI_UpdateProgress";
	public const string UINAME_WARNING = "UI_Warning";
	
	public const string UINAME_LOGINMANAGER = "UI_LoginManager";
	public const string UINAME_LOGINCHOOSE = "UI_LoginChoose";
	public const string UINAME_LOGINOPERATE = "UI_LoginOperate";
	
	public const string UINAME_SKILLTREE = "UI_SkillTree";
	
	public const string UINAME_MAINGAME = "UIMainGame";
	public const string UINAME_BUILDINGCOST = "UIBuildingCost";
	public const string UINAME_TASKUI = "UI_Task";
	
	public const string UINAME_BUILDSOLDIERS = "UI_BuildSoldiers";
	public const string UINAME_BUILDINGCASTLE = "UIBuilding_Castle";
	public const string UINAME_BUILDINGRESOURCES = "UIBuilding_Resources";
	//----------------------------------------界面名字------------------------------------//
	
	//----------------------------------------建筑----------------------------------------//
	public const string BUILDING_RESOURCES = "Building_Resources";
	public const string BUILDING_TECHNOLOGY = "Building_Technology";
	public const string BUILDING_SOLDIERS = "Building_Soldiers";
	public const string BUILDING_ATTACK = "Building_Attack";
	//----------------------------------------建筑----------------------------------------//
	
	//----------------------------------------Platform------------------------------------//
	public const string PLATFORM_ANDROID = "android";
	public const string PLATFORM_IOS = "ios";
	
	
	public const int RESOURCES_OPERATE_ADD = 0;//添加资源//
	public const int RESOURCES_OPERATE_DEC = 1;//减少资源//
	
	public const int MAX_POPULATION = 500;
	
	
	public const string ENEMY_LAYER = "Actor";
	
	
	//按钮事件//
	public const string UIBUTTONEVENT_FUNCTIONNAME = "ClickButton";
	
	
	public static string GetDataPath ()
	{
		return Application.dataPath;
	}
	public static string GetPersistentPath()
	{
		return Application.persistentDataPath;
	}
	public static string GetStreamAssetPath()
	{
		return Application.streamingAssetsPath;
	}
	
}

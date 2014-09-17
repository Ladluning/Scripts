using UnityEngine;
using System.Collections;

//0x0000020//
public class EventUI
{
	public uint EVENT_UI_SHOW_UI = 0x0000000;
	public uint EVENT_UI_HIDE_UI = 0x0000001;
	
	public uint EVENT_UI_COULD_TOUCH = 0x0000002;
	public uint EVENT_UI_COULD_NOT_TOUCH = 0x0000003;
	public uint EVENT_UI_ALL_COULD_TOUCH = 0x0000004;
	public uint EVENT_UI_ALL_COULD_NOT_TOUCH = 0x0000005;

	public uint EVENT_UI_SHOW_LOADING_BLOCK = 0x0000010;
	public uint EVENT_UI_HIDE_LOADING_BLOCK = 0x0000011;

	public uint EVENT_UI_SHOW_LOADING_TRANSPARENT = 0x0000012;
	public uint EVENT_UI_HIDE_LOADING_TRANSPARENT = 0x0000013;

	public uint EVENT_UI_SHOW_LOADING_ALPHA = 0x0000014;
	public uint EVENT_UI_HIDE_LOADING_ALPHA = 0x0000015;
	
	public uint EVENT_UI_CLICK_BUTTON = 0x0000016;
	
	
	public uint EVENT_UI_STARTTOCOPYFILE = 0x0000018;//开启复制文件//
	public uint EVENT_UI_SHOWCOPYSTATUS = 0x0000019;//复制状态//
	public uint EVENT_UI_STARTTOUPDATEFILE = 0x000001a;//开启下载文件//
	public uint EVENT_UI_SHOWUPDATESTATUS = 0x000001b;//下载状态//
	public uint EVENT_UI_SHOWPROGRESS = 0x000001c;//显示进度//
	public uint EVENT_UI_STARTTOLOGIN = 0x000001d;//开启登录//
	
	public uint EVENT_UI_SHOW_LOGINCHOOSE = 0x000001e;//开启选择注册(登陆)界面//
	public uint EVENT_UI_SHOW_LOGINOPERATE = 0x000001f;//开启注册(登陆)界面//
	
	public uint EVENT_UI_WARNINGMANAGER_SETSTATUS = 0x0000020;//警告信息//
	
	public uint EVENT_UI_REFRESH_SOLDIERS_LIST = 0x0000021;//刷新兵营界面显示//
}

//0x1000021//
public class EventWeb
{
	public uint EVENT_WEB_NOT_REACHABLE = 0xf000002;
	public uint EVENT_WEB_NEW_REQUST = 0xf000004;
	public uint EVENT_WEB_RECEIVE_VERSION = 0xf1000006;
	public uint EVENT_WEB_SEND_VERSION = 0xf1000008;//
	
	public uint EVENT_WEB_SEND_HEART = 0xf00000a;
	public uint EVENT_WEB_RECEIVE_HEART = 0xf00000c;
	
	public uint EVENT_WEB_SEND_REGIST = 0xf00000e;
	public uint EVENT_WEB_RECEIVE_REGIST = 0xf000010;
	
	public uint EVENT_WEB_SEND_LOGIN = 0xf000012;
	public uint EVENT_WEB_RECEIVE_LOGIN = 0xf000014;
	
	public uint EVENT_WEB_SEND_TALK = 0x1000020;
	public uint EVENT_WEB_RECEIVE_TALK = 0x1000022;
	
	public uint EVENT_WEB_SEND_UPDATE_POS = 0x1000024;
	public uint EVENT_WEB_RECEIVE_UPDATE_POS = 0x1000026;

	public uint EVENT_WEB_SEND_INIT_USER_DATA = 0x1000027;
	public uint EVENT_WEB_RECEIVE_INIT_USER_DATA = 0x1000028;
	
	public uint EVENT_WEB_SEND_UDPATE_USER_DATA = 0x1000029;
	public uint EVENT_WEB_RECEIVE_UDPATE_USER_DATA = 0x100002a;
	
	public uint EVENT_WEB_SEND_UPDATE_USER_TASK = 0x100002c;
	public uint EVENT_WEB_RECEIVE_UPDATE_USER_TASK = 0x100002e;
	
	public uint EVENT_WEB_SEND_INIT_USER_STORAGE = 0x1000030;
	public uint EVENT_WEB_RECEIVE_INIT_USER_STORAGE = 0x1000031;
	
	public uint EVENT_WEB_SEND_UPDATE_USER_STORAGE = 0x1000032;
	public uint EVENT_WEB_RECEIVE_UPDATE_USER_STORAGE = 0x1000033;
	
	public uint EVENT_WEB_SEND_Add_USER_STORAGE = 0x1000034;
	public uint EVENT_WEB_RECEIVE_Add_USER_STORAGE = 0x1000036;
	
	public uint EVENT_WEB_SEND_REMOVE_USER_STORAGE = 0x1000038;
	public uint EVENT_WEB_RECEIVE_REMOVE_USER_STORAGE = 0x100003a;
	
	public uint EVENT_WEB_SEND_UPDATE_VISIBLE_DATA= 0x100003c;
	public uint EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA = 0x100003e;
	
	public uint EVENT_WEB_SEND_UPDATE_VISIBLE_ENEMY_DATA = 0x1100000;
	public uint EVENT_WEB_RECEIVE_UPDATE_VISIBLE_ENEMY_DATA = 0x1100002;
	
	public uint EVENT_WEB_SEND_UPDATE_ENEMY_POS = 0x1100004;
	public uint EVENT_WEB_RECEIVE_UPDATE_ENEMY_POS = 0x1100006;
	
	public uint EVENT_WEB_SEND_UPDATE_ENEMY_DATA = 0x1100008;
	public uint EVENT_WEB_RECEIVE_UPDATE_ENEMY_DATA = 0x110000a;
	
	public uint EVENT_WEB_SEND_REQUEST_NPC_DATA = 0x1200000;
	public uint EVENT_WEB_RECEIVE_REQUEST_NPC_DATA = 0x1200002;
	
	public uint EVENT_WEB_SEND_BUY_ITEM = 0x1200004;
	
	public uint EVENT_WEB_SEND_ACCEPT_TASK = 0x1200010;
	
	public uint EVENT_WEB_SEND_EXIT_NPC = 0x1200010;
	public uint EVENT_WEB_RECEIVE_EXIT_NPC = 0x1200012;
	
	public uint EVENT_WEB_SEND_UPDATE_NPC_POS = 0x1200014;
	public uint EVENT_WEB_RECEIVE_UPDATE_NPC_POS = 0x1200016;
	
	public uint EVENT_WEB_SEND_INIT_TALENT_DATA = 0x1200020;
	public uint EVENT_WEB_RECEIVE_INIT_TALENT_DATA = 0x1200021;
	
	public uint EVENT_WEB_SEND_UDPATE_TALENT_DATA = 0x1200022;
	public uint EVENT_WEB_RECEIVE_UPDATE_TALENT_DATA = 0x1200023;
	
	public uint EVENT_WEB_SEND_SWIP_PSTORAGE_ITEM = 0x1200024;
	public uint EVENT_WEB_RECEIVE_SWIP_PSTORAGE_ITEM = 0x1200026;

    public uint EVENT_WEB_SEND_INIT_SCENE_DATA = 0x1200028;
    public uint EVENT_WEB_RECEIVE_INIT_SCENE_DATA = 0x120002a;
	
	public uint EVENT_WEB_SEND_SWITCH_SCENE = 0x1200030;
	public uint EVENT_WEB_RECEIVE_SWITCH_SCENE= 0x1200032;

    public uint EVENT_WEB_SEND_ACTIVE_STORY = 0x1200040;
    public uint EVENT_WEB_RECEIVE_ACTIVE_STORY = 0x1200042;

    public uint EVENT_WEB_SEND_OVER_STORY = 0x1200044;
    public uint EVENT_WEB_RECEIVE_OVER_STORY = 0x1200046;

    public uint EVENT_WEB_SEND_INIT_SCENE_NPC = 0x1200044;
    public uint EVENT_WEB_RECEIVE_INIT_SCENE_NPC = 0x1200046;

    public uint EVENT_WEB_SEND_UPDATE_NPC = 0x1200050;
    public uint EVENT_WEB_RECEIVE_UPDATE_NPC = 0x1200052;

    public uint EVENT_WEB_SEND_UPDATE_NPC_PACKAGE = 0x1200054;
    public uint EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE = 0x1200056;

    public uint EVENT_WEB_SEND_UPDATE_NPC_TASK = 0x1200058;
    public uint EVENT_WEB_RECEIVE_UPDATE_NPC_TASK = 0x120005a;
}

//0x2000000//
public class EventSys
{
	//key键检测(0x2000000~0x20000ff)------------------------------------//
	public uint EVENT_SYS_KEYCODE_RETURN = 0x0000016;
	public uint EVENT_SYS_KEYCODE_ESCAPE = 0x0000017;
	public uint EVENT_SYS_KEYCODE_HOME = 0x0000018;
	public uint EVENT_SYS_KEYCODE_MENU = 0x0000019;
	//key键检测------------------------------------//
	
	public uint EVENT_SYS_ERROR = 0x2000100;
}



//0x3000030//
public class EventFighting
{
	public uint EVENT_FIGHT_START_LEVEL = 0x3000000;
	public uint EVENT_FIGHT_STOP_LEVEL = 0x3000001;
	public uint EVENT_FIGHT_CONTINUE_LEVEL = 0x3000002;
	public uint EVENT_FIGHT_END_LEVEL = 0x3000003;

	public uint EVENT_FIGHT_HIDE_LEVEL = 0x3000010;//Hide MainCamera
	public uint EVENT_FIGHT_SHOW_LEVEL = 0x3000010;

	public uint EVENT_FIGHT_INIT_LEVEL = 0x3000020;//Hide MainCamera
	public uint EVENT_FIGHT_INIT_LEVEL_FINISH = 0x3000021;
	public uint EVENT_FIGHT_INIT_LEVEL_START = 0x3000022;//Create Player

	public uint EVENT_FIGHT_INIT_NPC = 0x3000030;//Hide MainCamera
	public uint EVENT_FIGHT_HIDE_NPC = 0x3000031;

	public uint EVENT_FIGHT_INIT_REFRESH_POINT = 0x3000040;
	public uint EVENT_FIGHT_UPDATE_REFRESH_POINT = 0x3000041;
	public uint EVENT_FIGHT_INIT_ENEMY = 0x3000042;
	public uint EVENT_FIGHT_INIT_ENEMY_FINISH = 0x3000043;
	public uint EVENT_FIGHT_HIDE_ENEMY = 0x3000044;
	public uint EVENT_FIGHT_CLEAR_ENEMY = 0x3000045;

	public uint EVENT_FIGHT_INIT_MAINCHARACTER_START = 0x3000050;
    public uint EVENT_FIGHT_INIT_MAINCHARACTER_FINISH = 0x3000052;
    public uint EVENT_FIGHT_STOP_MAINCHARACTER = 0x3000054;
    public uint EVENT_FIGHT_RESUME_MAINCHARACTER = 0x3000056;
    public uint EVENT_FIGHT_UPDATE_MAINCHARACTER = 0x3000058;

	public uint EVENT_FIGHT_CLICK_POS = 0x3001000;
	public uint EVENT_FIGHT_CLICK_NPC = 0x3001002;

    public uint EVENT_FIGHT_START_STORY = 0x3002000;
    public uint EVENT_FIGHT_END_STORY = 0x3002002;

	public uint EVENT_FIGHT_TRIGGER_ENTER_NPC = 0x3002004;
	public uint EVENT_FIGHT_TRIGGER_EXIT_NPC = 0x3002006;
}

//0x400000c//
public class EventData
{
	public uint EVENT_DATA_FAIL_UPDATEFILE = 0x4000000;//更新文件失败//
	public uint EVENT_DATA_FAIL_SETTASKLIST = 0x4000001;//设置任务列表失败//
	public uint EVENT_DATA_FAIL_GETLOCALLIST = 0x4000002;//获取Local更新列表失败//
	public uint EVENT_DATA_FAIL_GETSERVERLIST = 0x4000003;//获取Server更新列表失败//
	public uint EVENT_DATA_FAIL_SETLOCALLIST = 0x4000004;//写入Local更新列表失败//
	
	
	public uint EVENT_DATA_STARTCOPY = 0x4000005;//开始复制文件//
	public uint EVENT_DATA_SETCOPYSTATUS = 0x4000006;//改变复制文件流程状态//
	public uint EVENT_DATA_SUCCESSCOPYSINGLEFILE = 0x4000007;//单个文件复制成功//
	public uint EVENT_DATA_FINISHEDCOPY = 0x4000008;//复制文件完毕//
	
	public uint EVENT_DATA_STARTUPDATE = 0x4000009;//开启版本更新//
	public uint EVENT_DATA_SETUPDATESTATUS = 0x400000a;//改变更新版本流程状态//
	public uint EVENT_DATA_SUCCESSUPDATESINGLEFILE = 0x400000b;//单个文件下载成功//
	public uint EVENT_DATA_FINISHEDUPDATE = 0x400000c;//更新文件完毕//
}

//0x5000031//
public class EventInput
{
	public uint EVENT_INPUT_STOP_INPUT = 0x5000000;
	public uint EVENT_INPUT_RESUME_INPUT = 0x5000001;
}

public class GameEvent
{
	public static EventUI UIEvent = new EventUI();
	public static EventWeb WebEvent = new EventWeb();
	public static EventSys SysEvent = new EventSys();
	public static EventFighting FightingEvent = new EventFighting();
	public static EventData DataEvent = new EventData();
	public static EventInput InputEvent = new EventInput();
	
}
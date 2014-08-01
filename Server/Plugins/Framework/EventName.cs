using UnityEngine;
using System.Collections;

namespace Server
{
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

        public uint EVENT_WEB_SEND_UDPATE_USER_DATA = 0x1000028;
        public uint EVENT_WEB_RECEIVE_UDPATE_USER_DATA = 0x100002a;

        public uint EVENT_WEB_SEND_UDPATE_USER_TASK = 0x100002c;
        public uint EVENT_WEB_RECEIVE_UDPATE_USER_TASK = 0x100002e;

        public uint EVENT_WEB_SEND_UDPATE_USER_STORAGE = 0x1000030;
        public uint EVENT_WEB_RECEIVE_UDPATE_USER_STORAGE = 0x1000032;

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
    }
    public class EventWebView
    {
        public uint EVENT_WEBVIEW_OPEN_ANNOUNCEMENT = 0xe000000;
    }
    public class EventData
    {

    }
    public class EventFighting
    {
        public uint EVENT_FIGHT_NEW_PLAYER = 0xc000000;
        public uint EVENT_FIGHT_DESTROY_PLAYER = 0xc000002;

        public uint EVENT_FIGHT_NEW_ENEMY = 0xc000004;
        public uint EVENT_FIGHT_DESTROY_ENEMY = 0xc000006;

        public uint EVENT_FIGHT_NEW_NPC = 0xc000008;
        public uint EVENT_FIGHT_DESTROY_NPC = 0xc00000a;

    }
    public class EventSys
    {
        public uint EVENT_SYS_ERROR = 0x2000000;
    }
    public class EventInput
    { }
    public class GameEvent
    {
        public static EventWeb WebEvent = new EventWeb();
        public static EventFighting FightingEvent = new EventFighting();
        public static EventSys SysEvent = new EventSys();
        public static EventInput InputEvent = new EventInput();
        public static EventData DataEvent = new EventData();
        public static EventWebView WebViewEvent = new EventWebView();
    }
}
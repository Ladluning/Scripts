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
    }
    public class EventSys
    {

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
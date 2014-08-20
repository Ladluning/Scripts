using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Trigger_Story : Controller {

    public string mActiveStoryID;
    public string mTriggerID;
    public string mTriggerTag;
    void OnEnable()
    {
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_ACTIVE_STORY, OnHandleReceiveActiveStory);
    }

    void OnDisable()
    {
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_ACTIVE_STORY, OnHandleReceiveActiveStory);
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag != mTriggerTag)
            return;

        gameObject.collider.enabled = false;
        OnHandleTriggerEnter();
    }

    protected virtual void OnHandleTriggerEnter()
    {
        ActiveStory();
    }

    protected virtual void ActiveStory()
    {
        Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_ACTIVE_STORY);
        ((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
        ((Dictionary<string, object>)tmpSend["results"]).Add("target", mTriggerID);
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_ACTIVE_STORY, tmpSend);
    }

    protected virtual object OnHandleReceiveActiveStory(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != Client_User.Singleton().GetID() && (string)tmpJson["results"]["target"] != mTriggerID)
            return null;

        if ((bool)tmpJson["results"]["success"])
        {
            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_START_STORY, mActiveStoryID);
        }

        return null;
    }
}

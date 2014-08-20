using UnityEngine;
using System.Collections;

public class UI_Story_Manager : Controller {

    UI_Story_Interface mStoryInterface;

    public string[] mStoryLuaList;
	void OnEnable()
	{
        this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_START_STORY,OnHandleStartStory);
        this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_END_STORY,OnHandleEndStory);
	}

	void OnDisable()
	{
        this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_START_STORY, OnHandleStartStory);
        this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_END_STORY, OnHandleEndStory);
	}

    void Awake()
    {
        mStoryInterface = gameObject.GetComponent<UI_Story_Interface>();
    }

    void Start()
    {
        for (int i = 0; i < mStoryLuaList.Length; ++i)
        { 
            mStoryInterface.RegistFile(mStoryLuaList[i]);
        }
    }

    object OnHandleStartStory(object pSender)
    {
        if (!IsStoryExist((string)pSender))
            return null;

        mStoryInterface.StartStory((string)pSender);
        this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_STOP_MAINCHARACTER, null);
        return null;
    }

    object OnHandleEndStory(object pSender)
    {
        this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_RESUME_MAINCHARACTER, null);
        return null;
    }

    bool IsStoryExist(string Name)
    {
        for (int i = 0; i < mStoryLuaList.Length; ++i)
        {
            if (mStoryLuaList[i] == Name)
                return true;
        }
        return false;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// panel rect 1500 780   gridPos -739.7 354.7   8,7  1.04     rank scale 0.006
public class Game_Scene_Visible_Manager : Controller {

    Dictionary<string, Client_User> mVisibleUserList = new Dictionary<string, Client_User>();
    List<string> mRemoveUserList = new List<string>();
    List<string> mReveiveUserList = new List<string>();
    void OnEnable()
    {
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_VISIBLE_DATA,OnHandleUpdateVisibleData);
    }

    void OnDisable()
    {
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_VISIBLE_DATA, OnHandleUpdateVisibleData);
    }

    object OnHandleUpdateVisibleData(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);

        mRemoveUserList.Clear();
        mReveiveUserList.Clear();

        for (int i = 0; i < tmpJson["results"]["visibles"].Count; i++)
        {
            mReveiveUserList.Add((string)tmpJson["results"]["visibles"][i]["udid"]);
            if (!mVisibleUserList.ContainsKey((string)tmpJson["results"]["visibles"][i]["udid"]))
            { 
                
            }
        }

        foreach (string key in mVisibleUserList.Keys)
        {
            if (!mReveiveUserList.Contains(key))
            {
                mRemoveUserList.Add(key);
            }
        }

        for(int i=0;i<mRemoveUserList.Count;i++)
        {
            mVisibleUserList.Remove(mRemoveUserList[i]);
        }

        return null;
    }
}

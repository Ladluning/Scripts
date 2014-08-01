using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_Transform : Controller
{
    private Vector3 LastPosition;
    void Update()
    {
        if (LastPosition != transform.position)
        {
            Dictionary<string, object> tmpCmd = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_x", transform.position.x);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_y", transform.position.y);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_z", transform.position.z);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_x", transform.eulerAngles.x);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_y", transform.eulerAngles.y);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_z", transform.eulerAngles.z);
            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS, tmpCmd);
        }
        LastPosition = transform.position;
    }


    object OnHandleReceiveUpdatePos(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        Debug.Log(MiniJSON.Json.Serialize(pSender));

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));
        transform.eulerAngles = new Vector3((float)(tmpJson["results"]["rotate_x"]), (float)(tmpJson["results"]["rotate_y"]), (float)(tmpJson["results"]["rotate_z"]));

        return null;
    }
}

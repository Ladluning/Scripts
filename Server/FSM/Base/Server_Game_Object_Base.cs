using UnityEngine;
using System.Collections;
namespace Server
{
    public class Server_Game_Object_Base : Controller
    {

        //protected TD_ObjectProperty mObjectPreperty;
        //protected Game_Player_Info_Manager mObjectPreperty;
        protected GameObject mCurrentObject;
        protected Transform mCurrentTransform;
        //protected 
        public virtual void Init()
        {
            //mObjectPreperty = gameObject.GetComponent<TD_ObjectProperty>();
            mCurrentObject = gameObject;
            mCurrentTransform = transform;
        }
    }
}

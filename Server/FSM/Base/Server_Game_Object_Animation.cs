using UnityEngine;
using System.Collections;

namespace Server
{
public class Server_Game_Object_Animation : Server_Game_Object_Base
{
    [HideInInspector]
    public string mCurrentAnimationName;

    void OnDisable()
    {
        mCurrentAnimationName = "disable";
    }

    public void PlayAnimation(string AnimationName)
    {
        mCurrentAnimationName = AnimationName;
    }
}
}

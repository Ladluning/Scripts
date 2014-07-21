using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_FSM_Enemy_Base_Controller : Server_Game_FSM_Controller
    {
        [HideInInspector]
        public Server_Game_Enemy_Base mFather;
        [HideInInspector]
        public EnemyConfigInfo mEnemyInfo;

        public string GetCurrentAnimation()
        {
            Server_Game_Object_Animation tmp;
            if ((tmp = mStateMap[mCurrentState].GetComponent<Server_Game_Object_Animation>()) != null)
                return tmp.mCurrentAnimationName;

            return "disable";
        }

        public override void StartState(int nStateID)
        {
            base.StartState(nStateID);
            mFather.SetChanged();
        }

        public override void SwitchState(int nNextState)
        {
            base.SwitchState(nNextState);
            mFather.SetChanged();
        }
    }
}

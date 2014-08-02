using UnityEngine;
using System.Collections;
namespace Server
{
    public class Server_Game_FSM_NPC_Base_Controller : Server_Game_FSM_Controller
    {

        [HideInInspector]
        public Server_Game_NPC_Base mFather;

        public string GetCurrentAnimation()
        {
            Server_Game_Object_Animation tmp;
            if ((tmp = mStateMap[mCurrentState].GetComponent<Server_Game_Object_Animation>()) != null)
                return tmp.mCurrentAnimationName;

            return "disable";
        }

        public virtual void Init(Server_Game_NPC_Base Father)
        {
            mFather = Father;
        }

		public Server_Game_NPC_Base GetFather()
		{
			return mFather;
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

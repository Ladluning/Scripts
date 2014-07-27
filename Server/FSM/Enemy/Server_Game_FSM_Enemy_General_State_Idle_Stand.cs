using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_FSM_Enemy_General_State_Idle_Stand : Server_Game_FSM_State_Base
    {
        public Server_Game_FSM_Enemy_General_State_Idle_Stand(Server_Game_FSM_Controller FSMController) : base(FSMController) { }
        // Use this for initialization
        private float mStandTimer = 0;
        private float mStandTime;
        public override void Init()
        {
            mStateComponent.Add("Server_Game_Object_SearchTarget");
            mStateComponent.Add("Server_Game_Object_Animation");
            base.Init();

            GetComponent<Server_Game_Object_SearchTarget>().InitSearchType((uint)E_Actor_Type.Enemy);
        }

        public override void OnEnter()
        {
            base.OnEnter();

			mStandTimer = 0;
            mStandTime = ((Server_Game_FSM_Enemy_General_Controller)mController).mEnemyInfo.IdleStandTime;

            GetComponent<Server_Game_Object_Animation>().PlayAnimation("idle");
        }

        public override void OnLoop() 
        {
            if (GetComponent<Server_Game_Object_SearchTarget>().GetTargetList().Count > 0)
            {
                //mController.SwitchState((int)E_State_Enemy_General.Move);
                return;
            }

            mStandTimer += Time.deltaTime;
            if (mStandTimer > mStandTime)
            {
                mController.SwitchState((int)E_State_Enemy_General.Idle_Move);

                Debug.Log(mController.name + "--Switch State--Idle_Move");
            }
        }

        public override void OnExit()
        {
			mStandTimer = 0;
            base.OnExit();
        }
    }
}

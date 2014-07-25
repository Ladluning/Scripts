using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_FSM_NPC_Shopper_State_Idle_Stand : Server_Game_FSM_State_Base
    {
        public Server_Game_FSM_NPC_Shopper_State_Idle_Stand(Server_Game_FSM_Controller FSMController) : base(FSMController) { }
        // Use this for initialization
        private float mStandTimer = 0;
        private float mStandTime;
        public override void Init()
        {
            mStateComponent.Add("Server_Game_Object_Animation");
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            mStandTimer = 0;
            mStandTime = ((Server_Game_FSM_Enemy_General_Controller)mController).mEnemyInfo.IdleStandTime;

            GetComponent<Server_Game_Object_Animation>().PlayAnimation("Idle");
        }

        public override void OnLoop()
        {
            mStandTimer += Time.deltaTime;
            if (mStandTimer > mStandTime)
            {
                mController.SwitchState((int)E_State_NPC_Trader.Idle_Move);

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

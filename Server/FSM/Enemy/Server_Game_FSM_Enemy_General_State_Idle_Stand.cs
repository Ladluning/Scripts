﻿using UnityEngine;
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
            base.Init();

            GetComponent<Server_Game_Object_SearchTarget>().InitSearchType((uint)E_Actor_Type.Enemy);
        }


        public override void OnEnter()
        {
            base.OnEnter();

            mStandTime = ((Server_Game_FSM_Enemy_General_Controller)mController).mEnemyInfo.IdleStandTime;
        }

        public override void OnLoop() 
        {
            if (GetComponent<Server_Game_Object_SearchTarget>().GetTargetList().Count > 0)
            {
                //mController.SwitchState((int)E_State_GeneralEnemy.Move);
                return;
            }

            mStandTimer += Time.deltaTime;
            if (mStandTimer > mStandTime)
            {
                mController.SwitchState((int)E_State_GeneralEnemy.Idle_Move);

                Debug.Log(mController.name + "--Switch State--Idle_Move");
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}

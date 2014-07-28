﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_FSM_NPC_Trader_State_Idle_Move : Server_Game_FSM_State_Base
    {
        public Server_Game_FSM_NPC_Trader_State_Idle_Move(Server_Game_FSM_Controller FSMController) : base(FSMController) { }
        // Use this for initialization
        private List<Vector3> mMovePath = new List<Vector3>();
        private Server_Game_FSM_NPC_Trader_Controller mTraderController;
        public override void Init()
        {
            mStateComponent.Add("Server_Game_Object_SearchTarget");
            mStateComponent.Add("Server_Game_Object_Move");
            mStateComponent.Add("Server_Game_Object_Animation");
            base.Init();

            mTraderController = GetController<Server_Game_FSM_NPC_Trader_Controller>();
            GetComponent<Server_Game_Object_SearchTarget>().InitSearchType((uint)E_Actor_Type.Enemy);
        }


        public override void OnEnter()
        {
            base.OnEnter();
            GetComponent<Server_Game_Object_Animation>().PlayAnimation("Walk");
        }

        public override void OnLoop()
        {
            if (GetComponent<Server_Game_Object_SearchTarget>().GetTargetList().Count > 0)
            {
                //mController.SwitchState((int)E_State_Enemy_General.Move);
                return;
            }
            mTraderController.mFather.SetChanged();
        }

        public override void OnExit()
        {
            base.OnExit();

            mMovePath.Clear();
        }

        public override void OnDrawGizmos()
        {
            if (mMovePath == null)
                return;

            for (int i = 0; i < mMovePath.Count - 1; ++i)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(mMovePath[i], mMovePath[i + 1]);
            }

        }

        object OnActionOver(object pSender)
        {
            mController.SwitchState((int)E_State_Enemy_General.Idle_Stand);
            return null;
        }
    }
}
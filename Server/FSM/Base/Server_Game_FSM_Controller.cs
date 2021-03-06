﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_FSM_Controller : Controller
    {
        protected Dictionary<int, Server_Game_FSM_State_Base> mStateMap = new Dictionary<int, Server_Game_FSM_State_Base>();
        protected int mCurrentState = -1;
        protected bool mAIStop = false;
        protected Server_Game_Scene_Manager mManager;

        protected virtual void Awake()
        {

        }

        protected void InitState()
        {
            foreach (int tmpKey in mStateMap.Keys)
            {
                mStateMap[tmpKey].Init();
            }
        }

        public int GetCurrentState()
        {
            return mCurrentState;
        }

        public virtual void StartState(int nStateID)
        {
            mCurrentState = nStateID;
            mStateMap[mCurrentState].OnEnter();
        }

        public virtual void SwitchState(int nNextState)
        {
            if (mCurrentState != null && nNextState != mCurrentState && mStateMap[mCurrentState].GetIsCouldTranslate(nNextState))
            {
                mStateMap[mCurrentState].OnExit();

                mCurrentState = nNextState;
                mStateMap[mCurrentState].OnEnter();
            }
        }

        protected virtual void Update()
        {
            if (mAIStop)
                return;

            mStateMap[mCurrentState].OnLoop();
        }

        protected virtual void OnDrawGizmos()
        {
            mStateMap[mCurrentState].OnDrawGizmos();
        }

    }
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
	public class Server_Game_FSM_NPC_General_State_Idle_Move : Server_Game_FSM_State_Base
    {
		public Server_Game_FSM_NPC_General_State_Idle_Move(Server_Game_FSM_Controller FSMController) : base(FSMController) { }
        // Use this for initialization
        private List<Vector3> mMovePath = new List<Vector3>();
        private Server_Game_FSM_NPC_Base_Controller mEnemyController;
        public override void Init()
        {
            mStateComponent.Add("Server_Game_Object_SearchTarget");
            mStateComponent.Add("Server_Game_Object_Move");
            mStateComponent.Add("Server_Game_Object_Animation");
            base.Init();

            mEnemyController = GetController<Server_Game_FSM_NPC_Base_Controller>();
            GetComponent<Server_Game_Object_SearchTarget>().InitSearchType((uint)E_Actor_Type.Enemy,10f);
        }


        public override void OnEnter()
        {
            base.OnEnter();

            Int2 TmpCurrentPos = mEnemyController.mFather.GetManager().ConvertWorldPosToMapPos(mEnemyController.transform.localPosition);
			Int2 TmpTargetPos = mEnemyController.mFather.GetSpawnPoint().GetEmptyRandomPos(TmpCurrentPos);
			if (mEnemyController.mFather.GetManager().StartFindPath(TmpCurrentPos, TmpTargetPos))
            {
                for (int i = 0; i < mEnemyController.mFather.GetManager().MovePath.Count; ++i)
                {
                    mMovePath.Add(mEnemyController.mFather.GetManager().MovePath[i]);
                }
            }
//			Debug.Log (mEnemyController.name);
//			Debug.Log (mEnemyController.mFather.GetManager().GetPointIsInMap(TmpCurrentPos)+TmpCurrentPos.Printf()+" "+mEnemyController.transform.localPosition+" "+mEnemyController.mFather.GetManager().ConvertMapPosToWorldPos(TmpCurrentPos));
//			Debug.Log (mEnemyController.mFather.GetManager().GetPointIsInMap(TmpTargetPos)+TmpTargetPos.Printf());
			if (!mEnemyController.mFather.GetManager ().GetPointIsInMap (TmpTargetPos) ||! mEnemyController.mFather.GetManager ().GetPointIsInMap (TmpCurrentPos)) {
				Debug.LogError ("Error Pos");	
			}
			GetComponent<Server_Game_Object_Move>().MoveToTarget(mMovePath, OnActionOver, 1);
            GetComponent<Server_Game_Object_Animation>().PlayAnimation("walk");
            //更新地图数据
//			Debug.Log(mMovePath.Count+" Find Path Form" + TmpCurrentPos.x + " " + TmpCurrentPos.y + " To" + TmpTargetPos.x + " " + TmpTargetPos.y);
        }

        public override void OnLoop() 
        {
            if (GetComponent<Server_Game_Object_SearchTarget>().GetTargetList().Count > 0)
            {
                //mController.SwitchState((int)E_State_Enemy_General.Move);
                return;
            }
			mEnemyController.mFather.SetChanged();
        }

        public override void OnExit()
        {
            base.OnExit();

            mMovePath.Clear();
        }

        public override void OnDrawGizmos()
        {
            if(mMovePath == null)
                return;

            for(int i=0;i<mMovePath.Count-1;++i)
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

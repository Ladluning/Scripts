using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Game_FSM_NPC_General_State_Move : Server_Game_FSM_State_Base
    {
        public Server_Game_FSM_NPC_General_State_Move(Server_Game_FSM_Controller FSMController) : base(FSMController) { }


		private Server_Game_FSM_NPC_Base_Controller mEnemyController;
        // Use this for initialization
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

			string tmpTargetID = GetComponent<Server_Game_Object_SearchTarget> ().GetClosestTarget ();
			//Transform tmpTarget = mEnemyController.mFather.mManager.FindTarget (TargetID);
        }

        public virtual void OnLoop() { }

        public virtual void OnExit()
        {
            base.OnExit();
        }
		//寻找目标周围可站区域
    }
}

using UnityEngine;
using System.Collections;

namespace Server
{
    public class Server_Game_FSM_NPC_General_State_Idle_Stand : Server_Game_FSM_State_Base
    {

        public Server_Game_FSM_NPC_General_State_Idle_Stand(Server_Game_FSM_Controller FSMController) : base(FSMController) { }

        public override void Init()
        {
            mStateComponent.Add("Server_Game_Object_Animation");
            base.Init();

        }

        public override void OnEnter()
        {
            base.OnEnter();

            GetComponent<Server_Game_Object_Animation>().PlayAnimation("Idle");
        }

        public override void OnLoop() 
        {

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}

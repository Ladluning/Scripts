using UnityEngine;
using System.Collections;
namespace Server
{
    public class Server_Game_FSM_Enemy_General_State_Attack : Server_Game_FSM_State_Base
    {
        public Server_Game_FSM_Enemy_General_State_Attack(Server_Game_FSM_Controller FSMController) : base(FSMController) { }
        public override void Init()
        {
            base.Init();
        }


        public override void OnEnter()
        {
            base.OnEnter();
        }

        public virtual void OnLoop() { }

        public virtual void OnExit()
        {
            base.OnExit();
        }
    }
}

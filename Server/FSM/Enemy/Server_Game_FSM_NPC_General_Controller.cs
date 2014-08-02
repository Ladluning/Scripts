using UnityEngine;
using System.Collections;

namespace Server
{
    public enum E_State_NPC_General
    {
        Idle_Move = 0,
        Idle_Stand,
        Move,
        Attack,
        Away,
        Died,
        Escape,
    }

    public class Server_Game_FSM_NPC_General_Controller : Server_Game_FSM_NPC_Base_Controller
    {

        // Use this for initialization
        void OnEnable()
        {

        }
        
        void OnDisable()
        {

        }
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Init(Server_Game_NPC_Base Father)
        {
            base.Init(Father);

			mStateMap.Add((int)E_State_NPC_General.Idle_Stand, new Server_Game_FSM_NPC_General_State_Idle_Stand(this));
			mStateMap[(int)E_State_NPC_General.Idle_Stand].AddTranslate((int)E_State_NPC_General.Idle_Move);

			mStateMap.Add((int)E_State_NPC_General.Idle_Move, new Server_Game_FSM_NPC_General_State_Idle_Move(this));
			mStateMap[(int)E_State_NPC_General.Idle_Move].AddTranslate((int)E_State_NPC_General.Idle_Stand);


            this.InitState();
			this.StartState((int)E_State_NPC_General.Idle_Stand);
        }

        void Start()
        {

        }

        protected override void Update()
        {
            base.Update();
        }

    }
}

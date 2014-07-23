using UnityEngine;
using System.Collections;

namespace Server
{
    public enum E_State_NPC_Trader
    {
        Idle_Stand = 0,
        Idle_Move,
    }

    public class Server_Game_FSM_NPC_Trader_Controller : Server_Game_FSM_NPC_Base_Controller
    {

        public override void Init(Server_Game_NPC_Base Father)
        {
            base.Init(Father);

            mStateMap.Add((int)E_State_NPC_Trader.Idle_Stand, new Server_Game_FSM_NPC_Trader_State_Idle_Stand(this));
            mStateMap[(int)E_State_NPC_Trader.Idle_Stand].AddTranslate((int)E_State_NPC_Trader.Idle_Move);

            mStateMap.Add((int)E_State_NPC_Trader.Idle_Move, new Server_Game_FSM_NPC_Trader_State_Idle_Stand(this));
            mStateMap[(int)E_State_NPC_Trader.Idle_Move].AddTranslate((int)E_State_NPC_Trader.Idle_Stand);


            this.InitState();
            this.StartState((int)E_State_NPC_General.Idle_Stand);
        }
    }
}

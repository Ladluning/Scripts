﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_FSM_NPC_Trader_Controller : Server_Game_FSM_NPC_Base_Controller
    {
        [HideInInspector]
        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();

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
    }
}

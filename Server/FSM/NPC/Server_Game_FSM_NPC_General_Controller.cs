﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public enum E_State_NPC_General
    {
        Idle_Stand = 0,
    }

    public class Server_Game_FSM_NPC_General_Controller : Server_Game_FSM_NPC_Base_Controller
    {

        public override void Init(Server_Game_NPC_Base Father)
        {
            base.Init(Father);

            mStateMap.Add((int)E_State_NPC_General.Idle_Stand, new Server_Game_FSM_NPC_General_State_Idle_Stand(this));


            this.InitState();
            this.StartState((int)E_State_NPC_General.Idle_Stand);
        }
    }
}

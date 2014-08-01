using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_NPC_Trader : Server_Game_NPC_Shopper
    {
        public override void Init()
        {
            mController = gameObject.AddComponent<Server_Game_FSM_NPC_Trader_Controller>();
            mController.Init(this);
			InitNPC ();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }
    }
}

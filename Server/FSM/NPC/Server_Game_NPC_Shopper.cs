using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	[RequireComponent( typeof(Server_Game_NPC_Data_Transform))]
	[RequireComponent( typeof(Server_Game_NPC_Data_Property))]
	[RequireComponent( typeof(Server_Game_NPC_Data_Package))]
    public class Server_Game_NPC_Shopper : Server_Game_NPC_Base
    {
        public override void Init()
        {
			base.Init();

            mController = gameObject.AddComponent<Server_Game_FSM_NPC_Shopper_Controller>();
            mController.Init(this);



            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }
    }
}

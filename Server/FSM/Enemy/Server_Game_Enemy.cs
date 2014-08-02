using UnityEngine;
using System.Collections;

namespace Server
{
	[RequireComponent( typeof(Server_Game_NPC_Data_Transform))]
	[RequireComponent( typeof(Server_Game_NPC_Data_Property))]
    public class Server_Game_Enemy : Server_Game_NPC_Base
    {

        public override void Init()
        {
			mDataComponentList.Add(typeof(Server_Game_NPC_Data_Transform),gameObject.GetComponent<Server_Game_NPC_Data_Transform>());
			mDataComponentList.Add(typeof(Server_Game_NPC_Data_Property),gameObject.GetComponent<Server_Game_NPC_Data_Property>());

            mController = gameObject.AddComponent<Server_Game_FSM_NPC_General_Controller>();
            mController.Init(this);

			base.Init();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_ENEMY,this);
        }

    }
}

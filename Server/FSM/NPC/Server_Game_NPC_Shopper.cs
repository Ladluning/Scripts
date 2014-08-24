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
		Server_Game_NPC_Data_Transform mTransform;
		Server_Game_NPC_Data_Property  mProperty;
		Server_Game_NPC_Data_Package   mPackage;

		[HideInInspector]
		public Server_Game_Scene_NPC_Manager mNPCManager;
		void Awake()
		{
			mID = gameObject.name;

			Init ();
		}

        public override void Init()
        {
			mNPCManager = GameTools.FindComponentInHierarchy<Server_Game_Scene_NPC_Manager>(transform);
			mNPCManager.RegistNPC (this);

			mDataComponentList.Add(typeof(Server_Game_NPC_Data_Transform),gameObject.GetComponent<Server_Game_NPC_Data_Transform>());
			mDataComponentList.Add(typeof(Server_Game_NPC_Data_Property),gameObject.GetComponent<Server_Game_NPC_Data_Property>());
			mDataComponentList.Add(typeof(Server_Game_NPC_Data_Package),gameObject.GetComponent<Server_Game_NPC_Data_Package>());

			mController = gameObject.AddComponent<Server_Game_FSM_NPC_Shopper_Controller>();
			mController.Init(this);
			
			base.Init();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }
    }
}

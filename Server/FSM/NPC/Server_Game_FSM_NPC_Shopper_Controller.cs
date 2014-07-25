using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_FSM_NPC_Shopper_Controller : Server_Game_FSM_NPC_Base_Controller
    {

        [HideInInspector]
        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();

        public override void Init(Server_Game_NPC_Base Father)
        {
            base.Init(Father);

            mStateMap.Add((int)E_State_NPC_Trader.Idle_Stand, new Server_Game_FSM_NPC_Shopper_State_Idle_Stand(this));

            this.InitState();
            this.StartState((int)E_State_NPC_Trader.Idle_Stand);
        }
    }
}

using UnityEngine;
using System.Collections;

namespace Server
{

    public class Server_Game_Enemy : Server_Game_Enemy_Serialize
    {

        public override void Init()
        {
            mController = gameObject.AddComponent<Server_Game_FSM_Enemy_General_Controller>();
            mController.mEnemyInfo = mConfigInfo;
            mController.Init(this);

            base.Init();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_ENEMY,this);
        }

    }
}

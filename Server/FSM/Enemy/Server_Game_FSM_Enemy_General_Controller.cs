using UnityEngine;
using System.Collections;

namespace Server
{
    public enum E_State_GeneralEnemy
    {
        Idle_Move = 0,
        Idle_Stand,
        Move,
        Attack,
        Away,
        Died,
        Escape,
    }

    public class Server_Game_FSM_Enemy_General_Controller : Server_Game_FSM_Controller
    {
        [HideInInspector]
        public EnemyConfigInfo mEnemyInfo;
        [HideInInspector]
        public Server_Game_Enemy mFather;
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

        public void Init(Server_Game_Enemy Father)
        {
            mFather = Father;

            mStateMap.Add((int)E_State_GeneralEnemy.Idle_Stand, new Server_Game_FSM_Enemy_General_State_Idle_Stand(this));
            mStateMap[(int)E_State_GeneralEnemy.Idle_Stand].AddTranslate((int)E_State_GeneralEnemy.Idle_Move);

            mStateMap.Add((int)E_State_GeneralEnemy.Idle_Move, new Server_Game_FSM_Enemy_General_State_Idle_Move(this));
            mStateMap[(int)E_State_GeneralEnemy.Idle_Move].AddTranslate((int)E_State_GeneralEnemy.Idle_Stand);


            this.InitState();
            this.StartState((int)E_State_GeneralEnemy.Idle_Stand);
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

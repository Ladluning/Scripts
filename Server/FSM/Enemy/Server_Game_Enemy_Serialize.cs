using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{

    public class Server_Game_Enemy_Serialize : Server_Game_Enemy_Base
    {
        private Dictionary<string, object> tmpSend = new Dictionary<string, object>();
        public Dictionary<string, object> GetSerializeData()
        {
            return tmpSend;
        }
        public override void Init()
        {
            base.Init();

            tmpSend.Add("id", mEnemyID);
            tmpSend.Add("scene", mSceneID);
            tmpSend.Add("mesh", mConfigInfo.MeshID);
            tmpSend.Add("pos_x", mCurrentTransform.localPosition.x);
            tmpSend.Add("pos_y", mCurrentTransform.localPosition.y);
            tmpSend.Add("pos_z", mCurrentTransform.localPosition.z);
            tmpSend.Add("rotate_x", mCurrentTransform.localEulerAngles.x);
            tmpSend.Add("rotate_y", mCurrentTransform.localEulerAngles.y);
            tmpSend.Add("rotate_z", mCurrentTransform.localEulerAngles.z);
            tmpSend.Add("fsm", mController.GetCurrentState());
            tmpSend.Add("ani", mController.GetCurrentAnimation());
            tmpSend.Add("hp", mConfigInfo.HP);
            tmpSend.Add("maxHP", mConfigInfo.MaxHP);
            tmpSend.Add("mp", mConfigInfo.MP);
            tmpSend.Add("maxMP", mConfigInfo.MaxMP);
            tmpSend.Add("attack", mConfigInfo.Attack);
            tmpSend.Add("defend", mConfigInfo.Defend);
            tmpSend.Add("exp", mConfigInfo.EXP);
        }

        void LateUpdate()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            tmpSend.Clear();

            tmpSend["id"]=mEnemyID;
            tmpSend["scene"]=mSceneID;
            tmpSend["mesh"]=mConfigInfo.MeshID;
            tmpSend["pos_x"]=mCurrentTransform.localPosition.x;
            tmpSend["pos_y"]=mCurrentTransform.localPosition.y;
            tmpSend["pos_z"]=mCurrentTransform.localPosition.z;
            tmpSend["rotate_x"]=mCurrentTransform.localEulerAngles.x;
            tmpSend["rotate_y"]=mCurrentTransform.localEulerAngles.y;
            tmpSend["rotate_z"]=mCurrentTransform.localEulerAngles.z;
            tmpSend["fsm"]=mController.GetCurrentState();
            tmpSend["ani"]=mController.GetCurrentAnimation();
            tmpSend["hp"]=mConfigInfo.HP;
            tmpSend["maxHP"]=mConfigInfo.MaxHP;
            tmpSend["mp"]=mConfigInfo.MP;
            tmpSend["maxMP"]=mConfigInfo.MaxMP;
            tmpSend["attack"]=mConfigInfo.Attack;
            tmpSend["defend"]=mConfigInfo.Defend;
            tmpSend["exp"]=mConfigInfo.EXP;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
public class Server_Game_NPC_Base_Serialize : Server_Game_NPC_Base {

		private Dictionary<string, object> tmpSend = new Dictionary<string, object>();
		public Dictionary<string, object> GetSerializeData()
		{
			return tmpSend;
		}
		public override void Init()
		{
			base.Init();
			
			tmpSend.Add("id", mNPCID);
			tmpSend.Add("scene", mSceneID);
			tmpSend.Add("pos_x", mCurrentTransform.localPosition.x);
			tmpSend.Add("pos_y", mCurrentTransform.localPosition.y);
			tmpSend.Add("pos_z", mCurrentTransform.localPosition.z);
			tmpSend.Add("rotate_x", mCurrentTransform.localEulerAngles.x);
			tmpSend.Add("rotate_y", mCurrentTransform.localEulerAngles.y);
			tmpSend.Add("rotate_z", mCurrentTransform.localEulerAngles.z);
			tmpSend.Add("fsm", mController.GetCurrentState());
			tmpSend.Add("ani", mController.GetCurrentAnimation());
		}

		void LateUpdate()
		{
			UpdateData();
		}
		
		public void UpdateData()
		{
			tmpSend.Clear();
			
			tmpSend["id"]=mNPCID;
			tmpSend["scene"]=mSceneID;
			tmpSend["pos_x"]=mCurrentTransform.localPosition.x;
			tmpSend["pos_y"]=mCurrentTransform.localPosition.y;
			tmpSend["pos_z"]=mCurrentTransform.localPosition.z;
			tmpSend["rotate_x"]=mCurrentTransform.localEulerAngles.x;
			tmpSend["rotate_y"]=mCurrentTransform.localEulerAngles.y;
			tmpSend["rotate_z"]=mCurrentTransform.localEulerAngles.z;
			tmpSend["fsm"]=mController.GetCurrentState();
			tmpSend["ani"]=mController.GetCurrentAnimation();
		}
}
}

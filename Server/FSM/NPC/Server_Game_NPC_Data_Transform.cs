using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_NPC_Data_Transform : Server_Game_NPC_Data_Base {

		private Transform mCurrentTransform;
		public Dictionary<string, object> GetSerializeData()
		{
			return tmpSend;
		}
		public override void Init(Server_Game_NPC_Base Father)
		{
			base.Init(Father);

			mCurrentTransform = transform;

			tmpSend.Add("id", mFather.mID);
			tmpSend.Add("scene", mFather.mSceneID);
			tmpSend.Add("type",(int)mFather.mActorType);
			tmpSend.Add("pos_x", mCurrentTransform.localPosition.x);
			tmpSend.Add("pos_y", mCurrentTransform.localPosition.y);
			tmpSend.Add("pos_z", mCurrentTransform.localPosition.z);
			tmpSend.Add("rotate_x", mCurrentTransform.localEulerAngles.x);
			tmpSend.Add("rotate_y", mCurrentTransform.localEulerAngles.y);
			tmpSend.Add("rotate_z", mCurrentTransform.localEulerAngles.z);
			tmpSend.Add("fsm", mFather.GetController().GetCurrentState());
			tmpSend.Add("ani", mFather.GetController().GetCurrentAnimation());
		}
		
		void LateUpdate()
		{
			UpdateData();
		}
		
		public void UpdateData()
		{
			tmpSend.Clear();

			tmpSend["id"]=mFather.mID;
			tmpSend["scene"]=mFather.mSceneID;
			tmpSend["type"] = mFather.mActorType;
			tmpSend["pos_x"]=mCurrentTransform.localPosition.x;
			tmpSend["pos_y"]=mCurrentTransform.localPosition.y;
			tmpSend["pos_z"]=mCurrentTransform.localPosition.z;
			tmpSend["rotate_x"]=mCurrentTransform.localEulerAngles.x;
			tmpSend["rotate_y"]=mCurrentTransform.localEulerAngles.y;
			tmpSend["rotate_z"]=mCurrentTransform.localEulerAngles.z;
			tmpSend["fsm"]=mFather.GetController().GetCurrentState();
			tmpSend["ani"]=mFather.GetController().GetCurrentAnimation();
		}
	}
}

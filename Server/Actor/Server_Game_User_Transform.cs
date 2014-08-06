using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_User_Transform : Server_Game_User_Component {

		protected string mSceneID;

		protected void OnEnable()
		{
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS,OnHandleUpdatePos);
		}
		
		protected void OnDisable()
		{
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS, OnHandleUpdatePos);
		}

		public override void Init (Server_Game_User Father)
		{
			base.Init (Father);

			mCurrentTransform.localPosition = Father.mDataInfo.WorldPos;
			mCurrentTransform.localEulerAngles = Father.mDataInfo.WorldRotate;
			mSceneID = Father.mDataInfo.SceneID;
		}

		public override void UpdateData()
		{
			mUser.mDataInfo.WorldPos = mCurrentTransform.localPosition;
			mUser.mDataInfo.WorldRotate = mCurrentTransform.localEulerAngles;
			mUser.mDataInfo.SceneID = mSceneID;
		}

		object OnHandleUpdatePos(object pSender)
		{
			JsonData tmpJson = new JsonData(pSender);
			mCurrentTransform.localPosition = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));
			mCurrentTransform.localEulerAngles = new Vector3((float)(tmpJson["results"]["rotate_x"]), (float)(tmpJson["results"]["rotate_y"]), (float)(tmpJson["results"]["rotate_z"]));
			SetChanged();
			RequirePosData();
			return null;
		}
		
		public string RequirePosData()
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_POS);
			((Dictionary<string, object>)tmpSend["results"]).Add("id",mUser.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("pos_x", mCurrentTransform.localPosition.x);
			((Dictionary<string, object>)tmpSend["results"]).Add("pos_y", mCurrentTransform.localPosition.y);
			((Dictionary<string, object>)tmpSend["results"]).Add("pos_z", mCurrentTransform.localPosition.z);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_x", mCurrentTransform.localEulerAngles.x);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_y", mCurrentTransform.localEulerAngles.y);
			((Dictionary<string, object>)tmpSend["results"]).Add("rotate_z", mCurrentTransform.localEulerAngles.z);
			((Dictionary<string, object>)tmpSend["results"]).Add("scene", mSceneID);
			
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_POS, tmpSend);
			
			return MiniJSON.Json.Serialize(tmpSend);
		}

		public override void SerializeVisiblePosData(ref Dictionary<string, object> Father)
		{
			if(!GetIsChanged())
				return;

			Dictionary<string, object> tmpSend = new Dictionary<string, object>();
			tmpSend.Add("pos_x", mCurrentTransform.localPosition.x);
			tmpSend.Add("pos_y", mCurrentTransform.localPosition.y);
			tmpSend.Add("pos_z", mCurrentTransform.localPosition.z);
			tmpSend.Add("rotate_x", mCurrentTransform.localEulerAngles.x);
			tmpSend.Add("rotate_y", mCurrentTransform.localEulerAngles.y);
			tmpSend.Add("rotate_z", mCurrentTransform.localEulerAngles.z);
			tmpSend.Add("scene", mSceneID);
			Father.Add("transform",tmpSend);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
	public class Server_Game_User_Talent : Server_Game_User_Component 
	{
		public Server_Struct_Data_Talent mTalentData;

		public override void Init (Server_Game_User Father)
		{
			base.Init (Father);

			mTalentData = Server_Data_Talent.Singleton().GetCopyData();

			Server_Data_Talent.Singleton().SerializeTalent(ref mTalentData,mUser.mDataInfo.mTalent);
			ApplyTalent(mTalentData);
		}

		public Dictionary<string, object> RequireTalentData()
		{
			Dictionary<string, object> tmpSend = SerializeTalentData();
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_TALENT_DATA, tmpSend);
			return tmpSend;
		}

		Dictionary<string, object> SerializeTalentData()
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_TALENT_DATA);
			List<object> tmpData = new List<object>();
            for (int i = 0; i < mTalentData.Talent.Count; ++i)
			{
                SerializeTalentNodeData(ref tmpData, mTalentData.Talent[i].Top);
			}
            ((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("talent", tmpData);
			
			return tmpSend;
		}

        void SerializeTalentNodeData(ref List<object> TargetList,Server_Struct_Data_Talent_Node TargetNode)
        {
            for (int i = 0; i < TargetNode.Child.Count; i++)
            {
                SerializeTalentNodeData(ref TargetList, TargetNode.Child[i]);
            }


            Dictionary<string,object> tmpTalentNode = new Dictionary<string, object>();
			tmpTalentNode.Add("id",TargetNode.NodeID);
			tmpTalentNode.Add("count",TargetNode.CurrentCount);
            tmpTalentNode.Add("max",TargetNode.MaxCount);
            tmpTalentNode.Add("active", TargetNode.Avaliable);
			TargetList.Add(tmpTalentNode);
        }

		object OnHandleClearTalent(object pSender)
		{
			int tmpCurrentTalentCount = 0;
			for (int i = 0; i < mUser.mDataInfo.mTalent.TalentNode.Count; ++i)
			{
				tmpCurrentTalentCount += mUser.mDataInfo.mTalent.TalentNode[i].CurrentCount;
				mUser.mDataInfo.mTalent.TalentNode[i].CurrentCount = 0;
			}
			mUser.mDataInfo.mTalent.StartPage = -1;
			mUser.mDataInfo.mTalent.TalentCount += tmpCurrentTalentCount;

			Server_Data_Talent.Singleton().SerializeTalent(ref mTalentData,mUser.mDataInfo.mTalent);
			ApplyTalent(mTalentData);
			return null;
		}

		object OnHandleAddTalent(object pSender)
		{
			JsonData tmpJson = (JsonData)pSender;

			if(mUser.mDataInfo.mTalent.TalentCount<(int)tmpJson["results"]["count"])//Error
				return null;

			for (int i = 0; i < mUser.mDataInfo.mTalent.TalentNode.Count; ++i)
			{
				if(mUser.mDataInfo.mTalent.TalentNode[i].NodeID == (string)tmpJson["results"]["target"])
				{
					Server_Struct_Data_Talent_Node tmpTalent = GetTargetTalentData(mTalentData,mUser.mDataInfo.mTalent.TalentNode[i].NodeID);

					if((mUser.mDataInfo.mTalent.TalentNode[i].CurrentCount+(int)tmpJson["results"]["count"])>tmpTalent.MaxCount)
						return null;

					mUser.mDataInfo.mTalent.TalentNode[i].CurrentCount += (int)tmpJson["results"]["count"];
					Server_Data_Talent.Singleton().SerializeTalent(ref mTalentData,mUser.mDataInfo.mTalent);
					ApplyTalent(mTalentData);
					return null;
				}
			}
			return null;
		}

		object OnHandleRemoveTalent(object pSender)
		{
			return null;
		}

		void ApplyTalent(Server_Struct_Data_Talent Target)
		{
			for(int i=0;i<Target.Talent.Count;++i)
			{
				ApplyTalent(Target.Talent[i].Top);
			}
		}

		void ApplyTalent(Server_Struct_Data_Talent_Node Target)
		{
			if(Target.Child.Count<=0)
			{
				Target.Avaliable = true;
				return;
			}
			Target.Avaliable = true;
			for(int i=0;i<Target.Child.Count;++i)
			{
				if(Target.Child[i].CurrentCount<Target.Child[i].ActiveCount)
				{
					Target.Avaliable = false;
				}
				ApplyTalent(Target.Child[i]);
			}
		}

		Server_Struct_Data_Talent_Node GetTargetTalentData(Server_Struct_Data_Talent Target,string ID)
		{
			for(int i=0;i<Target.Talent.Count;++i)
			{
				if(Target.Talent[i].Top.NodeID == ID)
					return Target.Talent[i].Top;

				Server_Struct_Data_Talent_Node tmpReturn = GetTargetTalentData(Target.Talent[i].Top,ID);
				if(tmpReturn!=null)
					return tmpReturn;
			}

			return null;
		}

		Server_Struct_Data_Talent_Node GetTargetTalentData(Server_Struct_Data_Talent_Node Target,string ID)
		{
			for(int i=0;i<Target.Child.Count;++i)
			{
				if(Target.Child[i].NodeID == ID)
					return Target;

				Server_Struct_Data_Talent_Node tmpReturn = GetTargetTalentData(Target.Child[i],ID);
				if(tmpReturn!=null)
					return tmpReturn;
			}

			return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Server
{
	[System.Serializable]
	public class Server_Struct_Data_Talent
	{
		public int StartPage = -1;
		public int TalentCount = 0;
		public List<Server_Struct_Data_Talent_Page> Talent = new List<Server_Struct_Data_Talent_Page>();

		public Server_Struct_Data_Talent Copy()
		{
			Server_Struct_Data_Talent tmpData = new Server_Struct_Data_Talent();
			tmpData.StartPage = StartPage;
			tmpData.TalentCount = TalentCount;
			for(int i=0;i<Talent.Count;++i)
			{
				tmpData.Talent.Add(Talent[i].Copy());
			}
			return tmpData;
		}
	}
	[System.Serializable]
	public class Server_Struct_Data_Talent_Page
	{
		public string PageID;
		public string PageName;
		//public bool Avaliable = true;
		public Server_Struct_Data_Talent_Node Top;

		public Server_Struct_Data_Talent_Page Copy()
		{
			Server_Struct_Data_Talent_Page tmpData = new Server_Struct_Data_Talent_Page();
			tmpData.PageID = PageID;
			tmpData.PageName = PageName;
			//tmpData.Avaliable = Avaliable;
			tmpData.Top = Top.Copy();
			return tmpData;
		}
	}
	[System.Serializable]
	public class Server_Struct_Data_Talent_Node
	{
		public string NodeID;
		//public string IconID;
		public bool Avaliable = false;
		//public int DefaultCount;
		public int CurrentCount;
		public int MaxCount;
		public int ActiveCount;
		public List<Server_Struct_Data_Talent_Node> Child = new List<Server_Struct_Data_Talent_Node>();

		public Server_Struct_Data_Talent_Node()
		{
				
		}

		public Server_Struct_Data_Talent_Node(string ID,bool IsUse,int DefaultCount,int Max,int Active)
		{
			NodeID = ID;
			Avaliable = IsUse;
			CurrentCount = DefaultCount;
			MaxCount = Max;
			ActiveCount = Active;	
		}
		public Server_Struct_Data_Talent_Node Copy()
		{
			Server_Struct_Data_Talent_Node tmpData = new Server_Struct_Data_Talent_Node();
			tmpData.NodeID = NodeID;
			//tmpData.IconID = IconID;
			tmpData.Avaliable = Avaliable;
			//tmpData.DefaultCount = DefaultCount;
			tmpData.CurrentCount = CurrentCount;
			tmpData.MaxCount = MaxCount;
			tmpData.ActiveCount = ActiveCount;
			for(int i=0;i<Child.Count;++i)
			{
				tmpData.Child.Add(Child[i].Copy());
			}
			return tmpData;
		}
	}
	[System.Serializable]
	public class Struct_Game_User_Talent
	{
		public int StartPage = -1;
		public int TalentCount = 0;
		public List<Struct_Game_User_Talent_Node> TalentNode = new List<Struct_Game_User_Talent_Node>();
	}
	[System.Serializable]
	public class Struct_Game_User_Talent_Node
	{
		public string NodeID;
		public int CurrentCount;
		public Struct_Game_User_Talent_Node()
		{

		}
		public Struct_Game_User_Talent_Node(string ID,int Count)
		{
			NodeID = ID;
			CurrentCount = Count;
		}
	}
	public class Server_Data_Talent : MonoBehaviour
	{
		protected Server_Struct_Data_Talent mTalentData = new Server_Struct_Data_Talent();

		Server_Data_Talent()
		{
			//if(m_pInterface == null)
			m_pInterface = this;
			InitData ();
			//object tmpData = null;
			//Server_Data_IO.Singleton().LoadData(Application.persistentDataPath + "/Config_Talent.xml",ref tmpData,typeof(Server_Struct_Data_Talent));
			//Init mTalentData;
		}

		void Awake()
		{

		}

		void InitData()
		{
			Server_Struct_Data_Talent_Page newPage = new Server_Struct_Data_Talent_Page ();
			newPage.PageName = "Talent";
			newPage.PageID = "0";

			Server_Struct_Data_Talent_Node tmp_100 = new Server_Struct_Data_Talent_Node ("100",true,0,5,3);

			Server_Struct_Data_Talent_Node tmp_90 = new Server_Struct_Data_Talent_Node ("90",true,0,5,3);
			Server_Struct_Data_Talent_Node tmp_91 = new Server_Struct_Data_Talent_Node ("91",true,0,5,3);

			Server_Struct_Data_Talent_Node tmp_80 = new Server_Struct_Data_Talent_Node ("80",true,0,5,3);
			Server_Struct_Data_Talent_Node tmp_81 = new Server_Struct_Data_Talent_Node ("81",true,0,5,3);

			Server_Struct_Data_Talent_Node tmp_70 = new Server_Struct_Data_Talent_Node ("70",true,0,5,3);
			Server_Struct_Data_Talent_Node tmp_71 = new Server_Struct_Data_Talent_Node ("71",true,0,5,3);

			Server_Struct_Data_Talent_Node tmp_60 = new Server_Struct_Data_Talent_Node ("60",true,0,10,5);
			Server_Struct_Data_Talent_Node tmp_61 = new Server_Struct_Data_Talent_Node ("61",true,0,10,5);
			Server_Struct_Data_Talent_Node tmp_62 = new Server_Struct_Data_Talent_Node ("62",true,0,10,5);

			Server_Struct_Data_Talent_Node tmp_50 = new Server_Struct_Data_Talent_Node ("50",true,0,10,5);
			Server_Struct_Data_Talent_Node tmp_51 = new Server_Struct_Data_Talent_Node ("51",true,0,10,5);

			tmp_100.Child.Add (tmp_90);
			tmp_100.Child.Add (tmp_91);

			tmp_90.Child.Add (tmp_80);
			tmp_91.Child.Add (tmp_81);

			tmp_80.Child.Add (tmp_70);
			tmp_81.Child.Add (tmp_71);

			tmp_70.Child.Add (tmp_60);
			tmp_70.Child.Add (tmp_61);

			tmp_71.Child.Add (tmp_61);
			tmp_71.Child.Add (tmp_62);

			tmp_60.Child.Add (tmp_50);
			tmp_62.Child.Add (tmp_51);

			newPage.Top = tmp_100;

			mTalentData.Talent.Add (newPage);
		}

		static Server_Data_Talent m_pInterface;
		public static Server_Data_Talent Singleton()
		{

			return m_pInterface;
		}

		public Server_Struct_Data_Talent GetCopyData()
		{
			return mTalentData.Copy();
		}

		
		public void SerializeTalent(ref Server_Struct_Data_Talent CurrentData,Struct_Game_User_Talent TargetData)
		{
			CurrentData.StartPage = TargetData.StartPage;
			CurrentData.TalentCount = TargetData.TalentCount;
			
			for(int i=0;i<CurrentData.Talent.Count;++i)
			{
				InsertData(CurrentData.Talent[i].Top,TargetData);
			}
		}
		
		public void DeSerializeTalent(Server_Struct_Data_Talent Target,ref Struct_Game_User_Talent CurrentData)
		{
			CurrentData.StartPage = Target.StartPage;
			CurrentData.TalentCount = Target.TalentCount;
			
			for(int i=0;i<mTalentData.Talent.Count;++i)
			{
				InsertData(ref CurrentData,Target.Talent[i].Top);
			}
		}

		public Struct_Game_User_Talent GetOriginData()
		{
			Struct_Game_User_Talent  tmpData = new Struct_Game_User_Talent();
			tmpData.StartPage = mTalentData.StartPage;
			tmpData.TalentCount = mTalentData.TalentCount;

			for(int i=0;i<mTalentData.Talent.Count;++i)
			{
				InsertData(ref tmpData,mTalentData.Talent[i].Top);
			}

			return tmpData;
		}

		private void InsertData(ref Struct_Game_User_Talent TargetData,Server_Struct_Data_Talent_Node CurrentNode)
		{
			for(int i=0;i<CurrentNode.Child.Count;++i)
			{
				InsertData(ref TargetData,CurrentNode.Child[i]);
			}

			if(!IsContains(TargetData,CurrentNode.NodeID))
				TargetData.TalentNode.Add(new Struct_Game_User_Talent_Node(CurrentNode.NodeID,CurrentNode.CurrentCount));
		}

		//int Count = 0;
		private void InsertData(Server_Struct_Data_Talent_Node CurrentData,Struct_Game_User_Talent TargetData)
		{
			//Count ++;
			//if(Count>100)
			//{
			//	Debug.LogError("Error");
			//	return;
			//}

			for(int i=0;i<CurrentData.Child.Count;++i)
			{
				InsertData(CurrentData.Child[i],TargetData);
			}

			CurrentData.CurrentCount = GetTalentNode(TargetData,CurrentData.NodeID).CurrentCount;
			CurrentData.Avaliable = false;
		}

		private bool IsContains(Struct_Game_User_Talent TargetData,string NodeID)
		{
			for(int i=0;i<TargetData.TalentNode.Count;++i)
			{
				if(TargetData.TalentNode[i].NodeID == NodeID)
					return true;
			}
			return false;
		}

		private Struct_Game_User_Talent_Node GetTalentNode(Struct_Game_User_Talent TargetData,string NodeID)
		{
			for(int i=0;i<TargetData.TalentNode.Count;++i)
			{
				if(TargetData.TalentNode[i].NodeID == NodeID)
					return TargetData.TalentNode[i];
			}
			return null;
		}
	}
}

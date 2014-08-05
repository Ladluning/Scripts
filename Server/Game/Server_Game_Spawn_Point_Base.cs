using UnityEngine;
using System.Collections;
namespace Server
{
	public class Server_Game_Spawn_Point_Base : MonoBehaviour {

		public Vector3 SpawnPos;
		protected Transform mCurrentTransform;
		protected Server_Game_Scene_Manager mFather;

		protected virtual void Awake()
		{
			mCurrentTransform = transform;
		}

		public virtual void Init()
		{
			mFather = GameTools.FindComponentInHierarchy<Server_Game_Scene_Manager>(transform);
		}

		public virtual Int2 GetEmptyRandomPos()
		{
			return new Int2(SpawnPos);
		}

		public virtual Int2 GetEmptyRandomPos(Int2 CurrentPos)
		{
			return new Int2(SpawnPos);
		}
	}
}

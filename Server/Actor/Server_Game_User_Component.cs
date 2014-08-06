using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_User_Component : Controller {

		[HideInInspector]
		public Server_Game_User mUser;
		[HideInInspector]
		public Transform mCurrentTransform;

		protected bool mMarkAsChanged = true;

		public void SetChanged()
		{
			mMarkAsChanged = true;
		}
		
		public bool GetIsChanged()
		{
			return mMarkAsChanged;
		}

		protected virtual void FixedUpdate()
		{
			UpdateChanged();
		}

		protected virtual void UpdateChanged()
		{
			if (!mMarkAsChanged)
			{
				return;
			}
			
			mMarkAsChanged = false;
		}

		public virtual void Init(Server_Game_User Father)
		{
			mCurrentTransform = transform;
			mUser = Father;
		}

		public virtual void UpdateData()
		{

		}

		public virtual void SerializeVisiblePosData(ref Dictionary<string, object> Father)
		{
			return;
		}
	}
}

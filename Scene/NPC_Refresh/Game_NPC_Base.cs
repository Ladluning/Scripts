using UnityEngine;
using System.Collections;

public class Game_NPC_Base : MonoBehaviour {

	public string mNPCName;
	public string mNPCID;
	public int mEnableID = -1;//Default Enable

	public virtual bool GetIsEnable(int CurrentStoryID)
	{
		return true;
	}
}

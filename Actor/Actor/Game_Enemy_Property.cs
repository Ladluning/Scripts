using UnityEngine;
using System.Collections;

[System.Serializable]
public class Struct_Drop_Info
{
	public float DropProbability;
	public string DropType;
	public string DropQuality;
	public string DropCount;
}
public class Game_Enemy_Property : Game_Actor_Property_Base {

	public int mWatchDistance;
	public int mMaxWatchDistance;
	public int mAwayDistance;
	public Struct_Drop_Info[] mDropInfo;
}

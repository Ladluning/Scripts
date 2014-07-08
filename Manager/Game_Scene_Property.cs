using UnityEngine;
using System.Collections;

public class Game_Scene_Property : MonoBehaviour {

	public bool IsCloseOther = true;
	public GameObject FatherObject;
	public float DelayLoadingTime = 0.1f;

	public void ShowCurrent(GameObject Target)
	{
		FatherObject = Target;
	}
}

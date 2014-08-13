using UnityEngine;
using System.Collections;

public class UILanguage : Controller 
{
	void Awake()
	{
		Localization.LoadCSV(Resources.Load("Config/Language",typeof(TextAsset)) as TextAsset);
		Localization.language = "English";
	}

}

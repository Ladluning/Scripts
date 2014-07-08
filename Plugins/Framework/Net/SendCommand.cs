using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SendCommand {

	public static Dictionary<string,object> NewCommand(uint CommandID)
	{
		Dictionary<string,object> tmpDir = new Dictionary<string, object>();
		Dictionary<string,object> tmpDir_B = new Dictionary<string, object>();
		tmpDir.Add("sid",GlobalValueManager.SID);
		tmpDir.Add("event",CommandID);
		tmpDir.Add("results",tmpDir_B);
		tmpDir_B.Add("time",NetUtilHelper.GetLongFromTime());
		return tmpDir ;
	}
}

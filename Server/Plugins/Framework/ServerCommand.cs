using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class ServerCommand
    {

        public static Dictionary<string, object> NewCommand(uint CommandID)
        {
            Dictionary<string, object> tmpDir = new Dictionary<string, object>();
            Dictionary<string, object> tmpDir_B = new Dictionary<string, object>();
            tmpDir.Add("event", CommandID);
            tmpDir.Add("success", 1);
            tmpDir.Add("time", NetUtilHelper.GetLongFromTime());
            tmpDir.Add("results", tmpDir_B);
            return tmpDir;
        }
    }
}

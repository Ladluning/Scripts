using UnityEngine;
using System.Collections;
using System;
namespace Server
{
    public class Server_Data_IO
    {
        Server_Data_Save_XML mIO_XML;

        public Server_Data_IO()
        {
            mIO_XML = new Server_Data_Save_XML();
        }

        public void SaveData(string Path, object Target)
        {
            mIO_XML.SaveXML(Path, Target);
        }

        public void LoadData(string Path, ref object Target, Type TargetType)
        {
            mIO_XML.LoadXML(Path, ref Target, TargetType);
        }

    }
}

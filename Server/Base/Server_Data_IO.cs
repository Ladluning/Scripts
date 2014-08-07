using UnityEngine;
using System.Collections;
using System;
namespace Server
{
    public class Server_Data_IO
    {
		Server_Data_Save_XML mIO_XML;
		
		Server_Data_IO()
        {
			mIO_XML = new Server_Data_Save_XML();
		}

		static Server_Data_IO m_pInterface;
		public static Server_Data_IO Singleton()
		{
			if(m_pInterface == null)
				m_pInterface = new Server_Data_IO();
			return m_pInterface;
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

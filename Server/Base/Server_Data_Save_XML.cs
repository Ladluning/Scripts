using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.IO;
using System;

namespace Server
{
    public class Server_Data_Save_XML
    {

        public void LoadXML(string vPath, ref object vData, Type vObjType)
        {
            Debug.Log(vPath+" "+File.Exists(vPath));
            if (!File.Exists(vPath))
                return;

            vData = ReadData(vPath, vObjType);
        }

        public void SaveXML(string vPath, object vData)
        {
            Debug.Log("Save Data");
            SaveData(vPath, vData);
        }

        private object ReadData(string vPath, Type classType)
        {
            StreamReader r = File.OpenText(vPath);
            string _data = r.ReadToEnd();
            object o = DeserializeObject(_data, classType);
            r.Close();
            return o;
        }

        private bool SaveData(string vPath, object pObject)
        {
            try
            {
                StreamWriter writer;
                FileInfo fInfo = new FileInfo(vPath);
                writer = fInfo.CreateText();
                String _data = SerializeObject(pObject);
                writer.Write(_data);
                writer.Close();
                //					Debug.LogWarning(string.Format("Success to Save File: [{0}]", vPath));
            }
            catch (Exception e)
            {
                //					Debug.Log(string.Format("[Exception][OperateClassXML SaveData]: [{0}]", e.Message));
                throw e;
            }

            return true;
        }

        private string SerializeObject(object pObject)
        {
            string XmlizedString = string.Empty;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(pObject.GetType());
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, pObject);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString;
        }

        public object DeserializeObject(string pXmlizedString, Type classType)
        {
            XmlSerializer xs = new XmlSerializer(classType);
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            return xs.Deserialize(memoryStream);
        }

        //将Btye数组转为字串//
        private string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        //将字串转为Byte数组//
        private byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
    }
}

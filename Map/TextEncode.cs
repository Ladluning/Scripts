#define UT8EnDecode
using System;
using System.Text;
using UnityEngine;

namespace TextEncode
{
	public class Convert
	{
		public static string GBK_NAME = "GB_2312_80";
		 //GB2312转换成unicode编码 
        static public string GB2Unicode(string str) 
        { 
            string Hexs = ""; 
            string HH; 
            Encoding GB = Encoding.GetEncoding(GBK_NAME); 
            //Encoding unicode = Encoding.Unicode; 
            byte[] GBBytes = GB.GetBytes(str); 
            for (int i = 0; i < GBBytes.Length; i++) 
            { 
                HH = "%" + GBBytes[i].ToString("X"); 
                Hexs += HH; 
            } 
            return Hexs; 
        }
		//GB2312转换成unicode编码 
        static public string GB2UnicodeFromBin(byte[] GBBytes) 
        { 
            string Hexs = ""; 
            string HH; 
            //Encoding GB = Encoding.GetEncoding("GB2312"); 
            //Encoding unicode = Encoding.Unicode; 
            for (int i = 0; i < GBBytes.Length; i++) 
            { 
                HH = "%" + GBBytes[i].ToString("X"); 
                Hexs += HH; 
            } 
            return Hexs; 
        } 
        //unicode编码转换成GB2312汉字 
        static public string UtoGB(string str) 
        { 
            string[] ss = str.Split('%'); 
            byte[] bs = new Byte[ss.Length - 1]; 
            for (int i = 1; i < ss.Length; i++) 
            { 
                bs[i - 1] = System.Convert.ToByte(Convert2Hex(ss[i]));   //ss[0]为空串   
            } 
            char[] chrs = System.Text.Encoding.GetEncoding(GBK_NAME).GetChars(bs); 
            string s = ""; 
            for (int i = 0; i < chrs.Length; i++) 
            { 
                s += chrs[i].ToString(); 
            } 
            return s; 
        } 
        static private string Convert2Hex(string pstr) 
        { 
            if (pstr.Length == 2) 
            { 
                pstr = pstr.ToUpper(); 
                string hexstr = "0123456789ABCDEF"; 
                int cint = hexstr.IndexOf(pstr.Substring(0, 1)) * 16 + hexstr.IndexOf(pstr.Substring(1, 1)); 
                return cint.ToString(); 
            } 
            else 
            { 
                return ""; 
            } 
        }
		
		
		static public string GBK2UnicodeFromBin(byte[] data)
		{
			Encoding gbkencoding = Encoding.GetEncoding(936);
			byte[] buf2 = Encoding.Convert(gbkencoding,Encoding.Unicode, data);
			string atext =Encoding.Unicode.GetString(buf2);
			return atext;
		}
		
		static public byte[] Unicode2GBKBin(string gbk)
		{
			Encoding gbkencoding = Encoding.GetEncoding(936);
			byte[] gbkBytes = gbkencoding.GetBytes(gbk);
			return gbkBytes;
		}
		
		static public string ServerBin2UTFString(byte[] data)
		{
#if UT8EnDecode
			try {
				return Encoding.UTF8.GetString(data);
			} catch (Exception ex) {
				Debug.Log(ex);
				return "";
			}

#else
			Encoding gbkencoding = Encoding.GetEncoding(GBK_NAME);
			string aString = gbkencoding.GetString(data);
			return aString;
#endif
		}
		
		static public byte[] UTFString2ServerBin(string str, int byteLen = 0)
		{
			byte[] aBytes ;
#if UT8EnDecode
			aBytes = Encoding.UTF8.GetBytes(str);
#else
			Encoding gbkencoding = Encoding.GetEncoding(GBK_NAME);
			aBytes = gbkencoding.GetBytes(str);
#endif
			if(byteLen == 0)
				return aBytes;
			
			byte[] returnBytes = new byte[byteLen];
			System.Array.Copy(aBytes, returnBytes, Math.Min(byteLen, aBytes.Length));
			return returnBytes;

		}
		
		static public int BinLengthFromUTFString(string str)
		{
			return UTFString2ServerBin(str).Length;
		}
	}
}


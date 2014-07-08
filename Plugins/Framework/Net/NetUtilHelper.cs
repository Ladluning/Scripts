using UnityEngine;
using System.Collections;
using System;

public class NetUtilHelper 
{
    private static long TimeLeft = 621355968000000000;

    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }


    public static long GetLongFromTime()
    {
        DateTime dt1 = DateTime.Now.ToUniversalTime();
        long Sticks = (dt1.Ticks - TimeLeft) / 10000000;
        return Sticks;
    }

    public static string GetTimeFromLong(long time)
    {
        long Sticks = time * 10000000 + TimeLeft;
        DateTime dt = new DateTime(Sticks, DateTimeKind.Utc);
        string result = dt.ToLocalTime().ToString("M月D天");

        return result;
    }
	public static DateTime GetDateTimeFromLong(long time)
	{
		long Sticks = time * 10000000 + TimeLeft;
		return new DateTime(Sticks, DateTimeKind.Utc);
	}
	public static string GetNormalTimeFromLong(long time)
	{
		long Sticks = time * 10000000;
		DateTime dt = new DateTime(Sticks, DateTimeKind.Utc);
		return dt.TimeOfDay.ToString();
	}
	public static long GetLongFromDateTime(DateTime time)
	{
		return time.Ticks/10000000;
	}
    public static string GetDeviceID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }


}

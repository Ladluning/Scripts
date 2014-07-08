using UnityEngine;
using System.Collections;

public static class CommonFunction {
	
	//比对两个字串哪个最新(>0-vStr1最新 <0-vStr2最新 =0相等)//
	public static int CheckStringIsNew (string vStr1, string vStr2)
	{
		return string.Compare(vStr1, vStr2);
	}
	
	//获取建筑最大等级//
	public static int GetSingleBuildMaxLV (int vBuildType)
	{
		return 1;
	}
	
}

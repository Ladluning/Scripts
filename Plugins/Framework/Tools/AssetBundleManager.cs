using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class AssetBundleManager : MonoBehaviour {
	
	private static AssetBundleManager pInterface;
	public static AssetBundleManager Singleton ()
	{
		return pInterface;
	}
	
	void Awake ()
	{
		pInterface = this;
	}
	
	public object GetSingleObject (string vDataPath, string vObjName)
	{
		object tmpResult = null;
		//判断资源是否存在//
		if (!File.Exists(vDataPath))
			return tmpResult;
		//获取资源//
		AssetBundle tmpAssetBundle = AssetBundle.CreateFromFile(vDataPath);
		//判断文件是否存在//
		if (tmpAssetBundle.Contains(vObjName))
			tmpResult = tmpAssetBundle.Load(vObjName);
		//清空缓存//
		tmpAssetBundle.Unload(false);
		//返回数据//
		return tmpResult;
	}
}































//	//程序集测试//
//	private void TestAssemblyFunction ()
//	{	
//		AssemblyOperate tmpOperate1 = new AssemblyOperate(@"D:\TestAssemblyClass.dll", @"TestAssemblyDll.China");
//		Debug.Log(tmpOperate1.InvokeSingleFunction("GetCountryName", null));
//		Debug.Log(tmpOperate1.InvokeSingleFunction("GetResult", new object[]{9, 5}));
//		
//		AssemblyOperate tmpOperate2 = new AssemblyOperate(@"D:\TestAssemblyClass.dll", @"TestAssemblyDll.America");
//		Debug.Log(tmpOperate2.InvokeSingleFunction("GetCountryName", null));
//		Debug.Log(tmpOperate2.InvokeSingleFunction("GetResult", new object[]{7, 5}));
//	}
//using UnityEngine;
//using System.Collections;
//using System.Reflection;
//using System;
////
//////		List<int> tmpIntList = new List<int>();
//////		tmpIntList.Add(1);
//////		tmpIntList.Add(2);
//////		tmpIntList.Add(3);
//////		tmpIntList.Add(4);
//////		tmpIntList.Add(5);
//////		Debug.Log(string.Format("AddResult: [{0}]", TestAssemblyClass.GetAddResult(tmpIntList)));
//////		Debug.Log(string.Format("MulResult: [{0}]", TestAssemblyClass.GetMultiplyResult(tmpIntList)));
////		
//////		Country tmpCountry_1 = (Country)Assembly.Load(@"TestAssemblyClass").CreateInstance(@"TestAssemblyDll.China");
//////		Debug.Log(string.Format("tmpCountry_1: [{0}]", tmpCountry_1.GetCountryName()));
//////		Country tmpCountry_2 = (Country)Assembly.Load("TestAssemblyClass").CreateInstance("TestAssemblyDll.America");
//////		Debug.Log(string.Format("tmpCountry_1: [{0}]", tmpCountry_2.GetCountryName()));
////		
//////		//获取程序集//
//////		Assembly tmpAssembly = Assembly.LoadFile(@"D:\TestAssemblyClass.dll");
//////		//获取类型//
//////		Type tmpType = tmpAssembly.GetType(@"TestAssemblyDll.Country");
//////		//获取方法//
//////		MethodInfo tmpMethod = tmpType.GetMethod("GetCountryName");
//////		MethodInfo tmpMethodResult = tmpType.GetMethod("GetResult");
//////		//调用方法//
//////		object tmpObject_China = tmpAssembly.CreateInstance(@"TestAssemblyDll.China");
//////		Debug.Log(tmpMethod.Invoke(tmpObject_China, null));
//////		Debug.Log(tmpMethodResult.Invoke(tmpObject_China, new object[]{9, 5}));
//////		
//////		object tmpObject_America = tmpAssembly.CreateInstance(@"TestAssemblyDll.America");
//////		Debug.Log(tmpMethod.Invoke(tmpObject_America, null));
//////		Debug.Log(tmpMethodResult.Invoke(tmpObject_America, new object[]{5, 5}));
////
//public class AssemblyOperate {
//	
//	private Assembly mAssembly;//程序集//
//	private Type mAssemblyType;//程序集类型//
//	private object mAssemblyObj;//程序集实例//
//	
//	public AssemblyOperate (string vAssemblyName, string vClassName)
//	{
//		mAssembly = Assembly.LoadFile(vAssemblyName);
//		mAssemblyType = mAssembly.GetType(vClassName);
//		mAssemblyObj = mAssembly.CreateInstance(vClassName);
//	}
//
//	public object InvokeSingleFunction (string vFunctionName, object[] vParameters)
//	{
//		if ((mAssemblyType == null) || (mAssemblyObj == null))
//			return null;
//		
//		//获取方法//
//		MethodInfo tmpMethod = mAssemblyType.GetMethod(vFunctionName);
//		if (tmpMethod == null)
//			return null;
//		
//		//调用方法//
//		return tmpMethod.Invoke(mAssemblyObj, vParameters);
//	}
//	
//}
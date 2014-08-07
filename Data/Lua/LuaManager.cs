using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
public class LuaManager : MonoBehaviour 
{
	public Dictionary<string,Lua> mLuaList = new Dictionary<string, Lua>();
	public string mLuaRootPath;

	private static LuaManager m_pInterface;
	public static LuaManager Singleton()
	{
		return m_pInterface;
	}

	LuaManager()
	{
		m_pInterface = this;
	}

	public Lua RegistFile(string Path)
	{
		if(mLuaList.ContainsKey(Path))
			return mLuaList[Path];

		Lua l = new Lua();
		l.DoFile(mLuaRootPath+Path);

		mLuaList.Add(Path,l);
		return l;
	}

	public void RegistFunction(string Path,string FunctionName,object Target,MethodInfo Function)
	{
		if(!mLuaList.ContainsKey(Path))
			return;

		if(mLuaList[Path].GetFunction(FunctionName)==null)
			mLuaList[Path].RegisterFunction(FunctionName,Target,Function);
	}

	public LuaFunction GetFunction(string Path,string FunctionName)
	{
		if(!mLuaList.ContainsKey(Path))
			return null;
		
		return mLuaList[Path].GetFunction(FunctionName);
	}
}


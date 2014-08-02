using UnityEngine;
using System.Collections;

[System.Serializable]
public class Int2
{
	public int x;
	public int y;
	public Int2()
	{
		
	}

	public Int2(Vector3 value)
	{
		x = (int)value.x;
		y = (int)value.z;
	}

	public Int2(float xValue,float yValue)
	{
		x = (int)xValue;
		y = (int)yValue;
	}

	public Int2(int xValue,int yValue)
	{
		x = xValue;
		y = yValue;
	}
	
	public Int2(Int3 xyzValue)
	{
		x = xyzValue.x;
		y = xyzValue.z;
	}
	public Int3 ToInt3()
	{
		return new Int3 (x,0,y);
	}
	
	public Vector2 ToVector2()
	{
		return new Vector2 (x,y);
	}
	
	public Vector3 ToVector3()
	{
		return new Vector3 (x,0,y);
	}
	
	public static Int2 operator+(Int2 One,Int2 Other)
	{
		return new Int2 (One.x+Other.x,One.y+Other.y);
	}
	
	public static Int2 operator-(Int2 One,Int2 Other)
	{
		return new Int2 (One.x-Other.x,One.y-Other.y);
	}
	
	public static Int2 operator*(Int2 One,int Other)
	{
		return new Int2 (One.x*Other,One.y*Other);
	}

	public static Vector2 operator*(Int2 One,Vector2 Other)
	{
		return new Vector2 (One.x*Other.x,One.y*Other.y);
	}

	public static Int2 operator/(Int2 One,int Other)
	{
		return new Int2 (One.x/Other,One.y/Other);
	}
	
	public static bool operator==(Int2 One,Int2 Other)
	{		
		if((One as object) != null && (Other as object) != null)
			return One.x==Other.x&&One.y==Other.y;
		else if((One as object) == null && (Other as object) == null)
			return true;  
		else
			return false; 
	}
	
	public static bool operator!=(Int2 One,Int2 Other)
	{
		return !(One==Other);
	}
	
}
[System.Serializable]
public class Int3
{
	public int x;
	public int y;
	public int z;
	public Int3()
	{
		
	}
	
	public Int3(int xValue,int yValue,int zValue)
	{
		x = xValue;
		y = yValue;
		z = zValue;
	}
	
	public Int3(Int2 xzValue)
	{
		x = xzValue.x;
		z = xzValue.y;
	}
	
	public Int2 ToInt2()
	{
		return new Int2 (x,z);
	}
	
	public Vector3 ToVector3()
	{
		return new Vector3 (x,y,z);
	}
	
	public static Int3 operator+(Int3 One,Int3 Other)
	{
		return new Int3 (One.x+Other.x,One.y+Other.y,One.z+Other.z);
	}
	
	public static Int3 operator-(Int3 One,Int3 Other)
	{
		return new Int3 (One.x-Other.x,One.y-Other.y,One.z-Other.z);
	}
	
	public static Int3 operator*(Int3 One,int Other)
	{
		return new Int3 (One.x*Other,One.y*Other,One.z*Other);
	}
	
	public static Int3 operator/(Int3 One,int Other)
	{
		return new Int3 (One.x/Other,One.y/Other,One.z/Other);
	}
	
	public static bool operator==(Int3 One,Int3 Other)
	{
		return One.x==Other.x&&One.y==Other.y&&One.z==Other.z;
	}
	
	public static bool operator!=(Int3 One,Int3 Other)
	{
		return One.x!=Other.x||One.y!=Other.y||One.z!=Other.z;
	}
}

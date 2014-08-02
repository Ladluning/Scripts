using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_User : Controller 
{
    private static Client_User m_pInterface;
    public static Client_User Singleton()
    {
        return m_pInterface;
    }

    void Awake()
    {
        m_pInterface = this;
        mClientTransform = gameObject.AddComponent<Client_Transform>();
		mClientPackage   = gameObject.AddComponent<Client_Package>();
    }

    private string mID;
    public string GetID()
    {
        return mID;
    }
    private Client_Transform mClientTransform;
    private Client_Property mClientProperty;
    public Client_Property GetProperty()
    {
        return mClientProperty;
    }
    private Client_Package mClientPackage;
    public Client_Package GetPackage()
    {
        return mClientPackage;
    }
}

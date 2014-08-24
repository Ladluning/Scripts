using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Server
{
    public class Server_Game_FSM_State_Base
    {

        public List<string> mStateComponent = new List<string>();

        protected Server_Game_FSM_Controller mController;
        protected Dictionary<Type, object> mStateComonentList = new Dictionary<Type, object>();
        protected List<int> mStateTranslate = new List<int>();

        public Server_Game_FSM_State_Base(Server_Game_FSM_Controller FSMController)
        {
            mController = FSMController;
        }

        public T GetController<T>()
        {
            return (T)(object)mController;
        }

        public T GetComponent<T>()
        {
            if (mStateComonentList.ContainsKey(typeof(T)))
                return (T)mStateComonentList[typeof(T)];
            return default(T);
        }
        public void AddTranslate(int Target)
        {
            if (!mStateTranslate.Contains(Target))
            {
                mStateTranslate.Add(Target);
            }
        }
        public bool GetIsCouldTranslate(int Target)
        {
            return mStateTranslate.Contains(Target);
        }
        public virtual void Init()
        {
            foreach (string tmpComponentName in mStateComponent)
            {
                Server_Game_Object_Base tmpComponent = mController.GetComponent(tmpComponentName) as Server_Game_Object_Base;
                if (tmpComponent == null)
                {
                    tmpComponent = mController.gameObject.AddComponent(tmpComponentName) as Server_Game_Object_Base;
                }

                mStateComonentList.Add(tmpComponent.GetType(), tmpComponent);
            }

            foreach (Type types in mStateComonentList.Keys)
            {
                ((Server_Game_Object_Base)mStateComonentList[types]).Init();
            }

//            Debug.Log("InitState");
        }
        public virtual void OnEnter()
        {
            foreach (Type types in mStateComonentList.Keys)
            {
                ((MonoBehaviour)mStateComonentList[types]).enabled = true;
            }
        }

        public virtual void OnLoop() { }

        public virtual void OnExit()
        {
            foreach (Type types in mStateComonentList.Keys)
            {
                ((MonoBehaviour)mStateComonentList[types]).enabled = false;
            }
        }

        protected void RegistListen(uint EventID, RegistFunction pFunction)
        {
            Facade.Singleton().RegistListen(EventID, pFunction);
        }

        protected void UnRegistListen(uint EventID, RegistFunction pFunction)
        {
            Facade.Singleton().UnRegistListen(EventID, pFunction);
        }

        protected void RegistEvent(uint EventID, RegistFunction pFunction)
        {
            Facade.Singleton().RegistEvent(EventID, pFunction);
        }

        protected void UnRegistEvent(uint EventID, RegistFunction pFunction)
        {
            Facade.Singleton().UnRegistEvent(EventID, pFunction);
        }

        protected object SendEvent(uint EventID, object pSender)
        {
            return Facade.Singleton().SendEvent(EventID, pSender);
        }

        public virtual void OnDrawGizmos()
        { 
            
        }
    }
}

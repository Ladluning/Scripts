using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_FSM_State_Interface
    {
        public List<string> mStateComponent = new List<string>();
        protected Dictionary<Type, object> mStateComonentList = new Dictionary<Type, object>();
        protected List<int> mStateTranslate = new List<int>();

        public T GetComponent<T>()
        {
            if (mStateComonentList.ContainsKey(typeof(T)))
                return (T)mStateComonentList[typeof(T)];
            return default(T);
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

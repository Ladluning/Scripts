using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_User_Base : Controller
    {
        [HideInInspector]
        public Server_User mServer;
        [HideInInspector]
        public Struct_Game_User_Info mDataInfo;

        public string ID;

        protected bool mMarkAsChanged = true;
        protected Transform mCurrentTransform;
        protected Server_Manager mManager;
        protected Server_Game_User mSelf;

        protected List<Server_Game_User> mVisiblePlayerList = new List<Server_Game_User>();
        protected List<Server_Game_Enemy> mVisibleEnemyList = new List<Server_Game_Enemy>();

        protected virtual void Awake()
        {
            mManager = Server_Manager.Singleton();
            mCurrentTransform = transform;
        }

        protected virtual void OnEnable()
        { 
        
        }

        protected virtual void OnDisable()
        { 
            
        }

        public void SetChanged()
        {
            mMarkAsChanged = true;
        }

        public bool GetIsChanged()
        {
            return mMarkAsChanged;
        }

        protected void SetSelf(Server_Game_User Target)
        {
            mSelf = Target;
        }

        public void SetServer(Server_User Target)
        {
            mServer = Target;

            StopDestroy();
        }

        protected virtual void FixedUpdate()
        {
            UpdateMessage();
            UpdateChanged();
            UpdateDestroy();
        }

        protected virtual void UpdateMessage()
        {

        }

        protected virtual void UpdateChanged()
        {
            if (!mMarkAsChanged)
            {
                return;
            }

            mMarkAsChanged = false;

            UpdateData();
            UpdateBoard();
        }

        protected void UpdateData()
        {
            mDataInfo.WorldPos = mCurrentTransform.localPosition;
			mDataInfo.WorldRotate = mCurrentTransform.localEulerAngles;
        }

        protected virtual void UpdateBoard()
        {

        }

        float mWaitDestroyTime = 30f;
        float mWaitDestroyTimer = 0f;
        bool mIsDestroy = true;
        public void StartDestroy()
        {
            mIsDestroy = true;
            mWaitDestroyTimer = 0f;
        }

        public void StopDestroy()
        {
            mIsDestroy = false;
        }

        protected void UpdateDestroy()
        {
            if (!mIsDestroy)
                return;

            mWaitDestroyTimer += Time.deltaTime;
            if (mWaitDestroyTimer > mWaitDestroyTime)
            {
                mIsDestroy = false;
                mManager.DestroyPlayer(mSelf);
            }
        }
    }
}

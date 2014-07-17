using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Object_Action : Server_Game_Object_Base
    {
        private List<Vector3> mTargetPos = new List<Vector3>();
        private RegistFunction mCallBack;
        private bool IsStartMove;
        private int mCurrentPoint;
        private float mMoveSpeed;
        void Awake()
        {

        }
        public void MoveToTarget(Vector3 TargetPos, RegistFunction CallBack,float MoveSpeed = 1)
        {
            mTargetPos.Add(TargetPos);
            mMoveSpeed = MoveSpeed;
            mCallBack = CallBack;
            IsStartMove = true;
        }

        public void MoveToTarget(List<Vector3> TargetPos, RegistFunction CallBack, float MoveSpeed = 1)
        {
            mTargetPos = TargetPos;
            mCallBack = CallBack;
            mMoveSpeed = MoveSpeed;
            SlerpPath(mTargetPos);
            IsStartMove = true;
        }

        void OnEnable()
        {
            IsStartMove = false;
            mTargetPos.Clear();
        }

        void OnDisable()
        {
            IsStartMove = false;
            mTargetPos.Clear();
        }

        void Update()
        {
            if (!IsStartMove)
                return;

            mCurrentTransform.position = Vector3.MoveTowards(mCurrentTransform.position, mTargetPos[mCurrentPoint], mMoveSpeed * Time.deltaTime);

            if ((mTargetPos[mCurrentPoint] - mCurrentTransform.position).sqrMagnitude < 0.01f)
            {
                IsStartMove = false;
                mCallBack(null);
                return;
            }

            mCurrentPoint += 1;
            //moveDirection += Physics.gravity * Time.deltaTime;
            //mCharacterController.Move(moveDirection);
        }

        void SlerpPath(List<Vector3> TargetList)
        {
            for (int i = 0; i < TargetList.Count - 2; ++i)
            {
                TargetList[i + 1] = (TargetList[i] + TargetList[i + 1] + TargetList[i + 2]) / 3;
            }
        }
    }
}
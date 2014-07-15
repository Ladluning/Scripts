using UnityEngine;
using System.Collections;
namespace Server
{
    public class Server_Game_Object_Action : Server_Game_Object_Base
    {
        private Vector3 mTargetPos;
        private RegistFunction mCallBack;
        private bool IsStartMove;

        private CharacterController mCharacterController;
        private Server_Game_Scene_Manager mSceneManager;
        void Awake()
        {
            mCharacterController = gameObject.GetComponent<CharacterController>();
        }
        public void MoveToTarget(Vector3 TargetPos, RegistFunction CallBack)
        {
            mTargetPos = TargetPos;
            mCallBack = CallBack;

            mCurrentTransform.LookAt(new Vector3(TargetPos.x, mCurrentTransform.position.y, TargetPos.z));
            IsStartMove = true;
        }

        void OnEnable()
        {
            IsStartMove = false;
        }

        void OnDisable()
        {
            IsStartMove = false;
        }

        void Update()
        {
            if (!IsStartMove)
                return;

            //moveDirection += Physics.gravity * Time.deltaTime;
            //mCharacterController.Move(moveDirection);
        }

    }
}
using Controllers;
using Levels;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMoveController : AbstractPlayerMoveController
    {
        #region PRIVATE FIELDS

        private bool _isMove;
        private bool _isHorizontalMoveLock;
        private float _mouseXStartPosition;
        private float _swipeDelta;

        #endregion

        #region PRIVATE METHODS

        private void Move()
        {
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;
        }

        private void WinDance()
        {

            playerAnimator.SetBool("Move", false);
            playerAnimator.SetTrigger("Dance");
            playerAnimator.SetInteger("RandomDance", Random.Range(0, 4));

        }

        private void GoToCenterLine()
        {
           // StartCoroutine(GoToCenterLineCO());
        }
        /*private IEnumerator GoToCenterLineCO()
        {
        *//*    while (GameManagerld.Instance.conditionGame == ConditionGame.InGame)
            {
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(firstXPos, transform.position.y, transform.position.z), 2 * Time.deltaTime);
                speedForward = speedXWay;
                for (int i = 0; i < anim.Length; i++)
                {
                    anim[i].SetFloat("MoveSpeed", speedXWay / firstSpeed);
                }
            }*//*
        }*/

        #endregion

        #region PUBLIC METHODS

        public override void HorizontalMoveControl()
        {
            if (_isHorizontalMoveLock) return;

            // MOUSE DOWN
            if (Input.GetMouseButtonDown(0)) _mouseXStartPosition = Input.mousePosition.x;

            // MOUSE ON PRESS
            if (Input.GetMouseButton(0))
            {
                if (!playerAnimator.GetBool(moveParameterName)) playerAnimator.SetBool(moveParameterName, true);

                _swipeDelta = Input.mousePosition.x - _mouseXStartPosition;
                _mouseXStartPosition = Input.mousePosition.x;
            }

            // MOUSE UP
            if (Input.GetMouseButtonUp(0)) _swipeDelta = 0;

            transform.position = HorizontalPosition(transform.position, _swipeDelta);
        }

        public override void StartRun()
        {
            _isMove = true;

            playerAnimator.SetBool(moveParameterName, true);
        }

        public override void StopRun()
        {
            _isMove = false;
            playerAnimator.SetBool(moveParameterName, false);
        }

        public override void StopHorizontalControl(bool controlIsLock = true) => _isHorizontalMoveLock = controlIsLock;

        #endregion

        #region CUSTOM EVENT METHODS

        private void OnLevelStart(Level levelData) => StartRun();

        private void OnLevelFail(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }


        private void OnLevelComplete(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        #endregion

        #region UNITY EVENT METHODS

        protected override void OnComponentStart()
        {
            base.OnComponentStart();
            LevelManager.Instance.OnLevelStart += OnLevelStart;
            LevelManager.Instance.OnLevelFail += OnLevelFail;
            LevelManager.Instance.OnLevelComplete += OnLevelComplete;
        }

        protected override void OnComponentUpdate()
        {
            base.OnComponentUpdate();

            if (!_isMove) return;
            Move();
            HorizontalMoveControl();
        }

        protected override void OnComponentDestroy()
        {
            base.OnComponentDestroy();
            LevelManager.Instance.OnLevelStart -= OnLevelStart;
            LevelManager.Instance.OnLevelFail -= OnLevelFail;
            LevelManager.Instance.OnLevelComplete -= OnLevelComplete;
        }

        #endregion
    }
}
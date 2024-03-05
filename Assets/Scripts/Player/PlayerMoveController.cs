using Controllers;
using Levels;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerMoveController : AbstractPlayerMoveController
    {
        #region PRIVATE FIELDS

        private bool _isMove;
        private bool _isHorizontalMoveLock;
        private float _mouseXStartPosition;
        private float _swipeDelta;
        private float _firstXPos;
        private const string MOVE_PARAM = "Move";
        private const string DANCE_PARAM = "Dance";
        private const string SPIN_PARAM = "Spine";
        private const string RANDOM_DANCE_PARAM = "RandomDance";



        #endregion

        #region PRIVATE METHODS

        private void Move()
        {
            transform.position += transform.forward * (Time.deltaTime * forwardSpeed);
        }

        private void WinDance()
        {

            playerAnimator.SetBool(MOVE_PARAM, false);
            playerAnimator.SetTrigger(DANCE_PARAM);
            playerAnimator.SetInteger(RANDOM_DANCE_PARAM, Random.Range(0, 4));

        }

        private void OnUpgrade()
        {
            playerAnimator.SetTrigger(SPIN_PARAM);
        }

        private void GoToCenterLine()
        {
            StartCoroutine(GoToCenterLineCO());
        }
        private IEnumerator GoToCenterLineCO()
        {          
            yield return new WaitForEndOfFrame();
            transform.DOMoveX(_firstXPos, 2);
            forwardSpeed *= 2;
            _isHorizontalMoveLock = true;
        }

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
                if (!playerAnimator.GetBool(MOVE_PARAM)) playerAnimator.SetBool(MOVE_PARAM, true);

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

            playerAnimator.SetBool(MOVE_PARAM, true);
            _firstXPos = transform.position.x;
        }

        public override void StopRun()
        {
            _isMove = false;
            playerAnimator.SetBool(MOVE_PARAM, false);
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
            GameManager.Instance.OnUpgrade += OnUpgrade;
            GameManager.Instance.OnReachToFInishLine += GoToCenterLine;
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
            GameManager.Instance.OnUpgrade -= OnUpgrade;
            GameManager.Instance.OnReachToFInishLine -= GoToCenterLine;
        }

        #endregion
    }
}
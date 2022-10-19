using Controllers;
using Levels;
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
            transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
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
        }

        public override void StopRun()
        {
            _isMove = false;
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

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            forwardSpeed *= 3;
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
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelComplete += OnLevelComplete;
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
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelComplete -= OnLevelComplete;
        }

        #endregion
    }
}
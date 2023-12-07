using Controllers;
using Levels;
using UnityEngine;

namespace Player
{
    public class PlayerMoveController : MonoBehaviour, IActor
    {
        #region PRIVATE FIELDS

        private bool _isMove;
        private bool _isHorizontalMoveLock;
        private float _mouseXStartPosition;
        private float _swipeDelta;
        private SOPlayerParameters _playerParameters;
        private float forwardSpeed;
        private float horizontalSpeed;
        private float maximumHorizontalPosition;

        #endregion

        #region Interface METHODS

        public void HorizontalMoveControl()
        {
            if (_isHorizontalMoveLock) return;

            if (Input.GetMouseButtonDown(0))
                _mouseXStartPosition = Input.mousePosition.x;

            if (Input.GetMouseButton(0))
            {
                _swipeDelta = Input.mousePosition.x - _mouseXStartPosition;
                _mouseXStartPosition = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
                _swipeDelta = 0;

            transform.position = HorizontalPosition(transform.position, _swipeDelta);
        }

        private Vector3 HorizontalPosition(Vector3 targetPosition, float swipeDelta)
        {
            var xDirection = Time.deltaTime * swipeDelta * horizontalSpeed;
            var position = targetPosition;
            var xPos = Mathf.Clamp(
                position.x + xDirection,
                maximumHorizontalPosition * -1,
                maximumHorizontalPosition);
            position = new Vector3(xPos, position.y, position.z);
            return position;
        }

        public void Movement()
        {
            transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
        }

        public void StartRun()
        {
            _isMove = true;
        }
        public void StopRun()
        {
            _isMove = false;
        }

        public void StopHorizontalControl(bool controlIsLock = true)
        {
            _isHorizontalMoveLock = controlIsLock;
        }


        #endregion

        #region PRIVATE METHODS

        private void Initialized()
        {
            _playerParameters = Instantiate(Resources.Load("PlayerSetting")) as SOPlayerParameters;
            SetParameters(_playerParameters);
        }

        private void SetParameters(SOPlayerParameters parameter)
        {
            forwardSpeed = parameter.forwardSpeed;
            horizontalSpeed = parameter.horizontalSpeed;
            maximumHorizontalPosition = parameter.maximumHorizontalPosition;
        }

        #endregion

        #region Action METHODS           

        private void OnLevelStart(Level levelData)
        {
            StartRun();
        }

        private void OnLevelFail(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        private void OnLevelStageComplete(Level levelData)
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

        private void Awake()
        {
            Initialized();
        }

        private void Update()
        {
            if (!_isMove) return;
            Movement();
            HorizontalMoveControl();
        }

        private void OnEnable()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;

        }

        #endregion

    }
}
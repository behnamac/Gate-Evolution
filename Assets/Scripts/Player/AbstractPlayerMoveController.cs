using UnityEngine;

namespace Player
{
    public abstract class AbstractPlayerMoveController : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [Header("MOVE")]
        public float forwardSpeed;

        public float horizontalSpeed;
        public float maximumHorizontalPosition;
        
        #endregion

        #region ABSTRACT METHODS

        public abstract void HorizontalMoveControl();
        public abstract void StartRun();
        public abstract void StopRun();
        public abstract void StopHorizontalControl(bool controlIsLock = true);

        #endregion

        #region PRIVATE METHODS
        #endregion
        
        #region PROTECTED VIRTUAL METHODS

        protected Vector3 HorizontalPosition(Vector3 targetPosition, float swipeDelta)
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

        protected virtual void OnComponentAwake()
        {
        }

        protected virtual void OnComponentStart()
        {
        }

        protected virtual void OnComponentUpdate()
        {
        }

        protected virtual void OnComponentDestroy()
        {
        }

        #endregion

        #region UNITY EVENTS

        private void Awake() => OnComponentAwake();
        private void Start() => OnComponentStart();
        private void Update() => OnComponentUpdate();
        private void OnDestroy() => OnComponentDestroy();

        #endregion
    }
}
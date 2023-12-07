using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerSetting", menuName = "Create Actor/player")]
    public class SOPlayerParameters : ScriptableObject
    {
        #region SERIALIZE FIELDS

        [Header("MOVE")]
        public float forwardSpeed;
        public float horizontalSpeed;
        public float maximumHorizontalPosition;

        #endregion
    }
}
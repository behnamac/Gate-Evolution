using UnityEngine;
using Collectable;
using Controllers;

namespace Player
{
    public class PlayerCollisionControl : MonoBehaviour
    {
        #region PRIVATE FIELDS
        #endregion

        #region UNITY EVENT METHODS

        // TRIGGER EVENTS
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Triggable item))
                item.PerformCollect();
        }


        #endregion       

    }
}
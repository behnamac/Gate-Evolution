using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionControl : MonoBehaviour
    {

        // TRIGGER EVENTS
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITriggerable component))
            {
                component.TriggerAction();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ITriggerable component))
            {
                component.TriggerAction();
            }
        }
    }
}
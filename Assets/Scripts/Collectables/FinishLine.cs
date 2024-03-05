using UnityEngine;

namespace Collectables
{
    public class FinishLine : MonoBehaviour, ITriggerable
    {
        public void TriggerAction()
        {
            GameManager.Instance.OnReachToFInishLine?.Invoke();
        }    
    }
}

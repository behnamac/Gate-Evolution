using UnityEngine;

namespace Collectables
{
    public class Collectable : MonoBehaviour, ITriggerable
    {
        [SerializeField] private float scoreToGain=0.2f;


        public void TriggerAction()
        {
            GameManager.Instance.OnCollect?.Invoke(scoreToGain);
            Destroy(gameObject);
        }
    }
}

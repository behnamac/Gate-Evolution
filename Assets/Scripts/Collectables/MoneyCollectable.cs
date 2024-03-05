using UnityEngine;

namespace Collectables
{
    public class MoneyCollectable :MonoBehaviour, ITriggerable
    {
        [SerializeField] private int moneyToGain;
        public void TriggerAction()
        {
            MoneyController.Instance.SetCoin(moneyToGain);
            Destroy(gameObject);
        }
    }
}
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerQuestController : MonoBehaviour
    {
        [SerializeField] private ClothHolder[] clothHolders;
        [SerializeField] private Image progressBar;

        private float _questValue;
        private int _indexCloth;

        public float AllQuestValue { get; set; }
        public float AllScore { get; private set; }

        private void Awake()
        {
            for (int i = 0; i < clothHolders.Length; i++)
            {
                AllScore += clothHolders[i].targetValue;
            }
        }

        public void AddQuest(float value)
        {
            _questValue += value;
            AllQuestValue += value;
            AllQuestValue = Mathf.Clamp(AllQuestValue, -1, AllScore);
            _questValue = Mathf.Clamp(_questValue, -1, clothHolders.Last().targetValue + 1);
            if (_indexCloth + 1 < clothHolders.Length && _questValue > clothHolders[_indexCloth + 1].targetValue)
            {
                _questValue = 0;
                ChangeCloth(_indexCloth + 1);
            }

            if (_indexCloth > 0 && _questValue < 0)
            {
                ChangeCloth(_indexCloth - 1);
                _questValue = clothHolders[_indexCloth + 1].targetValue;
            }

            UpdateProgressBar();
        }

        private void ChangeCloth(int index)
        {
            for (int i = 0; i < clothHolders.Length; i++)
            {
                clothHolders[i].cloth.SetActive(false);
            }
            
            clothHolders[index].cloth.SetActive(true);
            _indexCloth = index;
        }

        private void UpdateProgressBar()
        {
            float current = _questValue;
            float target;
            if (_indexCloth + 1 < clothHolders.Length)
            {
                target = clothHolders[_indexCloth + 1].targetValue;
            }
            else
            {
                target = clothHolders.Last().targetValue;
            }

            float value = current / target;
            progressBar.fillAmount = value;
        }

        [System.Serializable]
        private class ClothHolder
        {
            public GameObject cloth;
            public float targetValue;
        }
    }
}

using UnityEngine;
using Collectable;
using Controllers;

namespace Player
{
    public class PlayerCollisionControl : MonoBehaviour
    {
        #region PRIVATE FIELDS
        
        private float _xQuesValue;
        private float _xNumber;
        
        #endregion

        #region UNITY EVENT METHODS

        // TRIGGER EVENTS
        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other.tag, other);
        }      
       

        #endregion

        #region Private METHODS

        private void TriggerEnter(string objTag, Collider obj)
        {
            switch (objTag)
            {
                case "Collectable":
                    var collectable = obj.GetComponent<CollectableController>();
                    var collectValue = collectable.collectValue;
                    if (collectable.canDestroy)
                    {
                        Destroy(obj.gameObject);
                    }
                    else
                    {
                        obj.enabled = false;
                    }

                    GetComponent<PlayerQuestController>().AddQuest(collectValue);
                    break;
                
                case "FinishLine":
                    _xQuesValue = GetComponent<PlayerQuestController>().AllScore / 10;
                    LevelManager.Instance.LevelStageComplete();
                    obj.enabled = false;
                    break;
                
                case "X":
                    _xNumber++;
                    GetComponent<PlayerQuestController>().AllQuestValue -= _xQuesValue;
                    if (_xNumber >= 10 || GetComponent<PlayerQuestController>().AllQuestValue <= 0)
                    {
                        LevelManager.Instance.LevelComplete();
                    }
                    obj.enabled = false;
                    break;
            }
        }
        #endregion

    }
}
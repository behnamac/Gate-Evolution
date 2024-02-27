using UnityEngine;

public class Collectable : MonoBehaviour, ITriggerable
{
    [SerializeField] private float scoreToGain;


    public void TriggerAction()
    {
        ScoreController.Instance.SetScore(scoreToGain);
    }
}

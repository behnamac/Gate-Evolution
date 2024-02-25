using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : ITriggerable
{
    [SerializeField] private float scoreValue;


    public void TriggerAction()
    {
        GameManager.Instance.OnCollect?.Invoke(scoreValue);
    }
}

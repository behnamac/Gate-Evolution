using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, ITriggerable
{
    public void TriggerAction()
    {
        GameManager.Instance.OnReachToFInishLine?.Invoke();
    }    
}

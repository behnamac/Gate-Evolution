using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityAction OnCollect;

    private void Awake()
    {
        instance = this;
    }

    public void CollectEffect()
    {
        OnCollect?.Invoke();
    }

}

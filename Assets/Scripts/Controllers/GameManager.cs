using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action<float> OnCollect;
    public Action OnUpgrade;
    public Action OnReachToFInishLine;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

    }
   

}
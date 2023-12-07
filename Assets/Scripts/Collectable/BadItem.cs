using UnityEngine;

public class BadItem : MonoBehaviour, ICollecable
{
    public void Collect()
    {
        Debug.Log("You get bad one!");
    }
}

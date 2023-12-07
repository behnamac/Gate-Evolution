using UnityEngine;

public class GoodItem : ICollecable
{
    public void Collect()
    {
        Debug.Log("You get good one!");
    }
}

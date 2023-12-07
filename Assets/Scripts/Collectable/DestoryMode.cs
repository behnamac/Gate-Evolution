using UnityEngine;

public class DestoryMode:MonoBehaviour, IDestroy
{
    public void ReactionMode(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    
      
}

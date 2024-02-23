using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float destroyTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyed", destroyTime);
    }
    void Destroyed() 
    {
        Destroy(gameObject);
    }
}

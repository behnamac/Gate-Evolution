using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchPeople : MonoBehaviour
{
    [SerializeField] string animStateName;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.Play(animStateName);
    }
}

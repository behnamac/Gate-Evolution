using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateCollectable : MonoBehaviour
{

    [SerializeField] float rotationDuration = 1f;
    private Vector3 rotationAmount = new Vector3(0, 360, 0);
    private LoopType loopType = LoopType.Restart;


    private void Start()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        // Rotate the object infinitely
        transform.DORotate(rotationAmount, rotationDuration, RotateMode.FastBeyond360)
                 .SetLoops(-1, loopType).SetEase(Ease.Linear);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMoney : MonoBehaviour
{
    Vector3 currentRotation;
    [SerializeField]
    Vector3 angleToRotate;
    [SerializeField]
    float speed;


    // Start is called before the first frame update
    void Start()
    {
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f,currentRotation.z%360) ;    
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationY=Quaternion.AngleAxis(angleToRotate.y*speed*Time.deltaTime,new Vector3(0,1f,0f));
        Quaternion rotationX = Quaternion.AngleAxis(angleToRotate.x *speed* Time.deltaTime, new Vector3(1f, 0f, 0f));
        Quaternion rotationZ = Quaternion.AngleAxis(angleToRotate.z *speed*Time.deltaTime, new Vector3(0, 0f, 1f));
        this.transform.rotation = rotationY * rotationX * rotationZ * this.transform.rotation;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHolder : MonoBehaviour
{
    [SerializeField] GateController[] childGates;
    [SerializeField] Material upper;
    [SerializeField] Material lower;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetMaterial", 1);
    }

    void SetMaterial() 
    {
        for (int i = 0; i < childGates.Length; i++)
        {
            for (int j = 0; j < childGates.Length; j++)
            {
                if (i != j)
                {
                    if (childGates[i].dataGate.clothType > childGates[j].dataGate.clothType)
                    {
                        childGates[i].SetMaterials(upper, GateType.Good);
                    }
                    if (childGates[i].dataGate.clothType < childGates[j].dataGate.clothType)
                    {
                        childGates[i].SetMaterials(lower, GateType.Bad);
                    }
                }
            }
        }
    }
}

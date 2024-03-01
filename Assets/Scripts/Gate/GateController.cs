using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace GateHolder
{
    public enum GateType { Good, Bad }
    public class GateController : MonoBehaviour
    {
        [SerializeField] private GateType typ = GateType.Good;

        [SerializeField] private GameObject[] gates;



        private void Awake()
        {
            Initialize();
        }


        private void Initialize()
        {
            var random = DoRandom.SetRandom(0, 2);
            if (random == 0)
            {
                gates[0].GetComponent<Gate>().SetGateEnum(GateType.Bad);
                gates[1].GetComponent<Gate>().SetGateEnum(GateType.Good);
            }
            else
            {
                gates[0].GetComponent<Gate>().SetGateEnum(GateType.Good);
                gates[1].GetComponent<Gate>().SetGateEnum(GateType.Bad);
            }

        }
    }
}

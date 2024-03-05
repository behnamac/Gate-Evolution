using System.Collections.Generic;
using Gate.Cloth;
using Tools;
using UnityEngine;

namespace Gate
{
    public enum GateType { Good, Bad }
    public class GateController : MonoBehaviour
    {
        [SerializeField] private GateType _typ = GateType.Good;

        [SerializeField] private Gate[] gates;

        [SerializeField] private SoDataGate[] dataGate;
        private Dictionary<int, SoDataGate> _gatesDict;

        private Gate _goodGate;
        private Gate _badGate;
        private SoDataGate _goodDataGate;
        private SoDataGate _badDataGate;


        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (gates == null)
            {
                gates = GetComponentsInChildren<Gate>();
            }
            var random = DoRandom.SetRandom(0, 2);
            if (random == 0)
            {
                _goodGate = gates[0];
                _badGate = gates[1];
            }
            else
            {
                _goodGate = gates[1];
                _badGate = gates[0];
            }

            SetGoodGate();

        }

        private void SetGoodGate()
        {
            _goodGate.SetGateEnum(GateType.Good);
            _goodGate.SetGateColor();
        }
    }
}

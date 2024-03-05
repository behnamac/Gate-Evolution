using Collectables;
using Gate.Cloth;
using TMPro;
using UnityEngine;

namespace Gate
{
    public class Gate : MonoBehaviour, ITriggerable
    {
        [SerializeField] private TextMeshProUGUI gatePrice;
        private GateType _type;
        private MeshRenderer _meshRenderer;


   
        public void SetGateEnum(GateType gateType)
        {
            _type = gateType;
        }

        public void SetGateColor()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (_type == GateType.Good)
            {
                _meshRenderer.material.color = Color.green;
            }
            else
            {
                _meshRenderer.material.color = Color.red;
            }
        }

        public void SetGatePrice(SoDataGate price)
        {
            gatePrice.text = price.ToString();
        }


        public void TriggerAction()
        {
            throw new System.NotImplementedException();
        }

     
    }
}
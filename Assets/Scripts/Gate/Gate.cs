using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GateHolder
{
    public class Gate : MonoBehaviour, ITriggerable
    {
        private GateType type;
        private MeshRenderer meshRenderer;


        private void Start ()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            if(type==GateType.Good)
            {
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.red;
            }
        }

        public void SetGateEnum(GateType gateType)
        {
            type = gateType;
        }

        public void TriggerAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
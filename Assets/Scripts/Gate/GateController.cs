using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GateType { Good, Bad }
public class GateController : MonoBehaviour
{
    public DataGate dataGate;

    [HideInInspector] public GateType gateType;
    [Tooltip("This variable specifies the location of the item sprite")]
    [SerializeField] Transform clothSpritePoint;
    [Tooltip("This variable indicates the value of the item")]
    [SerializeField] TextMeshPro itemePriceText;

    [SerializeField] MeshRenderer[] panelMesh;
    Transform cloth;
    //public MeshRenderer[] meshs;

    // Start is called before the first frame update
    void Start()
    {
        cloth = Instantiate(dataGate.spriteCloth, clothSpritePoint.position, clothSpritePoint.rotation);

       // meshs = p.GetComponentsInChildren<MeshRenderer>();

       // itemePriceText.text = "$ " + (dataGate.clothPrice * GameManagerld.Instance.levelNumber);
       // GameManagerld.OnUpgrade += SetPriceText;
       // GameManagerld.OnReset += ResetLevel;
    }

    public void SetMaterials(Material material, GateType type)
    {
        foreach (var item in panelMesh)
        {
            item.material = material;

        }
        // for (int i = 0; i < meshs.Length; i++)
        // {
        //      meshs[i].material = material;
        //  }
        gateType = type;
    }
    
    void SetPriceText()
    {
       // itemePriceText.text = "$ " + (dataGate.clothPrice * GameManagerld.Instance.levelNumber);
    }

    public void DestroyCloth() 
    {
        ParticleManager.PlayParticle("destroyCloth", cloth.position, Quaternion.identity);
        Destroy(cloth.gameObject);
    }

    void ResetLevel() 
    {
      // GameManagerld.OnUpgrade -= SetPriceText;
       // GameManagerld.OnReset -= ResetLevel;
    }
}

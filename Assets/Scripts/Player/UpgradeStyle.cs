using UnityEngine;

public class UpgradeStyle : MonoBehaviour
{
    [SerializeField] private ClothType[] levelTypes;
    private readonly string LEVELUP_PARICLE = "LevelUp";

    private int currentStyle;
    private float offset = 2.0f;


    public void UpgradeCharacter(int index)
    {

        currentStyle += index;

        currentStyle = Mathf.Clamp(currentStyle, 0, levelTypes.Length - 1);

        SetModel(currentStyle);

        Vector3 spawnPos = transform.position + Vector3.up * offset;
        ParticleManager.PlayParticle(LEVELUP_PARICLE, spawnPos, Quaternion.identity);

    }

    public string GetStyleName()
    {
        return levelTypes[currentStyle].clothData.ClothName;
    }

    public int GetStyleNumber()
    {
        return levelTypes.Length;
    }

    private void SetModel(int index)
    {
        for (int i = 0; i < levelTypes.Length; i++)
        {
            levelTypes[i].ClothModel.gameObject.SetActive(false);
        }

        levelTypes[index].ClothModel.gameObject.SetActive(true);
    }


    [System.Serializable]
    public struct ClothType
    {
        public SODataGate clothData;
        public GameObject ClothModel;
    }
}
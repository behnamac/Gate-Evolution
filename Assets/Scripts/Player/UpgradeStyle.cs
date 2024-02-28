using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStyle : MonoBehaviour
{
    [SerializeField] private LevelType[] levelTypes;
    private readonly string LEVELUP_PARICLE = "LevelUp";

    private int currentStyle;
    private float offset = 2.0f;


    public void UpgradeCharacter(int index)
    {

        currentStyle += index;

        currentStyle = Mathf.Clamp(currentStyle, 0, levelTypes.Length);

        SetModel(currentStyle);

        Vector3 spawnPos = transform.position + Vector3.up * offset;

        ParticleManager.PlayParticle(LEVELUP_PARICLE, spawnPos, Quaternion.identity);
    }

    public string GetStyleName()
    {
        return levelTypes[currentStyle].name;
    }

    public int GetStyleNumber()
    {
        return levelTypes.Length;
    }

    private void SetModel(int index)
    {
        for (int i = 0; i < levelTypes.Length; i++)
        {
            levelTypes[i].Model.gameObject.SetActive(false);
        }

        levelTypes[index].Model.gameObject.SetActive(true);
    }







    [System.Serializable]
    public class LevelType
    {
        public string name;
        public int level;
        public GameObject Model;
    }
}
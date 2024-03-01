using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItems", menuName = "GameData/LevelItems")]
public class SOLevelItems : ScriptableObject
{
    public GameObject[] collectiblePrefab;
    public float lineWidth;
    public float lineLength;
    public float offset;
    public float distanceBetweenCollectibles;

}

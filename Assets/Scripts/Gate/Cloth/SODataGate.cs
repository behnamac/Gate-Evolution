using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="gate", menuName = "GameData/Gate")]
public class SODataGate : ScriptableObject
{
    public string ClothName;

    public Transform[] Model;

    public int Level;

    public int MinPrice,MaxPrice;
}

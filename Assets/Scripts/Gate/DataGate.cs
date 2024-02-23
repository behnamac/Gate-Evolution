using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothType { Neutral, Wild, Dancer, Stripper }
public enum ClothBody { UpperBody, LowerBody, Head, Eyes, Lip, Hands, Foots, }
[CreateAssetMenu(menuName = "DataBase/Gate")]
public class DataGate : ScriptableObject
{
    [Tooltip("With this string you order which clothes to activate")]
    public string clothName;

    public ClothType clothType;
    [Tooltip("This variable determines which part of the body the garment belongs to")]
    public ClothBody clothBody;

    [Tooltip("This variable indicates what cloth this gate is going to give to the player. From inside the Prefabs folder, you can assign an item of sprite to this variable.")]
    public Transform spriteCloth;
    [Tooltip("This variable specifies the value of the cloth")]
    public int clothPrice;
}

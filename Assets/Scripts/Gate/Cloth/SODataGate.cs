using UnityEngine;
using UnityEngine.Serialization;

namespace Gate.Cloth
{
    [CreateAssetMenu(fileName ="gate", menuName = "GameData/Gate")]
    public class SoDataGate : ScriptableObject
    {
        [FormerlySerializedAs("ClothName")] public string clothName;

        [FormerlySerializedAs("Model")] public Transform[] model;

        [FormerlySerializedAs("Level")] public int level;

        public int MinPrice,MaxPrice;
    }
}

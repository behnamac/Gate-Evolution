using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Collectables
{
    public class ItemsSpawner : MonoBehaviour
    {

        [SerializeField] private LevelItems[] levelItems;

        private Vector3 _spawnPosition;
        private Vector3 _spawnRotation;

        private void OnEnable()
        {
            SpawnCollectibles();
        }


        void SpawnCollectibles()
        {

            for (int i = 0; i < levelItems.Length; i++)
            {
                var levelItem = levelItems[i].solevelItems;
                var spawnPoint = levelItems[i].spawnPoint;
                while (levelItems[i].distanceCovered < levelItem.lineLength && levelItems[i].numberOfCollectibles > 0)
                {
                    int xRandom =DoRandom.SetRandom(0, levelItems.Length);
                    if (xRandom == 0)
                    {
                        _spawnPosition = spawnPoint.position + transform.forward * levelItems[i].distanceCovered;
                    }
                    else
                    {
                        _spawnPosition = spawnPoint.position + transform.forward * (levelItems[i].distanceCovered) + transform.right * levelItem.offset;
                    }
                    int random = DoRandom.SetRandom(0, levelItem.collectiblePrefab.Length);

                    var spawnRotation = levelItems[i].spawnPoint.rotation;
                    Instantiate(levelItem.collectiblePrefab[random], _spawnPosition, spawnRotation, spawnPoint);

                    levelItems[i].distanceCovered += levelItem.distanceBetweenCollectibles;

                    levelItems[i].numberOfCollectibles--;
                }
            }
        }




        [System.Serializable]
        public struct LevelItems
        {
            [FormerlySerializedAs("Name")] public string name;
            [FormerlySerializedAs("SolevelItems")] public SOLevelItems solevelItems;
            [FormerlySerializedAs("NumberOfCollectibles")] public int numberOfCollectibles;
            [FormerlySerializedAs("DistanceCovered")] public float distanceCovered;
            [FormerlySerializedAs("SpawnPoint")] public Transform spawnPoint;

        }
    }
}



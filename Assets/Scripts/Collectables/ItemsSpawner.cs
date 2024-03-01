using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{

    [SerializeField] private LevelItems[] levelItems;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    private void OnEnable()
    {
        SpawnCollectibles();
    }


    void SpawnCollectibles()
    {

        for (int i = 0; i < levelItems.Length; i++)
        {
            var levelItem = levelItems[i].SolevelItems;
            var SpawnPoint = levelItems[i].SpawnPoint;
            while (levelItems[i].DistanceCovered < levelItem.lineLength && levelItems[i].NumberOfCollectibles > 0)
            {
                int _xRandom =DoRandom.SetRandom(0, levelItems.Length);
                if (_xRandom == 0)
                {
                    spawnPosition = SpawnPoint.position + transform.forward * levelItems[i].DistanceCovered;
                }
                else
                {
                    spawnPosition = SpawnPoint.position + transform.forward * (levelItems[i].DistanceCovered) + transform.right * levelItem.offset;
                }
                int random = DoRandom.SetRandom(0, levelItem.collectiblePrefab.Length);

                var spawnRotation = levelItems[i].SpawnPoint.rotation;
                Instantiate(levelItem.collectiblePrefab[random], spawnPosition, spawnRotation, SpawnPoint);

                levelItems[i].DistanceCovered += levelItem.distanceBetweenCollectibles;

                levelItems[i].NumberOfCollectibles--;
            }
        }
    }




    [System.Serializable]
    public struct LevelItems
    {
        public string Name;
        public SOLevelItems SolevelItems;
        public int NumberOfCollectibles;
        public float DistanceCovered;
        public Transform SpawnPoint;

    }
}



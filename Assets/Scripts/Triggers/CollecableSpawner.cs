using UnityEngine;

public class CollecableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefab;
    [SerializeField] private float lineWidth;
    [SerializeField] private float lineLength;
    [SerializeField] private int numberOfCollectibles;
    [SerializeField] private float distanceBetweenCollectibles = 5;
    [SerializeField] private float offset = 2;
    private Vector3 spawnPosition;

    private void OnEnable()
    {
        SpawnCollectibles();
    }


    void SpawnCollectibles()
    {
        float distanceCovered = 0f;

        while (distanceCovered < lineLength && numberOfCollectibles > 0)
        {
            int _xRandom = GetRandom(0, 2);
            if (_xRandom == 0)
            {
                spawnPosition = transform.position + transform.forward * distanceCovered;
            }
            else
            {
                spawnPosition = transform.position + transform.forward * (distanceCovered) + transform.right * offset;
            }
            int random = GetRandom(0, collectiblePrefab.Length);

            Instantiate(collectiblePrefab[random], spawnPosition, Quaternion.identity, transform);

            distanceCovered += distanceBetweenCollectibles;

            numberOfCollectibles--;
        }
    }


    private int GetRandom(int min, int max)
    {
        return Random.Range(min, max);
    }
}



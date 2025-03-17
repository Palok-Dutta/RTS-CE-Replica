using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformPrefab;
    public int poolSize = 7;
    public float spawnInterval = 6.75f;
    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
    public float platformSpeed = 5f;

    private List<GameObject> platformPool;
    private int currentIndex = 0;
    public int length=10;
    public GameObject cube;
    public GameObject player;
    public GameObject coins;

    void Start()
    {
        // Create the platform pool
        platformPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platformPrefab, new Vector3(player.transform.position.x,0f,0f),Quaternion.identity);
            for (int j = 0; j < length; j++)
            {
                
                Vector3 spawnPos = Vector3.zero;

                 float xrange = UnityEngine.Random.Range(-200f, 200f);
                float zrange = UnityEngine.Random.Range(-47f, 47f);
                spawnPos = new Vector3(player.transform.position.x+xrange,0f,zrange);

                // Check if the spot is clear (0.5f is the radius — adjust as needed)
                if (!Physics.CheckSphere(spawnPos, 1f))
                {
                    GameObject cubes = Instantiate(cube, spawnPos, Quaternion.identity);
                    cubes.transform.SetParent(platform.transform);
                }
            
            

            
            }
            Vector3 spawncoin = Vector3.zero;
            for (int k = 0; k < 15; k++)
            {
                float xcoinrange = UnityEngine.Random.Range(-100f, 100f);
                float zcoinrange = UnityEngine.Random.Range(-47f, 47f);
                spawncoin = new Vector3(player.transform.position.x+xcoinrange,1f,zcoinrange);

                // Check if the spot is clear (0.5f is the radius — adjust as needed)
                if (!Physics.CheckSphere(spawncoin, 2f))
                {
                    GameObject coin = Instantiate(coins, spawncoin, Quaternion.identity);
                    coin.transform.SetParent(platform.transform);
                }
            }
            platform.SetActive(false);
            platformPool.Add(platform);
        }

        // Start spawning platforms every 2 seconds
        InvokeRepeating(nameof(ActivateNextPlatform), 0f, spawnInterval);
    }

    void ActivateNextPlatform()
    {
        // Get the next platform from the pool
        GameObject platform = platformPool[currentIndex];
        currentIndex = (currentIndex + 1) % poolSize;

        // Reset the platform's position and activate it
        Vector3 spawnPosition = new Vector3(player.transform.position.x,0f,0f);
        platform.transform.position = spawnPosition;
        platform.SetActive(true);
        platform.GetComponent<PlatformMover>().StartMoving(platformSpeed);
    }
}

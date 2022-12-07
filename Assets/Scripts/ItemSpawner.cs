using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] items;
    public Transform playerTransform;

    public float maxDistance = 10f;
    public float timeBetSpawnMax = 100f;
    public float timeBetSpawnMin = 30f;
    private float timeBetspawn;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        timeBetspawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastSpawnTime + timeBetspawn && playerTransform != null)
        {
            lastSpawnTime = Time.time;
            timeBetspawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            SpawnItem();
        }
    }

    public void SpawnItem()
    {
        Vector3 spawnPosition = GetRandomPosition(playerTransform.position, maxDistance);

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPosition(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        return randomPos;

    }
}

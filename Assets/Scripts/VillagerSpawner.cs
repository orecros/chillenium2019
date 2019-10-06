using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : MonoBehaviour {

    public GameObject villager;

    public List<Transform> spawnPoints = new List<Transform>();

    private int random;

    private void Start() {
        for(int i = 0; i < spawnPoints.Count; i++)
            SpawnVillager(i);
    }

    public void SpawnVillager() {
        random = Random.Range(0, spawnPoints.Count - 1);
        Instantiate(villager, spawnPoints[random].position, Quaternion.identity, transform);
    }

    public void SpawnVillager(int num) {
        Instantiate(villager, spawnPoints[num].position, Quaternion.identity, transform);
    }

    public void CreateSpawnPoint() {
        GameObject newObj = new GameObject();
        newObj.transform.parent = transform;
        newObj.name = "Spawn Point";
        spawnPoints.Add(newObj.transform);
    }

}

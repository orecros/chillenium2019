using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject monster;
    public float waitTime, chanceTimer, spawnRate;
    public int initialSpawnAmount;
    
    public List<Transform> spawnPoints = new List<Transform>();

    private int random;

    private void Start() {
        for(int i = 0; i < initialSpawnAmount; i++)
            SpawnMonster();
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning() {
        yield return new WaitForSeconds(waitTime);

        int rand = 0;
        while(true) {
            rand = Random.Range(0, 100);
            if(rand <= spawnRate)
                SpawnMonster();
            yield return new WaitForSeconds(chanceTimer);
        }
    }

    public void SpawnMonster() {
        random = Random.Range(0, spawnPoints.Count - 1);
        Instantiate(monster, spawnPoints[random].position, Quaternion.identity, transform);
    }

    public void CreateSpawnPoint() {
        GameObject newObj = new GameObject();
        newObj.transform.parent = transform;
        newObj.name = "Spawn Point";
        spawnPoints.Add(newObj.transform);
    }

}

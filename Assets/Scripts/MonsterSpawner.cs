using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public static MonsterSpawner Instance;
    public GameObject monster;
    public float waitTime;
    
    public List<Transform> spawnPoints = new List<Transform>();
    public static List<GameObject> monsterList = new List<GameObject>();

    private int rand, waveNum;

    private void Start() {
        Instance = this;
        for(int i = 0; i < spawnPoints.Count; i++)
            SpawnMonster(i);
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning() {
        int spawnCount = 0;
        while(true) {
            while(monsterList.Count != 0) {
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);

            waveNum++;
            spawnCount = spawnPoints.Count + waveNum;
            spawnCount += waveNum > 3 ? Random.Range(1, waveNum) : 
                waveNum > 8 ? Random.Range(3, waveNum) :
                waveNum > 15 ? Random.Range(5, waveNum)
                : 0;
            if(PlayerManager.playerCount == 2)
                spawnCount = (int)(spawnCount * Random.Range(1.25f, 1.8f));
            else if(PlayerManager.playerCount == 3)
                spawnCount = (int)(spawnCount * Random.Range(1.75f, 2.5f));

            for( ; spawnCount > 0; spawnCount--) {
                SpawnMonster();
                yield return new WaitForSeconds(0.5f + Random.Range(0, 1.5f));
            }
        }
    }

    public void SpawnMonster() {
        rand = Random.Range(0, spawnPoints.Count);
        monsterList.Add(Instantiate(monster, spawnPoints[rand].position, Quaternion.identity, transform));
    }

    public void SpawnMonster(int pos) {
        monsterList.Add(Instantiate(monster, spawnPoints[pos].position, Quaternion.identity, transform));
    }

    public void CreateSpawnPoint() {
        GameObject newObj = new GameObject();
        newObj.transform.parent = transform;
        newObj.name = "Spawn Point";
        spawnPoints.Add(newObj.transform);
    }

}

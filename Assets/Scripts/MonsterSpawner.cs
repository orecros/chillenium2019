using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public static MonsterSpawner Instance;
    public GameObject monster;
    public float waitTime;
    
    public List<Transform> spawnPoints = new List<Transform>();
    public static List<GameObject> monsterList = new List<GameObject>();

    private float timeOut = 15f;
    private int rand, waveNum;

    private void Start() {
        Instance = this;
        for(int i = 0; i < spawnPoints.Count; i++)
            SpawnMonster(i);
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning() {
        float timeOutTimer = 0;
        int spawnCount = 0;

        while(!GameManager.GameOver) {
            while(monsterList.Count != 0) {
                yield return null;
                timeOutTimer += Time.deltaTime;
                if(timeOutTimer > timeOut)
                    break;
            }
            yield return new WaitForSeconds(waitTime);

            waveNum++;
            timeOut++;
            spawnCount = spawnPoints.Count + waveNum;
            spawnCount += waveNum > 5 ? Random.Range(1, waveNum - 2) : 
                waveNum > 10 ? Random.Range(3, waveNum - 4) :
                waveNum > 15 ? Random.Range(5, waveNum - 6)
                : 0;
            if(PlayerManager.playerCount == 2)
                spawnCount = (int)(spawnCount * Random.Range(1.4f, 1.8f));
            else if(PlayerManager.playerCount == 3)
                spawnCount = (int)(spawnCount * Random.Range(1.8f, 2.5f));

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : MonoBehaviour {

    public static VillagerSpawner vs;

    public GameObject villager;

    public List<Transform> spawnPoints = new List<Transform>();
    public static List<GameObject> villagerList = new List<GameObject>();

    private int random;

    private void Awake() {
        if(vs == null)
            vs = this;
        else
            Destroy(gameObject);

        for(int i = 0; i < spawnPoints.Count; i++)
            SpawnVillager(i);
    }

    public void SpawnVillager() {
        random = Random.Range(0, spawnPoints.Count - 1);
        villagerList.Add(Instantiate(villager, spawnPoints[random].position, Quaternion.identity, transform));
    }

    public void SpawnVillager(int num) {
        villagerList.Add(Instantiate(villager, spawnPoints[num].position, Quaternion.identity, transform));
    }

    public void CreateSpawnPoint() {
        GameObject newObj = new GameObject();
        newObj.transform.parent = transform;
        newObj.name = "Spawn Point";
        spawnPoints.Add(newObj.transform);
    }

    public void SpawnOnJoin() {
        for(int i = 0; i < spawnPoints.Count; i++)
            SpawnVillager(i);
    }

    public void DeleteOnLeave() {
        for(int i = 0; i < spawnPoints.Count; i++) {
            //villagerList.RemoveAt(villagerList.Count - 1);
            GameObject toRemove = villagerList[villagerList.Count - 1];
            villagerList.Remove(toRemove);
            toRemove.GetComponent<HealthController>().OnDeath.Invoke();

            if(villagerList.Count == 1)
                break;
        }
    }

}

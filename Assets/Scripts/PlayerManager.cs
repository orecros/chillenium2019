using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static bool player1 = false, player2 = false, player3 = false;
    public static int playerCount;
    public static bool canJoin = true;

    public GameObject playerPrefab;
    public Vector3 spawnPos;

    private VillagerSpawner villagerSpawner;

    private void Start() {
        villagerSpawner = VillagerSpawner.vs;
    }

    private void LateUpdate() {
        if(canJoin) {
            if(!player1 && Input.GetButtonDown("Act1")) { // Insert player 1
                Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                AddPlayer();
            } else if(!player2 && Input.GetButtonDown("Act2")) { // Insert player 2
                Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                AddPlayer();
            } else if(!player3 && Input.GetButtonDown("Act3")) { // Insert player 3
                Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                AddPlayer();
            }
        }
        if(playerCount > 3 || playerCount < 0)
            Debug.LogError("Player Count is out bounds! Currently at " + playerCount);
    }

    private void AddPlayer() {
        playerCount++;
        if(villagerSpawner != null)
            villagerSpawner.SpawnOnJoin();
    }

    public static void ResetPlayers() {
        player1 = false;
        player2 = false;
        player3 = false;
        playerCount = 0;

        canJoin = true;
    }

}

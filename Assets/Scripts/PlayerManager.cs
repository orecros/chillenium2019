using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static bool player1 = false, player2 = false, player3 = false;
    public static int playerCount;

    public GameObject playerPrefab;
    public Vector3 spawnPos;

    private void LateUpdate() {
        if(!player1 && Input.GetButtonDown("Act1")) { // Insert player 1
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            playerCount++;
        } else if(!player2 && Input.GetButtonDown("Act2")) { // Insert player 2
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            playerCount++;
        } else if(!player3 && Input.GetButtonDown("Act3")) { // Insert player 3
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            playerCount++;
        }
        if(playerCount > 3 || playerCount < 0)
            Debug.LogError("Player Count is out bounds! Currently at " + playerCount);
    }

    public static void ResetPlayers() {
        player1 = false;
        player2 = false;
        player3 = false;
    }

}

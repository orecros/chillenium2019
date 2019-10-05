using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed = 0.15f;
    public int maxHealth;

    private int playerNum;

    private CharacterController controller;
    private float moveX, moveZ, quitTimer;
    private int health;
    private List<GameObject> interactInRange = new List<GameObject>();

    private void Awake() {
        // Set variables
        controller = GetComponent<CharacterController>();
        health = maxHealth;

        // Get player number
        if(!PlayerManager.player1) {
            playerNum = 1;
            PlayerManager.player1 = true;
        } else if(!PlayerManager.player2) {
            playerNum = 2;
            PlayerManager.player2 = true;
        } else if(!PlayerManager.player3) {
            playerNum = 3;
            PlayerManager.player3 = true;
        } else {
            Debug.LogError("Attempting to go over player limit!");
            Destroy(gameObject);
        }
        if(PlayerManager.playerCount == 0)
            PlayerManager.playerCount++;
    }

    private void FixedUpdate() {
        if(playerNum == 1) { // Player 1
            moveX = Input.GetAxis("Horizontal1");
            moveZ = Input.GetAxis("Vertical1");
        
            controller.Move(new Vector3(moveX, 0, moveZ) * moveSpeed);
        } else if(playerNum == 2) { // Player 2
            moveX = Input.GetAxis("Horizontal2");
            moveZ = Input.GetAxis("Vertical2");

            controller.Move(new Vector3(moveX, 0, moveZ) * moveSpeed);
        } else { // Player 2
            moveX = Input.GetAxis("Horizontal3");
            moveZ = Input.GetAxis("Vertical3");

            controller.Move(new Vector3(moveX, 0, moveZ) * moveSpeed);
        }
    }

    private void LateUpdate() {
        // Player quit
        if(PlayerManager.playerCount > 1) {
            if(playerNum == 1 && Input.GetButton("Exit1")) { // Player 1
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player1 = false;
                    Destroy(gameObject);
                }
            } else if(playerNum == 2 && Input.GetButton("Exit2")) { // Player 2
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player2 = false;
                    Destroy(gameObject);
                }
            } else if(playerNum == 3 && Input.GetButton("Exit3")) { // Player 3
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player3 = false;
                    Destroy(gameObject);
                }
            }


        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Interactable")) {
            interactInRange.Add(other.gameObject);
        }
    }

    private void OnDestroy() {
        if(playerNum > 0)
            PlayerManager.playerCount--;
    }

}

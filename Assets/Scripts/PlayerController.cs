using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed = 0.5f;

    private int playerNum;

    private CharacterController controller;
    private float moveX, moveZ;

    private void Awake() {
        // Set variables
        controller = GetComponent<CharacterController>();

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

}

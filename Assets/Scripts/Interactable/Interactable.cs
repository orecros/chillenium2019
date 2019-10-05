using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public GameObject iconPrefab;
    public Vector3 offset;

    [HideInInspector] public bool selected;
    [HideInInspector] public int playerCount;

    protected GameObject icon;

    protected void Start() {
        icon = Instantiate(iconPrefab, Camera.main.WorldToScreenPoint(transform.position + offset), Quaternion.identity, Canvas.canvas.transform);
        icon.SetActive(false);

        selected = false;
    }

    protected void LateUpdate() {
        //Debug.Log(playerCount + " " + selected);
        if(selected && playerCount > 0)
            icon.SetActive(true);
        else {
            icon.SetActive(false);
            selected = false;
        }
    }

    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            playerCount++;
        }
    }

    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            playerCount--;
        }
    }

    public virtual void Interact() {
        Debug.Log("Interacting with " + name);
    }

    public virtual void Interact(GameObject player, int playerNum) {
        Debug.Log("Player " + playerNum + " interacting with " + name);
    }

}

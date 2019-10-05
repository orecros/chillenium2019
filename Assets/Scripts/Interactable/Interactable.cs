using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public GameObject iconPrefab;
    public Vector3 offset;

    [HideInInspector] public bool canInteract, selected;
    [HideInInspector] public int playerCount;

    protected GameObject icon;

    protected virtual void Start() {
        icon = Instantiate(iconPrefab, Camera.main.WorldToScreenPoint(transform.position + offset), Quaternion.identity, GlobalCanvas.canvas.transform);
        icon.SetActive(false);

        selected = false;
    }

    protected virtual void LateUpdate() {
        //Debug.Log(playerCount + " " + selected);
        if(selected && playerCount > 0 && canInteract)
            icon.SetActive(true);
        else {
            icon.SetActive(false);
            selected = false;
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
            playerCount++;
    }

    protected virtual void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
            playerCount--;
    }

    public virtual void Interact() {
        Debug.Log("Interacting with " + name);
    }

    public virtual void Interact(GameObject player, int playerNum) {
        Debug.Log("Player " + playerNum + " interacting with " + name);
    }

}

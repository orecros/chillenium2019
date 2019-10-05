using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public GameObject iconPrefab;
    public Vector3 offset;

    [HideInInspector] public bool selected;

    protected GameObject icon;
    protected int playerCount;

    protected void Start() {
        icon = Instantiate(iconPrefab, Camera.main.WorldToScreenPoint(transform.position + offset), Quaternion.identity, Canvas.canvas.transform);
        icon.SetActive(false);

        selected = false;
    }

    protected void LateUpdate() {
        if(playerCount <= 0)
            icon.SetActive(false);
        else if(selected)
            icon.SetActive(true);
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

}

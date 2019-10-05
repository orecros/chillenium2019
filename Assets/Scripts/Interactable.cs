using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public GameObject iconPrefab;
    public float iconHeight;

    protected GameObject icon;

    protected void Start() {
        icon = Instantiate(iconPrefab, transform.position + new Vector3(0, iconHeight), Quaternion.identity, transform);
    }

}

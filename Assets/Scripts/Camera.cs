using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    
    public static GameObject cam;

    private void Awake() {
        cam = gameObject;
    }

}

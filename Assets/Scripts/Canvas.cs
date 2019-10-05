using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour {

    public static GameObject canvas;

    private void Awake() {
        canvas = gameObject;
    }

}

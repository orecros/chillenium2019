using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCanvas : MonoBehaviour {

    public static GameObject canvas;

    public GameObject pauseMenu;

    private void Awake() {
        canvas = gameObject;
        try {
            if(pauseMenu == null)
                pauseMenu = transform.Find("Pause Menu").gameObject;
        } catch {
            Debug.LogError("No pause menu found in canvas!");
        }
    }

}

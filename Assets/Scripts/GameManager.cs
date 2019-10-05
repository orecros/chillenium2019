using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static bool paused;

    private void Start() {
        if(GlobalCanvas.canvas.GetComponent<GlobalCanvas>().pauseMenu.activeSelf) {
            Paused = true;
        }
    }

    private void Update() {
        if(Input.GetButtonDown("Pause")) {
            Paused = !paused;
        }
    }

    public bool Paused {
        get {
            return paused;
        }
        set {
            if(value != paused) {
                paused = !paused;
                if(paused) {
                    GlobalCanvas.canvas.GetComponent<GlobalCanvas>().pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                } else {
                    GlobalCanvas.canvas.GetComponent<GlobalCanvas>().pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            } else {
                Debug.Log("Attempting to set pause state to current state: " + paused);
            }
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

}

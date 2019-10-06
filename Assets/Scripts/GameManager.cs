using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static bool paused;
    public static GameManager Instance;
    List<HealthController> Objectives;

    private void Awake() {
        Instance = this;
        Objectives = new List<HealthController>();
    }

    private void Start() {
        if(GlobalCanvas.canvas.GetComponent<GlobalCanvas>().pauseMenu.activeSelf) 
            Paused = true;
    }

    private void Update() {
        if(Input.GetButtonDown("Pause")) {
            Paused = !paused;
        }
    }

    public void SetPause(bool set) {
        Paused = set;
    }

    public static bool Paused {
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

    public static void LoadSceneStatic(string scene) {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene) {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void RegisterObjective(HealthController objective) {
        Objectives.Add(objective);
    }

    public void MarkObjectiveLost(HealthController objective) {
        Objectives.Remove(objective);
        
        if(Objectives.Count == 0) {
            DoDefeat();
        }
    }

    public void DoDefeat() {
        print("the players lose :(");
    }
}

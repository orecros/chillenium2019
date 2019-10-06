using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static bool paused;
    public static GameManager Instance;
    List<HealthController> Houses;
    List<HealthController> Players;

    private void Awake() {
        Instance = this;
        Houses = new List<HealthController>();
        Players = new List<HealthController>();
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

    public void RegisterObjective(HealthController objective, ObjectiveMarker.Type type) {
        if(type == ObjectiveMarker.Type.House) {
            Houses.Add(objective);
        }
        else {
            Players.Add(objective);
        }
    }

    public void MarkObjectiveLost(HealthController objective, ObjectiveMarker.Type type) {
        if (type == ObjectiveMarker.Type.House) {
            Houses.Remove(objective);

            if (Houses.Count == 0) {
                DoDefeat();
            }
        }
        else {
            Players.Remove(objective);

            if (Players.Count == 0) {
                DoDefeat();
            }
        }
    }

    public void DoDefeat() {
        print("the players lose :(");
        if(Paused)
            Paused = false;
        //MonsterSpawner.Instance.chanceTimer = 0.5f;
        //MonsterSpawner.Instance.spawnRate = 100f;

        StartCoroutine(GlobalCanvas.canvas.GetComponent<GlobalCanvas>().GameOver());
    }
}

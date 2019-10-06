using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static bool paused, gameOver;
    public static GameManager Instance;
    List<HealthController> Houses;
    List<HealthController> Players;

    private void Awake() {
        Instance = this;
        Houses = new List<HealthController>();
        Players = new List<HealthController>();
    }

    private void Start() {
        /*if(GlobalCanvas.canvas.GetComponent<GlobalCanvas>().pauseMenu.activeSelf) 
            Paused = true;*/
        Paused = false;
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
            return paused || gameOver;
        }
        set {
            gameOver = false;
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
                //Debug.Log("Attempting to set pause state to current state: " + paused);
            }
        }
    }

    public static void LoadSceneStatic(string scene) {
        Time.timeScale = 1;
        PlayerManager.ResetPlayers();
        PlayerManager.canJoin = true;

        if(scene.Equals("")) {
            scene = "Field0" + Random.Range(1, 4);
        }
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene) {
        LoadSceneStatic(scene);
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
        PlayerManager.canJoin = false;
        if(!gameOver) {
            print("the players lose :(");
            if(Paused)
                Paused = false;
            gameOver = true;
            //MonsterSpawner.Instance.chanceTimer = 0.5f;
            //MonsterSpawner.Instance.spawnRate = 100f;

            StartCoroutine(GlobalCanvas.canvas.GetComponent<GlobalCanvas>().GameOver());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    
    public GameObject main, credits;

    private void Awake() {
        if(credits.activeSelf)
            SwapMenu();
    }

    public void SwapMenu() {
        if(credits.activeSelf) {
            main.SetActive(true);
            credits.SetActive(false);
        } else {
            main.SetActive(false);
            credits.SetActive(true);
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame() {
        Application.Quit();
    }

}

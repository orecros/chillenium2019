using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    
    public GameObject main, credits, camera;
    public float rotateTime;
    public UIButton startButton, creditsButton, exitButton;
    
    private bool scrollBuffer;
    private int arrowPos = 5;
    public static GameObject selectedButton;

    private Transform cameraTransform;
    private float timer, radians;

    private void Awake() {
        if(credits.activeSelf)
            SwapMenu();

        cameraTransform = camera.transform;
    }

    private void Update() {
        if(MaxAbsVerticalInput() > 0.125f) {
            if(!scrollBuffer) {
                StartCoroutine(ScrollPause());
                arrowPos += GetScrollDir();
                UpdateArrow();
            }
        } /*else
            scrollBuffer = false;*/

        if(!credits.activeSelf && (Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3"))) {
            if(arrowPos == 0)
                LoadScene("");
            else if(arrowPos == 1)
               SwapMenu();
            else
                ExitGame();
        } else if(credits.activeSelf && (Input.GetButtonDown("Back") || Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3")))
            SwapMenu();

        if(selectedButton != null && (Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3")))
            selectedButton.GetComponent<Button>().onClick.Invoke();
    }

    private void LateUpdate() {
        // rotate camera
        radians = (timer / rotateTime) * 2 * Mathf.PI;
        cameraTransform.position = new Vector3(Mathf.Cos(radians), 1.25f, Mathf.Sin(radians)) * 10;
        cameraTransform.LookAt(new Vector3(0, -5f, 0));

        timer += Time.deltaTime;
        if(timer > rotateTime)
            timer = 0;
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
        PlayerManager.ResetPlayers();
        PlayerManager.canJoin = true;

        if(scene.Equals("")) {
            scene = "Field0" + Random.Range(1, 4);
        }
        SceneManager.LoadScene(scene);
    }

    public void ExitGame() {
        Application.Quit();
    }

    private IEnumerator ScrollPause() {
        scrollBuffer = true;
        yield return new WaitForSeconds(0.25f);
        scrollBuffer = false;
    }

    private void UpdateArrow() {
        if(arrowPos >= 3)
            arrowPos = 0;
        else if(arrowPos <= -1)
            arrowPos = 2;

        if(selectedButton != null)
            selectedButton.GetComponent<UIButton>().OnExit();
        if(arrowPos == 0) {
            startButton.OnHover();
            //creditsButton.OnExit();
            //exitButton.OnExit();
        } else if(arrowPos == 1) {
            //startButton.OnExit();
            creditsButton.OnHover();
            //exitButton.OnExit();
        } else if(arrowPos == 2) {
            //startButton.OnExit();
            //creditsButton.OnExit();
            exitButton.OnHover();
        }

        /*if(arrowPos == 0)
            resume.GetComponent<UIButton>().OnHover();
        else
            mainMenu.GetComponent<UIButton>().OnHover();*/
    }

    private float MaxAbsVerticalInput() {
        return Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical1")), Mathf.Abs(Input.GetAxis("Vertical2")), Mathf.Abs(Input.GetAxis("Vertical3")));
    }

    private int GetScrollDir() {
        return -1 * (int)Mathf.Sign(Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3"));
    }

}

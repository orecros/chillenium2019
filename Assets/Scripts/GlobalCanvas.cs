using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCanvas : MonoBehaviour {

    public static GameObject canvas;
    public static GameObject selectedButton;

    public GameObject pauseMenu, resume, mainMenu, gameOverScreen, startOver, quit;
    
    //private RectTransform arrow;
    private bool scrollBuffer;
    private int arrowPos = 1;

    private void Awake() {
        canvas = gameObject;
        try {
            if(pauseMenu == null)
                pauseMenu = transform.Find("Pause Menu").gameObject;
            //arrow = pauseMenu.transform.Find("Arrow").GetComponent<RectTransform>();
            resume = pauseMenu.transform.Find("Resume").gameObject;
            mainMenu = pauseMenu.transform.Find("Main Menu").gameObject;

            gameOverScreen.SetActive(false);
        } catch {
            Debug.LogError("No pause menu found in canvas!");
        }
    }

    private void Update() {
        pauseMenu.transform.SetAsLastSibling();

        if(GameManager.Paused) {
            if(MaxAbsVerticalInput() > 0.125f) {
                if(!scrollBuffer) {
                    StartCoroutine(ScrollPause());
                    arrowPos += GetScrollDir();
                    UpdateArrow();
                }
            } else
                scrollBuffer = false;

            if(selectedButton != null && (Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3")))
                selectedButton.GetComponent<Button>().onClick.Invoke();
        } else
            arrowPos = 1;
    }

    private IEnumerator ScrollPause() {
        scrollBuffer = true;
        yield return new WaitForSeconds(0.35f);
        scrollBuffer = false;
    }

    private void UpdateArrow() {
        if(arrowPos >= 2)
            arrowPos = 0;
        else if(arrowPos <= -1)
            arrowPos = 1;

        if(selectedButton != null)
            selectedButton.GetComponent<UIButton>().OnExit();
        if(!gameOverScreen.activeSelf) {
            if(arrowPos == 0)
                resume.GetComponent<UIButton>().OnHover();
            else
                mainMenu.GetComponent<UIButton>().OnHover();
        } else {
            if(arrowPos == 0)
                startOver.GetComponent<UIButton>().OnHover();
            else
                quit.GetComponent<UIButton>().OnHover();
        }
    }

    private float MaxAbsVerticalInput() {
        return Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical1")), Mathf.Abs(Input.GetAxis("Vertical2")), Mathf.Abs(Input.GetAxis("Vertical3")));
    }

    private int GetScrollDir() {
        return (int)Mathf.Sign(Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3"));
    }

    public IEnumerator GameOver() {
        arrowPos = 5;
        yield return new WaitForSeconds(1f);
        
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().SetTrigger("Activate");
        
        while(true) {
            gameOverScreen.transform.SetAsLastSibling();
            yield return null;
        }
    }

}

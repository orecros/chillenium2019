using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCanvas : MonoBehaviour {

    public static GameObject canvas;

    public GameObject pauseMenu;
    
    private RectTransform arrow;
    private bool scrollBuffer;
    private int arrowPos = 0;

    private void Awake() {
        canvas = gameObject;
        try {
            if(pauseMenu == null)
                pauseMenu = transform.Find("Pause Menu").gameObject;
            arrow = pauseMenu.transform.Find("Arrow").GetComponent<RectTransform>();
        } catch {
            Debug.LogError("No pause menu found in canvas!");
        }
    }

    private void Update() {
        if(GameManager.Paused) {
            if(MaxAbsVerticalInput() > 0.125f) {
                if(!scrollBuffer) {
                    StartCoroutine(ScrollPause());
                    arrowPos += GetScrollDir();
                    UpdateArrow();
                }
            } else
                scrollBuffer = false;

            if(Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3")) {
                if(arrowPos == 0)
                    GameManager.Paused = false;
                else 
                    GameManager.LoadSceneStatic("MainMenu");
            }
        }
    }

    private IEnumerator ScrollPause() {
        scrollBuffer = true;
        yield return new WaitForSeconds(0.25f);
        scrollBuffer = false;
    }

    private void UpdateArrow() {
        if(arrowPos >= 2)
            arrowPos = 0;
        else if(arrowPos <= -1)
            arrowPos = 1;

        if(arrowPos == 0)
            arrow.localPosition = new Vector3(-125f, -10f, 0);
        else
            arrow.localPosition = new Vector3(-125f, -55f, 0);
    }

    private float MaxAbsVerticalInput() {
        return Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical1")), Mathf.Abs(Input.GetAxis("Vertical2")), Mathf.Abs(Input.GetAxis("Vertical3")));
    }

    private int GetScrollDir() {
        return (int)Mathf.Sign(Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3"));
    }

}

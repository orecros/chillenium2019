using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    
    public GameObject main, credits;

    private RectTransform arrow;
    private bool scrollBuffer;
    private int arrowPos = 0;

    private void Awake() {
        if(credits.activeSelf)
            SwapMenu();

        arrow = transform.GetChild(1).Find("Arrow").GetComponent<RectTransform>();
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
                LoadScene("SampleScene");
            else if(arrowPos == 1)
               SwapMenu();
            else
                ExitGame();
        } else if(credits.activeSelf && Input.GetButtonDown("Back"))
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

        if(arrowPos == 0)
            arrow.localPosition = new Vector3(-120f, 15f, 0);
        else if(arrowPos == 1)
            arrow.localPosition = new Vector3(-120f, -55f, 0);
        else
            arrow.localPosition = new Vector3(-120f, -125f, 0);
    }

    private float MaxAbsVerticalInput() {
        return Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical1")), Mathf.Abs(Input.GetAxis("Vertical2")), Mathf.Abs(Input.GetAxis("Vertical3")));
    }

    private int GetScrollDir() {
        return -1 * (int)Mathf.Sign(Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3"));
    }

}

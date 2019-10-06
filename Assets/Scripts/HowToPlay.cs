using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {

    private void Awake() {
        Time.timeScale = 0;
    }

    private void Update() {
        if(Input.GetButtonDown("Back") || Input.GetButtonDown("Act1") || Input.GetButtonDown("Act2") || Input.GetButtonDown("Act3")) {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    private void LateUpdate() {
        transform.SetAsLastSibling();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

    public Sprite blank;

    private Image image;
    private Sprite sprite;

    private void Awake() {
        image = GetComponent<Image>();
        sprite = image.sprite;
        image.sprite = blank;
    }

    private void LateUpdate() {
        if(GlobalCanvas.selectedButton != gameObject)
            image.sprite = blank;
    }

    public void OnHover() {
        image.sprite = sprite;
        GlobalCanvas.selectedButton = gameObject;
    }

    public void OnExit() {
        image.sprite = blank;
        GlobalCanvas.selectedButton = null;
    }
    
}

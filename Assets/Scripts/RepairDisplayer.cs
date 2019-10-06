using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairDisplayer : MonoBehaviour
{
    public float healFraction = 0;
    public Vector3 Offset;

    public GameObject RepairBarPrefab;
    [HideInInspector] public Image RepairBar;

    private void Start() {
        RepairBar = Instantiate(RepairBarPrefab, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), Quaternion.identity, GlobalCanvas.canvas.transform).GetComponent<Image>();
    }

    public void SetHealthFraction(float fraction) {
        healFraction = fraction;
    }
    
    protected void LateUpdate() {
       RepairBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)) + Offset;

        RepairBar.fillAmount = healFraction;
    }
}

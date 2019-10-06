using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStylePicker : MonoBehaviour
{
    public GameObject[] Styles;

    private void Awake() {
        int pickedIndex = Mathf.FloorToInt(Random.value * Styles.Length);
        
        for(int n = 0; n < Styles.Length; n++) {
            if(n == pickedIndex) {
                Styles[n].SetActive(true);
            }
            else {
                Destroy(Styles[n]);
            }
        }
    }
}

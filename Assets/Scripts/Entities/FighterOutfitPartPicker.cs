using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterOutfitPartPicker : MonoBehaviour
{
    public GameObject[] OutfitParts;
    public SolidsList solidsList;

    private void Awake() {
        int pickedIndex = Mathf.FloorToInt(Random.value * OutfitParts.Length);

        OutfitParts[pickedIndex].SetActive(true);

        if(solidsList != null) {
            solidsList.AddSolid(OutfitParts[pickedIndex]);
        }
    }
}

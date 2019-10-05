using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerTarget : Target {

    private void Start() {
        TargetSystem.Instance.AddVillagerTarget(this);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Villager")) {
            other.GetComponent<VillagerController>().OnEnterApproachRegion(this);
        }
    }

    public void DestroyThis() {
        TargetSystem.Instance.RemoveVillagerTarget(this);
        Destroy(this);
    }
}

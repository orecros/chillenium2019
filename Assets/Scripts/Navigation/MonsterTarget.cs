using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTarget : Target
{

    private void Start() {
        TargetSystem.Instance.AddMonsterTarget(this);
    }

    /*
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Monster")) {
            other.GetComponent<MonsterController>().OnEnterApproachRegion(this);
        }
    }
    */

    public void DestroyThis() {
        TargetSystem.Instance.RemoveMonsterTarget(this);
        Destroy(this);
    }
}

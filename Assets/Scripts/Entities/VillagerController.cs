using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : FighterController {

    Vector3 home;

    protected override void Awake() {
        base.Awake();

        home = transform.position;
    }

    protected override void Update() {
        base.Update();

        if(CurrentTarget == null) {
            navMeshAgent.SetDestination(home);
        }
    }

    protected override Target FindTarget(Target currentTarget) {
        return TargetSystem.Instance.GetVillagerTarget(transform.position, currentTarget as VillagerTarget);
    }

    public void RemoveVillager() {
        VillagerSpawner.villagerList.Remove(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : FighterController
{
    public Vector3 home = Vector3.zero;

    protected override void Update() {
        base.Update();

        if (CurrentTarget == null) {
            navMeshAgent.SetDestination(home);
        }
    }

    protected override Target FindTarget(Target currentTarget) {
        return TargetSystem.Instance.GetMonsterTarget(transform.position, currentTarget as MonsterTarget);
    }
}

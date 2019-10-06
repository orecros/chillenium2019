using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : FighterController
{
    protected override Target FindTarget(Target currentTarget) {
        return TargetSystem.Instance.GetMonsterTarget(transform.position, currentTarget as MonsterTarget);
    }
}

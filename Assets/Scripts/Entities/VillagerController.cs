using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : FighterController {

    protected override Target FindTarget(Target currentTarget, float currentInterestLevel) {
        return TargetSystem.Instance.GetVillagerTarget(transform.position, currentTarget as VillagerTarget, currentInterestLevel);
    }
}

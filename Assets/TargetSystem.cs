using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public static TargetSystem Instance;
    List<MonsterTarget> MonsterTargets;

    private void Awake() {
        Instance = this;
    }

    public void AddMonsterTarget(MonsterTarget newTarget) {
        if(MonsterTargets == null) {
            MonsterTargets = new List<MonsterTarget>();
        }

        MonsterTargets.Add(newTarget);
    }

    public void RemoveMonsterTarget(MonsterTarget oldTarget) {
        MonsterTargets.Remove(oldTarget);
    }

    public MonsterTarget GetMonsterTarget(Vector3 monsterLocation) {
        float bestScore = -1;
        MonsterTarget bestTarget = null;

        foreach(MonsterTarget m in MonsterTargets) {
            float score = m.CalculateValue(monsterLocation);
            if(score > bestScore) {
                bestScore = score;
                bestTarget = m;
            }
        }

        return bestTarget;
    }
}

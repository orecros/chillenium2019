﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetSystem : MonoBehaviour
{
    public static TargetSystem Instance;
    [SerializeField] List<MonsterTarget> MonsterTargets;
    NavMeshSurface NavMesh;

    private void Awake() {
        Instance = this;
        NavMesh = GetComponent<NavMeshSurface>();
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
    
    public MonsterTarget GetMonsterTarget(Vector3 monsterLocation, MonsterTarget currentBestTarget = null, float currentInterestLevel = 0) {

        foreach(MonsterTarget m in MonsterTargets) {
            float score = GetTargetBaseInterest(monsterLocation, m);
            if(score > currentInterestLevel) {
                currentInterestLevel = score;
                currentBestTarget = m;
            }
        }

        return currentBestTarget;
    }

    public float GetTargetBaseInterest(Vector3 position, Target target) {
        return target.CalculateInterestLevel(position);
    }
}
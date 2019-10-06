using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonsterSpawner))]
public class MonsterSpawnerInspector : Editor {
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        MonsterSpawner spawner = target as MonsterSpawner;

        if(GUILayout.Button("Add Spawn Point")) {
            spawner.CreateSpawnPoint();
        }

        if(GUILayout.Button("Force Spawn")) {
            spawner.SpawnMonster();
        }
    }

}

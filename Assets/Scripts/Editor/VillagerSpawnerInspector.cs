using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VillagerSpawner))]
public class VillagerSpawnerInspector : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        VillagerSpawner spawner = target as VillagerSpawner;

        if(GUILayout.Button("Add Spawn Point")) {
            spawner.CreateSpawnPoint();
        }

        if(GUILayout.Button("Force Spawn")) {
            spawner.SpawnVillager();
        }
    }

}

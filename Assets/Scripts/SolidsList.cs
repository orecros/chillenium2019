using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidsList : MonoBehaviour
{
    public GameObject[] Solids;

    public GameObject CloneAsPhysicsObjects() {
        GameObject parent = new GameObject();

        foreach(GameObject Solid in Solids) {
            GameObject clone = Instantiate(Solid, parent.transform, true);
            clone.SetActive(true);

            if(Solid.CompareTag("MultiPartHat")) {
                foreach (Transform child in clone.transform) {
                    child.gameObject.AddComponent<Rigidbody>();
                    MeshCollider m = child.gameObject.AddComponent<MeshCollider>();
                    m.convex = true;
                }
            }
            else {
                clone.AddComponent<Rigidbody>();
                MeshCollider m = clone.AddComponent<MeshCollider>();
                m.convex = true;
            }

            
        }

        return parent;
    }

    public void AddSolid(GameObject solid) {
        GameObject[] newSolids = new GameObject[Solids.Length + 1];

        for(int n = 0; n < Solids.Length; n++) {
            newSolids[n] = Solids[n];
        }
        newSolids[Solids.Length] = solid;

        Solids = newSolids;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquifier : MonoBehaviour
{
    public Material BodyMaterial;
    public SolidsList solidsList;
    public GameObject LiquidPrefab;

    public void MakeCorpse() {
        // make our corpse empty
        GameObject corpse = new GameObject();
        corpse.name = "Corpse";

        // create all the props and add as child
        GameObject solids = solidsList.CloneAsPhysicsObjects();
        solids.transform.parent = corpse.transform;

        // create our liquid prefab and add as child
        GameObject liquidObject = Instantiate(LiquidPrefab, corpse.transform, true);

        // snap our liquid prefab onto the ground
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, Vector3.down,out hitInfo, 2f,1 << 0,QueryTriggerInteraction.Ignore)) {
            liquidObject.transform.position = hitInfo.point;
        }
        else {
            liquidObject.transform.position = transform.position;
        }

        // recolor all the meshes in our liquid to our specified body material
        MeshRenderer[] renderers = liquidObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer v in renderers ) {
            v.material = BodyMaterial;
        }
    }
}

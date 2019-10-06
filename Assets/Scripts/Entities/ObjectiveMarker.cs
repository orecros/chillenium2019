using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
    private void Start() {
        GameManager.Instance.RegisterObjective(GetComponent<HealthController>());
    }

    public void MarkObjectiveLost() {
        GameManager.Instance.MarkObjectiveLost(GetComponent<HealthController>());
    }
}

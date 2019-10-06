using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
    public enum Type {
        Player, House
    }
    public Type ObjectiveType;


    private void Start() {
        GameManager.Instance.RegisterObjective(GetComponent<HealthController>(), ObjectiveType);
    }

    public void MarkObjectiveLost() {
        GameManager.Instance.MarkObjectiveLost(GetComponent<HealthController>(), ObjectiveType);
    }
}

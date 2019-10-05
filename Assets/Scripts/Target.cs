using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    /// <summary>
    /// how far away a monster can target this unit from
    /// </summary>
    public float MaxDetectionRange;

    /// <summary>
    /// the interest level this object has as the furthest distance
    /// </summary>
    public float MinimumInterest = 1;

    /// <summary>
    /// the interest level this object has at its closest distance
    /// </summary>
    public float MaximumInterest = 2;

    /// <summary>
    /// What multiplier to put on the entity's interest
    /// </summary>
    public float InterestMultiplier = 1;

    /// <summary>
    /// Get how interesting this target looks from Vector3 position
    /// </summary>
    /// <param name="otherPosition"></param>
    /// <returns></returns>
    public float CalculateValue(Vector3 otherPosition) {
        float distance = Vector3.Distance(otherPosition, transform.position);

        if( distance > MaxDetectionRange) {
            return 0;
        }

        return Mathf.Lerp(MaximumInterest,MinimumInterest,distance / MaxDetectionRange);
    }

    /*
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Mesh m = Resources.Load<Mesh>("Gizmos/circle");
        Gizmos.DrawWireMesh(m,transform.position,transform.rotation,Vector3.one * MaxDetectionRange);
    }
    */
}

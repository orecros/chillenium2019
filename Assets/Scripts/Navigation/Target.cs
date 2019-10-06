using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
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
    public float MaximumInterest = 3;

    /// <summary>
    /// What multiplier to put on the entity's interest
    /// </summary>
    public float InterestMultiplier = 1;

    /// <summary>
    /// Get how interesting this target looks from Vector3 position
    /// </summary>
    /// <param name="otherPosition"></param>
    /// <returns></returns>
    public float CalculateInterestLevel(Vector3 otherPosition) {
        float distance = Vector3.Distance(otherPosition, transform.position);

        if( distance > MaxDetectionRange) {
            return 0;
        }

        return Mathf.Lerp(MaximumInterest,MinimumInterest,distance / MaxDetectionRange);
    }

    public HealthController healthController;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Mesh m = Resources.Load<Mesh>("Gizmos/circle");
        Gizmos.DrawWireMesh(m,transform.position,transform.rotation,Vector3.one * MaxDetectionRange);
    }

    public void OnTargetted() {
        InterestMultiplier /= 2;
    }

    public void OnUnTargetted() {
        InterestMultiplier *= 2;
    }
}

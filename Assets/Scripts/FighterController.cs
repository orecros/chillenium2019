using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FighterController : MonoBehaviour {
    public enum State {
        Navigating,
        Approaching,
        Attacking
    }
    State fighterState;
    public State FighterState {
        get { return fighterState; }
        set {
            if (value == fighterState) return;

            if (value == State.Navigating) {
                navMeshAgent.updatePosition = true;
                navMeshAgent.updateRotation = true;
            }
            else if (value == State.Attacking) {
                nextAttackTime = Time.time + attackDelay;
            }

            if (fighterState == State.Navigating) {
                navMeshAgent.updatePosition = false;
                navMeshAgent.updateRotation = false;
                manualVelocity = navMeshAgent.velocity;
            }


            fighterState = value;
        }
    }

    public Vector3 manualVelocity;

    public float attackRange;
    public float attackDelay;
    public int attackDamage;
    float nextAttackTime;

    /// <summary>
    /// how long to wait before attempting to acquire a new target
    /// </summary>
    public float targetAcquisitionInterval;
    /// <summary>
    /// the next time at which to attempt to acquire a new target
    /// </summary>
    float nextTargetAcquiusitionQuery;
    /// <summary>
    /// our current target
    /// </summary>
    Target currentTarget = null;
    /// <summary>
    /// how interested our monster is in this target
    /// </summary>
    float currentInterestLevel = 0;

    NavMeshAgent navMeshAgent;
    CharacterController characterController;

    // Start is called before the first frame update
    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        AcquireNewTarget();
    }

    public void OnEnterApproachRegion(Target target) {
        if (target != currentTarget) return;

        FighterState = State.Approaching;
    }

    private void Update() {

        // state machine
        if (FighterState == State.Navigating) {
            DoStateNavigating();
        }
        else if (FighterState == State.Approaching) {
            DoStateApproaching();
        }
        else if (FighterState == State.Attacking) {
            DoStateAttacking();
        }
    }
    void DoStateNavigating() {
        if (nextTargetAcquiusitionQuery < Time.time || currentTarget == null) {
            AcquireNewTarget();
        }
    }
    void DoStateApproaching() {

        // if our target is invalid 
        if (currentTarget == null) {

            // go back to navigating
            FighterState = State.Navigating;
            return;
        }

        // if we are close enough to our target to attack,
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < attackRange) {

            // go to the attack state
            FighterState = State.Attacking;
            return;
        }

        // update our manual velocity
        DoManualVelocityAdjustment();

        // move towards our target
        characterController.Move(manualVelocity * Time.deltaTime);

    }
    void DoStateAttacking() {

        // if our target is invalid go back to navigating
        if (currentTarget == null) {
            FighterState = State.Navigating;
            return;
        }

        // if it's time to attack...
        if (nextAttackTime < Time.time) {
            // perform an attack
            DoAttack();
        }
    }
    void DoAttack() {
        currentTarget.healthController.DealDamage(attackDamage);
        nextAttackTime = Time.time + attackDelay;
    }
    void DoManualVelocityAdjustment() {
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

        // accelerate towards our max velocity using the values in our navmeshagent
        manualVelocity = Vector3.MoveTowards(manualVelocity, direction * navMeshAgent.speed, navMeshAgent.acceleration * Time.deltaTime);
    }
    void AcquireNewTarget() {

        if (TargetSystem.Instance == null) {
            // our target system has not been set up yet. just wait for the next frame to set a target.

            nextTargetAcquiusitionQuery = Time.time;
            return;
        }

        nextTargetAcquiusitionQuery = Time.time + targetAcquisitionInterval;

        // if our current best target was destroyed, make sure we're looking for ANY replacement target
        if (currentTarget == null) currentInterestLevel = 0;

        Target newTarget = FindTarget(currentTarget, currentInterestLevel);
        if (newTarget != null && newTarget != currentTarget) {
            currentInterestLevel += newTarget.CalculateInterestLevel(transform.position);
            currentTarget = newTarget;

            navMeshAgent.destination = currentTarget.transform.position;
        }
    }
    protected abstract Target FindTarget(Target currentTarget, float currentInterestLevel);

}

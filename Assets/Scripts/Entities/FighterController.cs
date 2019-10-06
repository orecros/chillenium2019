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
    public State fighterState;
    public State FighterState {
        get { return fighterState; }
        set {
            if (value == fighterState) return;

            if (value == State.Navigating) {
                //navMeshAgent.updatePosition = true;
                //navMeshAgent.updateRotation = true;
                navMeshAgent.isStopped = false;
            }
            else if (value == State.Attacking) {
                animator.SetBool("Moving", false);
                navMeshAgent.velocity = Vector3.zero;
                navMeshAgent.isStopped = true;
            }

            if (fighterState == State.Navigating) {
                //navMeshAgent.updatePosition = false;
                //navMeshAgent.updateRotation = false;
                //manualVelocity = navMeshAgent.velocity;
                navMeshAgent.isStopped = false;
            }


            fighterState = value;
        }
    }

    public Vector3 manualVelocity;

    public float approachRange;
    public float attackRange;
    public float attackDelay;
    public float attackApplicationDelay;
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

    Target currentTarget = null;
    /// <summary>
    /// our current target
    /// </summary>
    [HideInInspector]
    public Target CurrentTarget {
        get { return currentTarget; }
        set {
            if (currentTarget == value) return;
            if (currentTarget != null) {
                currentTarget.OnUnTargetted();
            }

            if (value != null) {
                value.OnTargetted();
            }

            currentTarget = value;
        }
    }

    protected NavMeshAgent navMeshAgent;
    CharacterController characterController;

    public Animator animator;

    // Start is called before the first frame update
    protected virtual void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        AcquireNewTarget();
    }

    public void OnEnterApproachRegion(Target target) {
        if (target != CurrentTarget) return;

        FighterState = State.Approaching;
    }

    protected virtual void Update() {
        
        // state machine
        if (FighterState == State.Navigating) {
            DoStateNavigating();
            if (navMeshAgent.velocity.magnitude >= 0.125f)
                animator.SetBool("Moving", true);
            else
                animator.SetBool("Moving", false);
        }
        else if (FighterState == State.Approaching) {
            DoStateApproaching();
            if (characterController.velocity.magnitude >= 0.125f)
                animator.SetBool("Moving", true);
            else
                animator.SetBool("Moving", false);
        }
        else if (FighterState == State.Attacking) {
            DoStateAttacking();
        }
    }
    void DoStateNavigating() {
        if (nextTargetAcquiusitionQuery < Time.time || CurrentTarget == null) {
            AcquireNewTarget();
        }

        if(CurrentTarget != null && GetDistanceToTarget() < approachRange) {
            FighterState = State.Approaching;
        }
    }
    void DoStateApproaching() {

        // if our target is invalid 
        if (CurrentTarget == null) {

            // go back to navigating
            FighterState = State.Navigating;
            return;
        }

        // if we are close enough to our target to attack,
        if (GetDistanceToTarget() < attackRange) {

            // go to the attack state
            FighterState = State.Attacking;
            return;
        }
        else if(GetDistanceToTarget() > approachRange) {
            FighterState = State.Navigating;
        }

        /*
        // update our manual velocity
        DoManualVelocityAdjustment();

        // look at our target
        AimTowards(CurrentTarget.transform.position);

        // move towards our target
        characterController.Move(manualVelocity * Time.deltaTime);
        */
        
        if (GetDistanceToTarget() < attackRange) {
            FighterState = State.Approaching;
        }

    }
    void DoStateAttacking() {

        // if our target is invalid go back to navigating
        if (CurrentTarget == null) {
            FighterState = State.Navigating;
            return;
        }

        // if it's time to attack...
        if (nextAttackTime < Time.time) {
            
            // perform an attack
            DoAttack();
        }

        // look at our target
        AimTowards(CurrentTarget.transform.position);

        // if we are too far from our target to attack,
        if (GetDistanceToTarget() > attackRange) {

            // go to the approach state
            FighterState = State.Approaching;
            return;
        }
    }
    void DoAttack() {
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackDelay;

        StartCoroutine(DealDamageDelayed(CurrentTarget, attackDamage, attackApplicationDelay));
    }
    void DoManualVelocityAdjustment() {
        Vector3 direction = (CurrentTarget.transform.position - transform.position).normalized;

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
        
        Target newTarget = FindTarget(CurrentTarget);
        if (newTarget != null) {
            if (newTarget != CurrentTarget) {

                CurrentTarget = newTarget;
            }
            
            navMeshAgent.destination = CurrentTarget.transform.position;
        }
    }
    protected abstract Target FindTarget(Target CurrentTarget);
    void AimTowards(Vector3 targetPoint) {
        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
    }
    float GetDistanceToTarget() {
        return Vector3.Distance(CurrentTarget.transform.position, transform.position);
    }

    IEnumerator DealDamageDelayed(Target target, int Damage, float Delay) {
        yield return new WaitForSeconds(Delay);

        if(target != null) {
            target.healthController.DealDamage(Damage);
        }
    }
}

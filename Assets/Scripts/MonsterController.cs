using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : FighterController
{
    protected override Target FindTarget(Target currentTarget, float currentInterestLevel) {
        return TargetSystem.Instance.GetMonsterTarget(transform.position, currentTarget as MonsterTarget, currentInterestLevel);
    }
    /// <summary>
    /// the current weapon type
    /// 0 = sword, 1 = club
    /// </summary>
    public int weaponType;

    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        try {
            anim = GetComponent<Animator>();
            weaponType = Mathf.Clamp(weaponType, 0, 1);
            anim.SetInteger("WeaponType", weaponType);
            SetWeapon();
        } catch {
            Debug.LogError("No Animator componenet attatched to " + name + ", deleted object.");
            Destroy(gameObject);
        }

    }

    private void Update() {
        // animations
        if(characterController.velocity.magnitude >= 0.125f)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
        // Rotation
        if(characterController.velocity.magnitude > 0.125f)
            transform.rotation = Quaternion.LookRotation(characterController.velocity) * Quaternion.Euler(0, -90, 0);

        
    }
    
    
    void DoAttack() {
        anim.SetTrigger("Attack");
        currentTarget.healthController.DealDamage(attackDamage);
        nextAttackTime = Time.time + attackDelay;
    }
   

    private void SetWeapon() {
        switch(weaponType) {
            case 0:
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                transform.GetChild(1).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}

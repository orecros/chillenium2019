using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour {

    private CharacterController characterController;

    private Animator anim;
    private int weaponType;

    private void Awake() {
        anim = GetComponent<Animator>();
        weaponType = Mathf.Clamp(weaponType, 0, 1);
        SetWeapon();
        anim.SetInteger("WeaponType", weaponType);

        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        // Animations
        if(characterController.velocity.magnitude >= 0.125f)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
        // Rotation
        if(characterController.velocity.magnitude > 0.125f)
            transform.rotation = Quaternion.LookRotation(characterController.velocity) * Quaternion.Euler(0, -90, 0);
    }

    private void Attack() {
        anim.SetTrigger("Attack");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAnimationController : MonoBehaviour
{
    public HealthController HouseHealthController;
    Animator animator;

    public int CriticalHealthThreshold;

    private void Awake() {
        HouseHealthController.OnHealthChanged.AddListener(OnHealthChanged);
        animator = GetComponent<Animator>();
    }

    public void OnHealthChanged() {
        int health = HouseHealthController.Health;

        if(health == 0) {
            animator.SetInteger("HealthState", 0);
            print(0);
        }
        else if( health <= CriticalHealthThreshold) {
            animator.SetInteger("HealthState", 1);
            print(1);
        }
        else if( health < HouseHealthController.MaxHealth) {
            animator.SetInteger("HealthState", 2);
            print(2);
        }
        else {
            animator.SetInteger("HealthState", 3);
            print(3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth;
    [SerializeField] int Health;

    public void DealDamage(int damage) {
        Health -= damage;
        
        if(Health <= 0) {
            DoDeath();
        }
    }

    public void HealDamage(int damage) {
        Health = Mathf.Min(Health + damage, MaxHealth);
    }

    public void DoDeath() {

    }
    
    public bool AtMax() {
        return MaxHealth == Health;
    }

}

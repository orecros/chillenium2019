using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent OnDeath;
    [HideInInspector] public UnityEvent OnHealthChanged;

    public int MaxHealth;
    [SerializeField] int health;
    [HideInInspector] public int Health {
        get { return health; }
        set {
            health = value;
            OnHealthChanged.Invoke();
        }
    }

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
        OnDeath.Invoke();
    }
    
    public bool AtMax() {
        return MaxHealth == Health;
    }

    public bool IsDead() {
        return Health <= 0;
    }

    public int DamageAmount() {
        return MaxHealth - Health;
    }
}

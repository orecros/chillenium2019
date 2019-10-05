using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInteract : Interactable {
    
    public static float healTime = 1f;

    private HealthController health;
    private bool beingHealed;

    protected override void Start() {
        base.Start();
        health = GetComponent<HealthController>();
    }

    protected virtual void Update() {
        if(health.AtMax() || beingHealed)
            canInteract = false;
        else
            canInteract = true;
    }

    public override void Interact(GameObject player, int playerNum) {
        StartCoroutine(InteractFreeze(player, playerNum));
    }

    private IEnumerator InteractFreeze(GameObject player, int playerNum) {
        Debug.Log("Healing...");
        beingHealed = true;
        yield return StartCoroutine(player.GetComponent<PlayerController>().FreezePlayer(healTime * health.DamageAmount()));

        health.HealDamage(health.MaxHealth);
        beingHealed = false;
        Debug.Log("Done healing.");
    }

}

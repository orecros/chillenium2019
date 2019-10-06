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

    protected override void LateUpdate() {
        base.LateUpdate();
        icon.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    public override void Interact(GameObject player, int playerNum) {
        StartCoroutine(InteractFreeze(player, playerNum));
    }

    private IEnumerator InteractFreeze(GameObject player, int playerNum) {
        //Debug.Log("Healing...");
        beingHealed = true;
        while(!health.AtMax()) {
            player.GetComponent<PlayerController>().SetAnimTrigger("Heal");
            yield return StartCoroutine(player.GetComponent<PlayerController>().FreezePlayer(healTime));

            health.HealDamage(1);
        }
        
        beingHealed = false;
        //Debug.Log("Done healing.");
    }

}

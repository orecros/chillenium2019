using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Interactable {

    public static float repairTime = 1.25f;
    public static int repairLoopCount = 2;

    private HealthController health;
    private bool repairing;

    protected override void Start() {
        base.Start();
        health = GetComponent<HealthController>();
    }

    protected virtual void Update() {
        if(health.AtMax() || repairing)
            canInteract = false;
        else
            canInteract = true;
    }

    public override void Interact() {
        base.Interact();
    }

    public override void Interact(GameObject player, int playerNum) {
        StartCoroutine(InteractFreeze(player, playerNum));
    }

    private IEnumerator InteractFreeze(GameObject player, int playerNum) {
        //Debug.Log("Repairing...");
        PlayerController pc = player.GetComponent<PlayerController>();
        repairing = true;
        pc.SwitchWeapon(true);
        for(int i = 0; i < repairLoopCount; i++) {
            pc.SetAnimTrigger("Repair");
            yield return StartCoroutine(pc.FreezePlayer(repairTime));
        }

        health.HealDamage(3);
        repairing = false;
        yield return null;
        if(!health.AtMax())
            player.GetComponent<PlayerController>().AddInteractable(gameObject);
        pc.SwitchWeapon(false);
        //Debug.Log("Done repairing.");
    }

}

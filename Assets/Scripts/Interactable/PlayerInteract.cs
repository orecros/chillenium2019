using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Interactable {

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
        //Debug.Log(name + " " + playerCount);
        icon.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + offset) + new Vector3(0, 12);
    }

    protected override void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && other.gameObject != gameObject)
            playerCount++;
    }

    protected override void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && other.gameObject != gameObject)
            playerCount--;
    }

    public override void Interact(GameObject player, int playerNum) {
        StartCoroutine(InteractFreeze(player, playerNum));
    }

    private IEnumerator InteractFreeze(GameObject player, int playerNum) {
        //Debug.Log("Healing...");
        beingHealed = true;
        while(!health.AtMax()) {
            player.GetComponent<PlayerController>().SetAnimTrigger("Heal");
            StartCoroutine(GetComponent<PlayerController>().FreezePlayer(healTime));
            yield return StartCoroutine(player.GetComponent<PlayerController>().FreezePlayer(healTime));

            health.HealDamage(5);
        }

        beingHealed = false;
        //Debug.Log("Done healing.");
    }
}

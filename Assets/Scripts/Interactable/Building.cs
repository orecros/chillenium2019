using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Interactable {

    public static float repairTime = 1.25f;
    public static int repairLoopCount = 1;

    public Sprite hammerIcon;

    private Sprite a_Icon;
    private HealthController health;
    private bool repairing;

    protected override void Start() {
        base.Start();
        health = GetComponent<HealthController>();

        a_Icon = icon.GetComponent<Image>().sprite;
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);
    }

    protected virtual void Update() {
        if(health.AtMax() || health.IsDead() || repairing)
            canInteract = false;
        else
            canInteract = true;
    }

    protected override void LateUpdate() {
        if(selected && playerCount > 0 && canInteract) {
            icon.GetComponent<Image>().sprite = a_Icon;
            icon.SetActive(true);
        } else if(!selected && !health.AtMax() && !health.IsDead()) {
            icon.GetComponent<Image>().sprite = hammerIcon;
            icon.SetActive(true);
        } else {
            icon.SetActive(false);
            selected = false;
        }
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

        if(!health.IsDead())
            health.HealDamage(10);
        repairing = false;
        yield return null;
        if(!health.AtMax())
            player.GetComponent<PlayerController>().AddInteractable(gameObject);
        pc.SwitchWeapon(false);
        //Debug.Log("Done repairing.");
    }

}

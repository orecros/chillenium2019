﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class VillagerInteract : Interactable {
    
    public static float healTime = 1f;

    public GameObject healthBarPrefab;
    public Sprite[] healthBars = new Sprite[3];

    private Image healthBar;
    private HealthController health;
    private bool beingHealed;

    protected override void Start() {
        base.Start();
        health = GetComponent<HealthController>();
        healthBar = Instantiate(healthBarPrefab, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), Quaternion.identity, GlobalCanvas.canvas.transform).GetComponent<Image>();
    }

    protected virtual void Update() {
        if(health.AtMax() || beingHealed)
            canInteract = false;
        else
            canInteract = true;
    }

    protected override void LateUpdate() {
        base.LateUpdate();
        icon.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + offset) + new Vector3(0, 12);

        healthBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f));
        switch(health.Health) {
            case 3:
                healthBar.sprite = healthBars[2];
                break;
            case 2:
                healthBar.sprite = healthBars[1];
                break;
            case 1:
                healthBar.sprite = healthBars[0];
                break;
            default:
                Destroy(healthBar.gameObject);
                break;
        }
    }

    public override void Interact(GameObject player, int playerNum) {
        StartCoroutine(InteractFreeze(player, playerNum));
    }

    private IEnumerator InteractFreeze(GameObject player, int playerNum) {
        //Debug.Log("Healing...");
        beingHealed = true;
        GetComponent<NavMeshAgent>().isStopped = true;
        while(!health.AtMax()) {
            player.GetComponent<PlayerController>().SetAnimTrigger("Heal");
            yield return StartCoroutine(player.GetComponent<PlayerController>().FreezePlayer(healTime));

            health.HealDamage(1);
        }

        GetComponent<NavMeshAgent>().isStopped = false;
        beingHealed = false;
        //Debug.Log("Done healing.");
    }

    public void DestroyHealthBar() {
        Destroy(healthBar.gameObject);
    }

}

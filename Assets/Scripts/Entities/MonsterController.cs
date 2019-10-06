using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : FighterController {

    public GameObject healthBarPrefab;
    public Sprite[] healthBars = new Sprite[2];
    private Image healthBar;
    private HealthController health;

    protected void Start() {
        // Health Bar
        health = GetComponent<HealthController>();
        healthBar = Instantiate(healthBarPrefab, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), Quaternion.identity, GlobalCanvas.canvas.transform).GetComponent<Image>();
    }

    protected void LateUpdate() {
        // Health Bar
        healthBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f));
        switch(health.Health) {
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

    protected override Target FindTarget(Target currentTarget, float currentInterestLevel) {
        return TargetSystem.Instance.GetMonsterTarget(transform.position, currentTarget as MonsterTarget, currentInterestLevel);
    }

    public void DestroyHealthBar() {
        Destroy(healthBar.gameObject);
    }

    public void RemoveMonster() {
        MonsterSpawner.monsterList.Remove(gameObject);
    }
}

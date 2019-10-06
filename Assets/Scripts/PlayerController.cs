using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed = 0.15f;

    public GameObject healthBarPrefab;
    public Sprite[] healthBars = new Sprite[3];
    private Image healthBar;
    private HealthController health;

    private int playerNum;

    private CharacterController controller;
    private Animator anim;
    private float moveX, moveZ, quitTimer;
    private Vector3 moveDir;

    private List<GameObject> interactInRange = new List<GameObject>();
    private GameObject currentInteract;
    //private bool interactBuffer;
    [HideInInspector] public bool canAct;

    public GameObject syringe, hammer;

    private void Awake() {
        // Set variables
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        canAct = true;

        // Get player number
        if(!PlayerManager.player1) {
            playerNum = 1;
            PlayerManager.player1 = true;
        } else if(!PlayerManager.player2) {
            playerNum = 2;
            PlayerManager.player2 = true;
        } else if(!PlayerManager.player3) {
            playerNum = 3;
            PlayerManager.player3 = true;
        } else {
            Debug.LogError("Attempting to go over player limit! Deleting newly added player object.");
            Destroy(gameObject);
        }
        if(PlayerManager.playerCount == 0)
            PlayerManager.playerCount++;
    }

    private void Start() {
        // Health Bar
        health = GetComponent<HealthController>();
        healthBar = Instantiate(healthBarPrefab, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), Quaternion.identity, GlobalCanvas.canvas.transform).GetComponent<Image>();
    }

    private void Update() {
        // Interact
        if(canAct && Input.GetButtonDown("Act" + playerNum) && currentInteract != null) {
            currentInteract.GetComponent<Interactable>().Interact(gameObject, playerNum);
        }

        // Remove objects that you can't interact with
        for(int i = 0; i < interactInRange.Count; i++)
            if(interactInRange[i] == null || !interactInRange[i].GetComponent<Interactable>().canInteract)
                interactInRange.RemoveAt(i);

        // Animations
        if(controller.velocity.magnitude > 0.125f)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
        // Rotation
        if(moveDir.magnitude > 0.125f)
            transform.rotation = Quaternion.LookRotation(moveDir) * Quaternion.Euler(0, -90, 0);
    }

    private void FixedUpdate() {
        if(canAct) {
            moveX = Input.GetAxis("Horizontal" + playerNum);
            moveZ = Input.GetAxis("Vertical" + playerNum);
            moveDir = new Vector3(moveX, 0, moveZ);
            controller.SimpleMove(moveDir * moveSpeed / Time.fixedDeltaTime);
        }

        //transform.rotation = Quaternion.Euler(moveX * 360f, 0, moveZ * 360f);
    }

    private void LateUpdate() {
        // Player quit
        if(PlayerManager.playerCount > 1) {
            if(playerNum == 1 && Input.GetButton("Exit1")) { // Player 1
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player1 = false;
                    VillagerSpawner.vs.DeleteOnLeave();
                    Destroy(gameObject);
                }
            } else if(playerNum == 2 && Input.GetButton("Exit2")) { // Player 2
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player2 = false;
                    VillagerSpawner.vs.DeleteOnLeave();
                    Destroy(gameObject);
                }
            } else if(playerNum == 3 && Input.GetButton("Exit3")) { // Player 3
                quitTimer += Time.deltaTime;
                if(quitTimer > 1) {
                    PlayerManager.player3 = false;
                    VillagerSpawner.vs.DeleteOnLeave();
                    Destroy(gameObject);
                }
            }
        }
        
        // Interacting
        if(interactInRange.Count > 0 && canAct) {
            currentInteract = FindClosestInteractable();
            foreach(GameObject obj in interactInRange) {
                if(obj == currentInteract)
                    obj.GetComponent<Interactable>().selected = true;
                else if(obj.GetComponent<Interactable>().playerCount == 1)
                    obj.GetComponent<Interactable>().selected = false;
            }
        } else
            currentInteract = null;

        // Health Bar
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

    private void OnTriggerEnter(Collider other) {
        if((other.CompareTag("Interactable") || other.CompareTag("Villager") || (other.CompareTag("Player") && other.gameObject != gameObject)) && other.GetComponent<Interactable>().canInteract) {
            interactInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Interactable") && !other.GetComponent<HealthController>().AtMax() && !interactInRange.Contains(other.gameObject))
            interactInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Interactable") || other.CompareTag("Villager") || (other.CompareTag("Player") && other.gameObject != gameObject)) {
            interactInRange.Remove(other.gameObject);
        }
    }

    private void OnDestroy() {
        if(healthBar != null)
            Destroy(healthBar.gameObject);
        if(playerNum > 0)
            PlayerManager.playerCount--;
    }
    

    /// <summary>
    /// Finds and returns the closest interactable object.
    /// </summary>
    /// <returns>Returns closest interactable object. 
    /// Returns null if no interactable objects are nearby.</returns>
    private GameObject FindClosestInteractable() {
        if(interactInRange.Count == 0)
            return null;

        GameObject current = interactInRange[0];
        float distance = Mathf.Infinity;
        foreach(GameObject obj in interactInRange) {
            if(obj == null) {
                interactInRange.Remove(obj);
                return null;
            }

            if(Vector3.Distance(transform.position, obj.transform.position) < distance) {
                current = obj;
                distance = Vector3.Distance(transform.position, obj.transform.position);
            }
        }
        return current;
    }

    public IEnumerator FreezePlayer(float time) {
        canAct = false;
        yield return new WaitForSeconds(time);
        canAct = true;
    }

    public bool AddInteractable(GameObject obj) {
        if(!interactInRange.Contains(obj)) {
            interactInRange.Add(obj);
            return true;
        }
        return false;
    }

    public void SetAnimTrigger(string trigger) {
        anim.SetTrigger(trigger);
    }

    public void SwitchWeapon(bool setHammer) {
        if(setHammer) {
            syringe.SetActive(false);
            hammer.SetActive(true);
        } else {
            syringe.SetActive(true);
            hammer.SetActive(false);
        }
    }

}

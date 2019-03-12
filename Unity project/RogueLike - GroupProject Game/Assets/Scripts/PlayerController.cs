using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;
    /// <summary>
    /// -1f means no ability is active(a.k.a basic attack) then from 0 to 1 (the step is 0.2) there are all the other abilities
    /// This is used by the animator to decide which animation to play
    /// </summary>
    public float activeAbility = -1f;

    public List<Ability> abilities;
    private Ability m_activeAbility;

    public Stats playerStats;
    public float maxHp;
    public float currentHp;

    public Animator anim;

    public GameObject chestPrefab;
    public GameObject chestLoot;
    private bool isShowing;
    private bool abilityToggle = true;

    private void Awake()
    {
        //Initialize player's stats with default values
        playerStats = new Stats(10, 10, 10);
        maxHp = playerStats.vitality + 200f;
        currentHp = maxHp;
        //Create abilities
        abilities = new List<Ability>();
        Ability ability = new Ability(0.5f, 2f, 5f);
        Ability ability2 = new Ability(0.5f, 2f, 5f);
        Ability ability3 = new Ability(0.5f, 2f, 5f);
        Ability ability4 = new Ability(0.5f, 2f, 5f);
        Ability ability5 = new Ability(0.5f, 2f, 5f);
        Ability ability6 = new Ability(0.5f, 2f, 5f);
        abilities.Add(ability);
        abilities.Add(ability2);
        abilities.Add(ability3);
        abilities.Add(ability4);
        abilities.Add(ability5);
        abilities.Add(ability6);
        //m_activeAbility = abilities[0];
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast((ray), out hit))
            {
                if (hit.collider.gameObject.tag == "Exit")
                {
                    StartCoroutine(MoveTo(hit.collider.gameObject.transform.position, hit.collider.gameObject, ChangeRoomAction, 0));
                }
                else if (hit.collider.gameObject.tag == "Enemy")
                {
                    StartCoroutine(MoveTo(hit.collider.gameObject.transform.position, hit.collider.gameObject, AttackEnemy, 1));
                }
                else if (hit.collider.gameObject.tag == "Chest")
                {
                    StartCoroutine(MoveTo(hit.collider.gameObject.transform.position, hit.collider.gameObject, OpenChest, 2));
                    isShowing = !isShowing;
                    chestLoot.SetActive(isShowing);
                }
            }
        }
    }

    IEnumerator MoveTo(Vector3 targetPos, GameObject target, UnityAction<GameObject> action, int modifier)
    {
        isMoving = true;
        while (transform.position.x != targetPos.x || transform.position.z != targetPos.z - modifier)
        {
            if (transform.position.x != targetPos.x)
            {
                if ((targetPos - transform.position).x < 0)
                {
                    transform.position += new Vector3(-1f, 0f, 0f);
                }
                else
                {
                    transform.position += new Vector3(1f, 0f, 0f);
                }

            }
            else if (transform.position.z != targetPos.z - modifier)
            {
                if (targetPos.z + modifier == transform.position.z)
                {
                    break;
                }
                if ((targetPos - transform.position).z < modifier)
                {
                    transform.position += new Vector3(0f, 0f, -1f);
                }
                else
                {
                    transform.position += new Vector3(0f, 0f, 1f);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        isMoving = false;
        action(target);
    }

    public void AttackEnemy(GameObject enemy)
    {
        if (activeAbility == -1f)
        {
            if (enemy.GetComponent<EnemyController>().self.currentHp > 0)
            {
                enemy.GetComponent<EnemyController>().self.currentHp -= playerStats.strength;
                if (abilityToggle)
                {
                    AttackEnemy(enemy);
                }
            }
            else if (enemy.GetComponent<EnemyController>().self.currentHp <= 0)
            {
                GameManager.instance.currentRoom.EnemyAlive[enemy] = false;
                enemy.SetActive(false);
                GameManager.instance.currentRoom.DropChest();
            }
        }
        else
        {
            StartCoroutine(CastAbility(enemy));
        }
    }
    
    public void ChangeRoomAction(GameObject exit)
    {
        GameManager.instance.currentRoom.ChangeRoom(exit, this.gameObject);
    }

    public void OpenChest(GameObject chest)
    {
        //Do something to open the chest
    }

    IEnumerator CastAbility(GameObject enemy)
    {
        while (enemy.GetComponent<EnemyController>().self.currentHp > 0f && abilityToggle)
        {
            anim.gameObject.transform.position = enemy.transform.position;
            anim.SetBool("UseAbility", true);
            enemy.GetComponent<EnemyController>().self.currentHp -= playerStats.strength + m_activeAbility.damage;
            yield return new WaitForSecondsRealtime(m_activeAbility.cooldown + anim.GetCurrentAnimatorStateInfo(0).length);
        }
        anim.gameObject.transform.localPosition = Vector3.zero;
        if (enemy.GetComponent<EnemyController>().self.currentHp <= 0f)
        {
            GameManager.instance.currentRoom.EnemyAlive[enemy] = false;
            enemy.SetActive(false);
            GameManager.instance.currentRoom.DropChest();
        }
    }

    public void ChangeActiveAbility()
    {

    }

    public void SwapAbilityAndBody(EnemyController enemy)
    {
        currentHp = maxHp;
        m_activeAbility = enemy.self.ability;
        //Hard coded value will need change
        activeAbility = 0f;
    }
}

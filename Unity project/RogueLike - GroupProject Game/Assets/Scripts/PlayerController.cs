﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Dictionary<Element, int> enemiesKilledByAbility;
    public bool toggleAttack = true;

    public bool isMoving = false;
    /// <summary>
    /// -1f means no ability is active(a.k.a basic attack) then from 0 to 1 (the step is 0.2) there are all the other abilities
    /// This is used by the animator to decide which animation to play
    /// </summary>
    public float activeAbility = 0.2f;

    public List<Ability> abilities;
    private Ability m_activeAbility;

    public Stats playerStats;
    public float maxHp;
    public float currentHp;
    public Image Healthbar;

    public Animator anim;

    public GameObject chestPrefab;
    public GameObject chestLoot;
    private bool isShowing;
    private bool abilityToggle = true;
    public Button ToggleAttack;

    [SerializeField]
    private float currency = 0f;
    private int chestCount = 0;
    public Text CurrencyText;

    public void Start()
    {
        //ToggleAttack.onClick.AddListener(ButtonClicked);
        currentHp = maxHp;
        enemiesKilledByAbility = new Dictionary<Element, int>();

        enemiesKilledByAbility.Add(Element.Fire, 0);
        enemiesKilledByAbility.Add(Element.Water, 0);
        enemiesKilledByAbility.Add(Element.Wind, 0);
        enemiesKilledByAbility.Add(Element.Earth, 0);
        enemiesKilledByAbility.Add(Element.Light, 0);
        enemiesKilledByAbility.Add(Element.Dark, 0);
    }

    private void Awake()
    {
        Healthbar = GameObject.FindGameObjectWithTag("Health").GetComponentInChildren<Image>();
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
        m_activeAbility = abilities[0];
        GameManager.instance.player = this;
    }

    public void ButtonClicked()
    {
        Debug.Log("button pressed");
    }

    void Update()
    {
        //MoveToClosestEnemyAI();
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
                else if (hit.collider.gameObject.tag == "Shop")
                {
                    StartCoroutine(MoveTo(hit.collider.transform.position, hit.collider.gameObject, OpenShop, 1));
                }
            }
        }
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        Healthbar.fillAmount = currentHp / maxHp;
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
            yield return new WaitForSeconds(0.03f);
        }

        isMoving = false;
        action(target);
    }

    public void AttackEnemy(GameObject enemy)
    {
        //if (activeAbility == -1f)
        //{
        //    if (enemy.GetComponent<EnemyController>().self.currentHp > 0)
        //    {
        //        enemy.GetComponent<EnemyController>().self.currentHp -= playerStats.strength;
        //        if (abilityToggle)
        //        {
        //            AttackEnemy(enemy);
        //        }
        //    }
        //    else if (enemy.GetComponent<EnemyController>().self.currentHp <= 0)
        //    {
        //        GameManager.instance.currentRoom.EnemyAlive[enemy] = false;
        //        enemy.SetActive(false);
        //        GameManager.instance.currentRoom.DropChest();
        //    }
        //}
        //else
        //{
            StartCoroutine(CastAbility(enemy));
        //}
    }
    
    public void ChangeRoomAction(GameObject exit)
    {
        GameManager.instance.currentRoom.ChangeRoom(exit, this.gameObject);
    }

    public void OpenChest(GameObject chest)
    {
        if (!GameManager.instance.currentRoom.isChestOpened)
        {
            chestCount++;
            currency += chestCount * UnityEngine.Random.Range(150f, 500f) + UnityEngine.Random.Range(1f, 99f);
            GameManager.instance.currentRoom.isChestOpened = true;
        }
    }

    IEnumerator CastAbility(GameObject enemy)
    {
        while (enemy.GetComponent<EnemyController>().self.currentHp > 0f)
        {
            if (!toggleAttack)
            {
                break;
            }
            anim.gameObject.transform.position = enemy.transform.position + new Vector3(0, 1, 0);
            anim.SetBool("UseAbility", true);
            anim.SetFloat("AbilityType", activeAbility);
            enemy.GetComponent<EnemyController>().self.currentHp -= playerStats.strength + m_activeAbility.damage + 10f;
            yield return new WaitForSecondsRealtime(m_activeAbility.cooldown + anim.GetCurrentAnimatorStateInfo(0).length);
        }
        anim.gameObject.transform.localPosition = Vector3.zero;
        if (enemy.GetComponent<EnemyController>().self.currentHp <= 0f)
        {
            if (activeAbility == 0f)
            {
                enemiesKilledByAbility[Element.Water] += 100;
            }
            else if (activeAbility == 0.2f)
            {
                enemiesKilledByAbility[Element.Dark]++;
            }
            else if (activeAbility == 0.4f)
            {
                enemiesKilledByAbility[Element.Light]++;
            }
            else if (activeAbility == 0.6f)
            {
                enemiesKilledByAbility[Element.Fire]++;
            }
            else if (activeAbility == 0.8f)
            {
                enemiesKilledByAbility[Element.Wind]++;
            }
            else if (activeAbility == 1f)
            {
                enemiesKilledByAbility[Element.Earth]++;
            }
            GameManager.instance.currentRoom.EnemyAlive[enemy] = false;
            enemy.SetActive(false);
            GameManager.instance.currentRoom.DropChest();
        }
    }

    public void OpenShop(GameObject shop)
    {
        UIManager.instance.shopReference.SetActive(true);
        UpdateShopText();
    }

    public void UpdateShopText()
    {
        UIManager.instance.strText.text = (Shop.strPriceMultiplier * 1500 + UnityEngine.Random.Range(1f, 99f)).ToString();
        UIManager.instance.vitText.text = (Shop.vitPriceMultiplier * 1500 + UnityEngine.Random.Range(1f, 99f)).ToString();
        UIManager.instance.defText.text = (Shop.defPriceMultiplier * 1500 + UnityEngine.Random.Range(1f, 99f)).ToString();
    }

    public void ChangeActiveAbility()
    {

    }

    public void SwapAbilityAndBody(EnemyController enemy)
    {
        currentHp = maxHp;
        m_activeAbility = enemy.self.ability;
        //Hard coded value will need change
        activeAbility = enemy.activeAbility;
    }

    private void MoveToClosestEnemyAI()
    {
        if (isMoving || !GameManager.instance.currentRoom.EnemyAlive.ContainsValue(true))
        {
            return;
        }
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;
        foreach (KeyValuePair<GameObject, bool> pair in GameManager.instance.currentRoom.EnemyAlive)
        {
            if (pair.Value)
            {
                if (closestDistance > Vector3.Distance(transform.position, pair.Key.transform.position))
                {
                    closestDistance = Vector3.Distance(transform.position, pair.Key.transform.position);
                    closestEnemy = pair.Key;
                }
            }
        }
        StartCoroutine(MoveTo(closestEnemy.transform.position, closestEnemy, AttackEnemy, 1));
    }

    public bool BuyStats(string statName)
    {
        float price;
        if (statName == "Str")
        {
            float.TryParse(UIManager.instance.strText.text, out price);
            price *= Shop.strPriceMultiplier;

            if (currency >= price)
            {
                currency -= price;
                playerStats.SetStr(playerStats.strength + 1);
                Debug.Log(playerStats.strength);
                UpdateCurrency();
                return true;
            }
        }
        else if (statName == "Vit")
        {
            float.TryParse(UIManager.instance.vitText.text, out price);
            price *= Shop.vitPriceMultiplier;
            Debug.Log(price);
            if (currency >= price)
            {
                currency -= price;
                playerStats.SetVit(playerStats.vitality + 1);
                Debug.Log(playerStats.vitality);
                UpdateCurrency();
                return true;
            }
        }
        else if (statName == "Def")
        {
            float.TryParse(UIManager.instance.defText.text, out price);
            price *= Shop.defPriceMultiplier;

            if (currency >= price)
            {
                currency -= price;
                playerStats.SetDef(playerStats.defense + 1);
                Debug.Log(playerStats.defense);
                UpdateCurrency();
                return true;
            }
        }
        return false;
    }

    public void AddCurrency(int NewCurrencyValue)
    {
        currency += NewCurrencyValue;
        UpdateCurrency();
    }
    void UpdateCurrency()
    {
        CurrencyText.text = currency.ToString();
    }
}

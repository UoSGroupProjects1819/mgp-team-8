using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;

    public Stats playerStats;
    public Animator anim;

    public GameObject chestPrefab;
    public GameObject chestLoot;
    private bool isShowing;

    private void Awake()
    {
        //Initialize player's stats with default values
        playerStats = new Stats(10, 10, 10);
        
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
                    StartCoroutine(MoveToExit(hit.collider.gameObject.transform.position, hit.collider.gameObject));
                }
                else if (hit.collider.gameObject.tag == "Enemy")
                {
                    StartCoroutine(MoveToEnemy(hit.collider.gameObject.transform.position, hit.collider.gameObject));
                }
                else if (hit.collider.gameObject.tag == "Chest")
                {
                    StartCoroutine(MoveToChest(hit.collider.gameObject.transform.position, hit.collider.gameObject));
                    isShowing = !isShowing;
                    chestLoot.SetActive(isShowing);
                }
            }
        }
    }
    
    IEnumerator MoveToExit(Vector3 targetPos, GameObject exit)
    {
        //Vector3 movementVector = Vector3.MoveTowards(GameManager.instance.currentRoom.grid.CellToWorld(myPos), GameManager.instance.currentRoom.grid.CellToWorld(targetPos), 1f);
        isMoving = true;
        while (transform.position.x != targetPos.x || transform.position.z != targetPos.z)
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
            else if (transform.position.z != targetPos.z)
            {
                if ((targetPos - transform.position).z < 0)
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
        GameManager.instance.currentRoom.ChangeRoom(exit, this.gameObject);
    }

    IEnumerator MoveToEnemy(Vector3 targetPos, GameObject enemy)
    {
        isMoving = true;
        while (transform.position.x != targetPos.x || transform.position.z != targetPos.z - 1)
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
            else if (transform.position.z != targetPos.z - 1)
            {
                if ((targetPos - transform.position).z < 1)
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
        //Changed condition so the enemy actually dies
        while (enemy.activeSelf)
        {
            AttackEnemy(enemy);
        }
    }

    public void AttackEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyController>().self.currentHp > 0)
        {
            enemy.GetComponent<EnemyController>().self.currentHp -= playerStats.strength;
        }else if (enemy.GetComponent<EnemyController>().self.currentHp <= 0)
        {
            GameManager.instance.currentRoom.EnemyAlive[enemy] = false;
            enemy.SetActive(false);
        }
    }

    IEnumerator MoveToChest(Vector3 targetPos, GameObject chest)
    {
        isMoving = true;
        while (transform.position.x != targetPos.x || transform.position.z != targetPos.z - 2)
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
            else if (transform.position.z != targetPos.z - 2)
            {
                if ((targetPos - transform.position).z < 2)
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
    }
}

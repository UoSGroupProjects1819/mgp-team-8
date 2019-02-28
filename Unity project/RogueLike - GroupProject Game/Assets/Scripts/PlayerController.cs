using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;

    public Stats playerStats;

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
                    //StartCoroutine(Move(GameManager.instance.currentRoom.grid.WorldToCell(transform.position), GameManager.instance.currentRoom.grid.WorldToCell(hit.collider.gameObject.transform.position)));
                }
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    StartCoroutine(MoveToEnemy(hit.collider.gameObject.transform.position, hit.collider.gameObject));
                }
            }
        }
    }

    IEnumerator Move(Vector3Int myPos, Vector3Int targetPos)
    {
        Vector3 movementVector = Vector3.MoveTowards(GameManager.instance.currentRoom.grid.CellToWorld(myPos), GameManager.instance.currentRoom.grid.CellToWorld(targetPos), 1f);
        isMoving = true;
        while (myPos.x != targetPos.x || myPos.z != targetPos.z)
        {
            if (myPos.x != targetPos.x)
            {
                movementVector.x = transform.position.x;
            }
            else if (myPos.z != targetPos.z)
            {
                movementVector.z = transform.position.z;
            }
            transform.position = movementVector - new Vector3(0f, movementVector.y - 1f, 0f);
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            myPos = GameManager.instance.currentRoom.grid.WorldToCell(transform.position);
            yield return new WaitForSeconds(0.01f);
            movementVector = Vector3.MoveTowards(movementVector, GameManager.instance.currentRoom.grid.CellToWorld(targetPos), 1f);
        }

        isMoving = false;
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
        //Vector3 movementVector = Vector3.MoveTowards(GameManager.instance.currentRoom.grid.CellToWorld(myPos), GameManager.instance.currentRoom.grid.CellToWorld(targetPos), 1f);
        isMoving = true;
        while (transform.position.x != targetPos.x || transform.position.z != targetPos.z-1)
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
            else if (transform.position.z != targetPos.z-1)
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
        GameManager.instance.currentRoom.ChangeRoom(enemy, this.gameObject);

        
    }
}

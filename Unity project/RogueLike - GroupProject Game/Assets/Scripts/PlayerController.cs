using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Room currentRoom;
    private bool isMoving = false;

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
                    StartCoroutine(Move(currentRoom.grid.WorldToCell(transform.position), currentRoom.grid.WorldToCell(hit.collider.gameObject.transform.position)));
                }
            }
        }
    }

    IEnumerator Move(Vector3Int myPos, Vector3Int targetPos)
    {
        Vector3 movementVector = Vector3.MoveTowards(currentRoom.grid.CellToWorld(myPos), currentRoom.grid.CellToWorld(targetPos), 1f);
        isMoving = true;
        while (myPos.x != targetPos.x || myPos.y != targetPos.y)
        {
            if (myPos.x == targetPos.x)
            {
                movementVector.x = transform.position.x;
            }
            else
            {
                movementVector.z = transform.position.z;
            }
            transform.position = movementVector - new Vector3(0f, movementVector.y - 1f, 0f);
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            myPos = currentRoom.grid.WorldToCell(transform.position);
            yield return new WaitForSeconds(0.01f);
            movementVector = Vector3.MoveTowards(movementVector, currentRoom.grid.CellToWorld(targetPos), 1f);
        }

        isMoving = false;
    }
}

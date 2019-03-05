using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum States
    {
        Idle, Move
    }
    public Enemy self;

    public Vector2Int currentPosition;
    public States currentState = States.Idle;
    int moveCount = 0;
    private void FixedUpdate()
    {
        if (currentState == States.Idle && moveCount >= Random.Range(25, 51))
        {
            currentState = States.Move;
            MoveState();
            moveCount = 0;
        }
        moveCount++;
    }
    private void MoveState()
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        positions.Add(new Vector2Int(currentPosition.x, currentPosition.y - 1));
        positions.Add(new Vector2Int(currentPosition.x, currentPosition.y + 1));
        positions.Add(new Vector2Int(currentPosition.x - 1, currentPosition.y));
        positions.Add(new Vector2Int(currentPosition.x + 1, currentPosition.y));
        List<Vector2Int> validPositions = new List<Vector2Int>();
        foreach (var position in positions)
        {
            if (GameManager.instance.currentRoom.validTiles[position])
            {
                validPositions.Add(position);
            }
        }
        currentPosition = validPositions[Random.Range(0, validPositions.Count)];
        transform.position = GameManager.instance.currentRoom.tiles[currentPosition.x][currentPosition.y].transform.position + new Vector3(0f, 1f, 0f);
        currentState = States.Idle;
    }
}

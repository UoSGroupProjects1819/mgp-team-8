using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum States
    {
        Idle, Move, Attack
    }
    public Enemy self;
    public Animator anim;

    public Vector2Int currentPosition;
    public States currentState = States.Idle;
    int moveCount = 0;
    public bool inCombat = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (currentState == States.Idle && moveCount >= Random.Range(25, 51))
        {
            currentState = States.Move;
            MoveState();
            moveCount = 0;
        }
        else if (currentState != States.Attack && IsPlayerInRange())
        {
            currentState = States.Attack;
            StartCoroutine(AttackState());
        }
        moveCount++;
    }

    private bool IsPlayerInRange()
    {
        if (player == null)
        {
            Debug.LogError("Player doesn't exist in the current scene!");
            return false;
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= self.ability.range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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

    IEnumerator AttackState()
    {
        while (player.GetComponent<PlayerController>().currentHp > 0f && IsPlayerInRange())
        {
            anim.gameObject.transform.position = player.transform.position;
            anim.SetBool("UseAbility", true);
            player.GetComponent<PlayerController>().currentHp -= self.stats.strength + self.ability.damage;
            yield return new WaitForSecondsRealtime(self.ability.cooldown + anim.GetCurrentAnimatorStateInfo(0).length);
        }
        anim.gameObject.transform.localPosition = Vector3.zero;
        currentState = States.Idle;
        if (player.GetComponent<PlayerController>().currentHp <= 0f)
        {
            player.GetComponent<PlayerController>().SwapAbilityAndBody(this);
        }
    }
}

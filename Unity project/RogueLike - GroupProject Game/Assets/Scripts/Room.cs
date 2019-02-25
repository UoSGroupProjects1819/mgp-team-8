using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Idea about room navigation keeping track of each room's neighbor and direction
    public Element biome;

    public Dictionary<GameObject, Room> roomsByExit;

    public Grid grid { get; private set; }
    public List<GameObject> exits;

    public GameObject[][] tiles;
    public Dictionary<Vector2Int, bool> validTiles;
    private List<GameObject> enemies;

    public bool isNeighbor { get; private set; } = false;
    [Header("Enemy prefabs")]
    public GameObject fireEnemyPrefab;
    public GameObject waterEnemyPrefab;
    public GameObject windEnemyPrefab;
    public GameObject earthEnemyPrefab;
    public GameObject lightEnemyPrefab;
    public GameObject darkEnemyPrefab;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        enemies = new List<GameObject>();
        validTiles = new Dictionary<Vector2Int, bool>();
        roomsByExit = new Dictionary<GameObject, Room>();
        exits = new List<GameObject>();
    }

    private void Start()
    {
        int enemyNumber = Random.Range(1, 7);
        for (int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemies();
        }
    }

    public void SetupTileArray(int width, int height)
    {
        tiles = new GameObject[width][];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i] = new GameObject[height];
            }
        }
    }

    public void SpawnEnemies()
    {
        Enemy enemyScript = new Enemy(10, 10, 10, biome);
        GameObject enemy = Instantiate(GetEnemyOfElement((int)biome), this.transform);
        enemy.GetComponent<EnemyController>().self = enemyScript;
        Vector3 enemyPos = SetEnemyPosition(enemy);
        enemy.transform.position = enemyPos;
        enemies.Add(enemy);
    }

    private GameObject GetEnemyOfElement(int index)
    {
        if (index == 0)
        {
            return fireEnemyPrefab;
        }
        else if (index == 1)
        {
            return waterEnemyPrefab;
        }
        else if (index == 2)
        {
            return windEnemyPrefab;
        }
        else if (index == 3)
        {
            return earthEnemyPrefab;
        }
        else if (index == 4)
        {
            return lightEnemyPrefab;
        }
        else if (index == 5)
        {
            return darkEnemyPrefab;
        }
        else
        {
            Debug.LogError("The selected element is not valid!");
            return null;
        }
    }

    private Vector3 SetEnemyPosition(GameObject enemy)
    {
        int x = Random.Range(0, tiles.Length);
        int y = Random.Range(0, tiles[0].Length);

        if (validTiles.ContainsKey(new Vector2Int(x, y)))
        {
            if (!validTiles[new Vector2Int(x, y)])
            {
                return SetEnemyPosition(enemy);
            }
            else
            {
                enemy.GetComponent<EnemyController>().currentPosition = new Vector2Int(x, y);
                return new Vector3(tiles[x][y].transform.position.x, 1f, tiles[x][y].transform.position.z);
            }
        }
        else
        {
            validTiles.Add(new Vector2Int(x, y), false);
            enemy.GetComponent<EnemyController>().currentPosition = new Vector2Int(x, y);
            return new Vector3(tiles[x][y].transform.position.x, 1f, tiles[x][y].transform.position.z);
        }
    }

    public void ChangeRoom(GameObject exit, GameObject player)
    {
        roomsByExit[exit].gameObject.SetActive(true);
        foreach (var pair in roomsByExit[exit].roomsByExit)
        {
            if (pair.Value == this)
            {
                player.transform.position = pair.Key.transform.position + new Vector3(0f, 1f, 0f);
            }
        }
        GameManager.instance.currentRoom = roomsByExit[exit];
        GameManager.instance.RoomChanged();
        this.gameObject.SetActive(false);
    }

    public void SetNeighbor(Room neighbor)
    {
        isNeighbor = true;
        if (neighbor != null)
        {
            foreach (var exit in exits)
            {
                if (!roomsByExit.ContainsKey(exit))
                {
                    roomsByExit.Add(exit, neighbor);
                    break;
                }
            }
        }
    }
}

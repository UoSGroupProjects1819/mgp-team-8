using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Idea about room navigation keeping track of each room's neighbor and direction
    public Element biome;

    public Grid grid { get; private set; }

    public GameObject[][] tiles;
    public Dictionary<Vector2Int, bool> validTiles;
    private List<GameObject> enemies;
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
        Vector3 enemyPos = SetEnemyPosition();
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

    private Vector3 SetEnemyPosition()
    {
        int x = Random.Range(0, tiles.Length);
        int y = Random.Range(0, tiles[0].Length);

        if (validTiles.ContainsKey(new Vector2Int(x, y)))
        {
            if (!validTiles[new Vector2Int(x, y)])
            {
                return SetEnemyPosition();
            }
            else
            {
                return new Vector3(tiles[x][y].transform.position.x, 1f, tiles[x][y].transform.position.z);
            }
        }
        else
        {
            validTiles.Add(new Vector2Int(x, y), false);
            return new Vector3(tiles[x][y].transform.position.x, 1f, tiles[x][y].transform.position.z);
        }
    }
}

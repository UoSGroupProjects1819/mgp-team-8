using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject roomPrefab;
    public GameObject playerPrefab;
    public GameObject exitPrefab;

    public Transform roomParent;

    //Keeps track of the rooms created
    public List<Room> rooms;

    private int roomWidth = 20;
    private int roomHeight = 20;

    void Start()
    {
        rooms = new List<Room>();
        for (int i = 0; i < 15; i++)
        {
            GameObject room = CreateRoom();
            if (i != 0)
            {
                room.SetActive(false);
            }
            else
            {
                GameManager.instance.currentRoom = room.GetComponent<Room>();
                GameObject player = Instantiate(playerPrefab);
                player.transform.position = room.GetComponent<Room>().tiles[1][1].transform.position + new Vector3(0f, 1f, 0f);
                room.GetComponent<Room>().validTiles[new Vector2Int(1, 1)] = false;
                room.GetComponent<Room>().SetNeighbor(null);
            }
        }
        //Get exits for the first room
        int count = 1;
        for (int i = 0; i < rooms[0].exits.Count; i++)
        {
            rooms[0].roomsByExit.Add(rooms[0].exits[i], rooms[count]);
            rooms[count].SetNeighbor(rooms[0]);
            count++;
        }
    }
    
    public GameObject CreateRoom()
    {
        GameObject room = Instantiate(roomPrefab, roomParent);
        room.GetComponent<Room>().SetupTileArray(roomWidth, roomHeight);
        //Select a random biome (Can be changed to specific rules to create better biomes)
        room.GetComponent<Room>().biome = (Element)Random.Range(0, 6);

        Vector3 currentPosition = Vector3.zero;
        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                if (isWall(i, j))
                {
                    if (i == roomWidth / 2 || j == roomHeight / 2)
                    {
                        GameObject tile = Instantiate(exitPrefab, room.transform);
                        tile.transform.localPosition = currentPosition;
                        currentPosition.x += 1f;
                        room.GetComponent<Room>().tiles[i][j] = tile;
                        room.GetComponent<Room>().exits.Add(tile);
                        if (!room.GetComponent<Room>().validTiles.ContainsKey(new Vector2Int(i, j)))
                        {
                            room.GetComponent<Room>().validTiles.Add(new Vector2Int(i, j), false);
                        }
                    }
                    else
                    {
                        GameObject tile = Instantiate(wallTile, room.transform);
                        tile.tag = "Wall";
                        tile.GetComponent<Renderer>().material.color = Color.red;
                        tile.transform.localPosition = currentPosition;
                        currentPosition.x += 1f;
                        room.GetComponent<Room>().tiles[i][j] = tile;
                        if (!room.GetComponent<Room>().validTiles.ContainsKey(new Vector2Int(i, j)))
                        {
                            room.GetComponent<Room>().validTiles.Add(new Vector2Int(i, j), false);
                        }
                    }
                }
                else
                {
                    GameObject tile = Instantiate(floorTile, room.transform);
                    tile.tag = "Floor";
                    tile.transform.localPosition = currentPosition;
                    currentPosition.x += 1f;
                    room.GetComponent<Room>().tiles[i][j] = tile;
                    if (!room.GetComponent<Room>().validTiles.ContainsKey(new Vector2Int(i, j)))
                    {
                        room.GetComponent<Room>().validTiles.Add(new Vector2Int(i, j), true);
                    }
                }
            }
            currentPosition.x = 0f;
            currentPosition.z += 1f;
        }
        rooms.Add(room.GetComponent<Room>());
        return room;
    }

    //This method checks if the tile has to be a wall
    private bool isWall(int i, int j)
    {
        if (i == 0 || i == roomWidth - 1 || j == 0 || j == roomHeight - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

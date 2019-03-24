using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject roomPrefab;
    public GameObject playerPrefab;
    public GameObject exitPrefab;
    public GameObject shopPrefab;

    public Transform roomParent;

    //Keeps track of the rooms created
    public List<Room> rooms;

    private int roomWidth = 20;
    private int roomHeight = 20;

    private int biomeCount = 0;
    private Element nextBiome = Element.Fire;

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
        bool isShop;
        Element biome = ChooseBiome(out isShop);
        GameObject room;
        if (isShop)
        {
            room = Instantiate(shopPrefab, roomParent);
        }
        else
        {
            room = Instantiate(roomPrefab, roomParent);
        }
        room.GetComponent<Room>().SetupTileArray(roomWidth, roomHeight);
        room.GetComponent<Room>().biome = biome;

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
                        GameObject tile = Instantiate(wallTiles[(int)nextBiome], room.transform);
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
                    GameObject tile = Instantiate(floorTiles[(int)nextBiome], room.transform);
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

    private Element ChooseBiome(out bool isShop)
    {
        isShop = false;
        if (biomeCount == 0)
        {
            nextBiome = GenerateElement();
            biomeCount++;
            return nextBiome;
        }
        else
        {
            if (5f / biomeCount < Random.Range(0f, 1f))
            {
                biomeCount = 1;
                nextBiome = GenerateElement();
                isShop = true;
                return nextBiome;
            }
            else
            {
                biomeCount++;
                return nextBiome;
            }
        }
    }
    
    //Generates an element that is different from the nextBiome variable
    private Element GenerateElement()
    {
        Element element = (Element)Random.Range(0, 6);
        if (element == nextBiome)
        {
            return GenerateElement();
        }
        else
        {
            return element;
        }
    }
}

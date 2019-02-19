using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject roomPrefab;

    public Transform roomParent;

    //Keeps track of the rooms created
    public List<Room> rooms;

    private int roomWidth = 15;
    private int roomHeight = 15;

    void Start()
    {
        rooms = new List<Room>();
        for (int i = 0; i < 99; i++)
        {
            GameObject room = CreateRoom();
            if (i != 0)
            {
                room.SetActive(false);
            }
        }
    }
    
    private GameObject CreateRoom()
    {
        GameObject room = Instantiate(roomPrefab, roomParent);
        room.GetComponent<Room>().SetupTileArray(roomWidth, roomHeight);
        Vector3 currentPosition = Vector3.zero;
        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                if (isWall(i, j))
                {
                    GameObject tile = Instantiate(wallTile, room.transform);
                    tile.tag = "Wall";
                    tile.transform.localPosition = currentPosition + new Vector3(0f, 1f, 0f);
                    currentPosition.x += 1f;
                    room.GetComponent<Room>().tiles[i][j] = tile;
                }
                else
                {
                    GameObject tile = Instantiate(floorTile, room.transform);
                    tile.tag = "Floor";
                    tile.transform.localPosition = currentPosition;
                    currentPosition.x += 1f;
                    room.GetComponent<Room>().tiles[i][j] = tile;
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

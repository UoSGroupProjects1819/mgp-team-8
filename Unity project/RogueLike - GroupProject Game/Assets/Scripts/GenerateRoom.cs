using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject roomPrefab;

    public Transform roomParent;

    void Start()
    {
        
    }
    
    private void CreateRoom()
    {
        GameObject room = Instantiate(roomPrefab, roomParent);
    }
}

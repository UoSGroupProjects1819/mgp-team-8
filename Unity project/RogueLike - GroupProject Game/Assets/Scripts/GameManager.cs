using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Grid roomGrid;
    public Room currentRoom;
    public GenerateRoom generateRoom;
    public PlayerController player;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }

    public void RoomChanged()
    {
        if (currentRoom.roomsByExit.Count != currentRoom.exits.Count)
        {
            foreach (var exit in currentRoom.exits)
            {
                if (!currentRoom.roomsByExit.ContainsKey(exit))
                {
                    Room room = LookForNeighbor();
                    currentRoom.roomsByExit.Add(exit, room);
                    room.SetNeighbor(currentRoom);
                }
            }
        }
    }

    private Room LookForNeighbor()
    {
        for (int i = 0; i < generateRoom.rooms.Count; i++)
        {
            if (!generateRoom.rooms[i].isNeighbor)
            {
                return generateRoom.rooms[i];
            }
        }
        for (int i = 0; i < 40; i++)
        {
            GameObject room = generateRoom.CreateRoom();
            room.SetActive(false);
        }
        return LookForNeighbor();
    }
}
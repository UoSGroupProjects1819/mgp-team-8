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
    public AudioController audioController;

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

    public void SetAudioBackgroundMusic()
    {
        if (currentRoom.biome == Element.Dark)
        {
            audioController.SetAudioClip(audioController.darkBiome);
        }
        else if (currentRoom.biome == Element.Earth)
        {
            audioController.SetAudioClip(audioController.earthBiome);
        }
        else if (currentRoom.biome == Element.Fire)
        {
            audioController.SetAudioClip(audioController.fireBiome);
        }
        else if (currentRoom.biome == Element.Light)
        {
            audioController.SetAudioClip(audioController.lightBiome);
        }
        else if (currentRoom.biome == Element.Water)
        {
            audioController.SetAudioClip(audioController.waterBiome);
        }
        else
        {
            audioController.SetAudioClip(audioController.windBiome);
        }
    }

    private Room LookForNeighbor()
    {
        SetAudioBackgroundMusic();
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
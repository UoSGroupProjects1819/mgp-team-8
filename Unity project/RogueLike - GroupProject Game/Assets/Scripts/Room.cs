using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Idea about room navigation keeping track of each room's neighbor and direction
    public enum NeighborDrection
    {
        N, S, E, W
    }
    public Dictionary<NeighborDrection, Room> neighbors;

    public Grid grid { get; private set; }

    public GameObject[][] tiles;

    private void Start()
    {
        grid = GetComponentInChildren<Grid>();
        neighbors = new Dictionary<NeighborDrection, Room>();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Grid grid { get; private set; }

    private void Start()
    {
        grid = GetComponentInChildren<Grid>();
    }
}

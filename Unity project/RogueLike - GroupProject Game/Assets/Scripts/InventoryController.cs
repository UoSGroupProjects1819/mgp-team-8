using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    public GameObject inventory; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            inventory.SetActive(isShowing);
        }
    }
}
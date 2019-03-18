using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogueController : MonoBehaviour
{
    public GameObject catalogue;
    private bool isActive;

    public void OpenCanvas()
    {
        isActive = !isActive;
        catalogue.SetActive(isActive);
    }
}

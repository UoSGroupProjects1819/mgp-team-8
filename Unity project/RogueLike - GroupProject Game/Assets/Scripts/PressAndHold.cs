using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressAndHold : MonoBehaviour
{

    //public GameObject canvas;
    //public GameObject Lists;
    //private bool isActiveCanvas;
    //private bool isActiveLists;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isActiveCanvas = !isActiveCanvas;
    //    canvas.SetActive(isActiveCanvas);

    //    isActiveLists = !isActiveLists;
    //    Lists.SetActive(isActiveLists);
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    isActiveCanvas = !isActiveCanvas;
    //    canvas.SetActive(isActiveCanvas);

    //    isActiveLists = !isActiveLists;
    //    Lists.SetActive(isActiveLists);
    //}
    
    public void OnPointerDown()
    {
        StartCoroutine(startHoldTimer());
    }

    public void OnPointerUp()
    {
        StopCoroutine(startHoldTimer());
    }

    public void OnPointerExit()
    {
        StopCoroutine(startHoldTimer());
    }

    private IEnumerator startHoldTimer()
    {
        yield return new WaitForSeconds(1.5F);
        Debug.Log("button pressed");
    }
}

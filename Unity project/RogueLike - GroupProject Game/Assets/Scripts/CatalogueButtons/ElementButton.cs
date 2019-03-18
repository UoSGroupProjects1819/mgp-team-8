using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementButton : MonoBehaviour
{
    public Button Fire;
    public GameObject FirePanel;

    public Button Water;
    public GameObject WaterPanel;

    public Button Wind;
    public GameObject WindPanel;

    public Button Earth;
    public GameObject EarthPanel;

    public Button Light;
    public GameObject LightPanel;

    public Button Dark;
    public GameObject DarkPanel;

    // Start is called before the first frame update
    void Start()
    {
        FirePanel.SetActive(true);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(false);
    }
    
    public void SetFireInfo()
    {
        FirePanel.SetActive(true);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(false);
    }

    public void SetWaterInfo()
    {
        FirePanel.SetActive(false);
        WaterPanel.SetActive(true);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(false);
    }

    public void SetWindInfo()
    {
        FirePanel.SetActive(false);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(true);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(false);
    }

    public void SetEarthInfo()
    {
        FirePanel.SetActive(false);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(true);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(false);
    }

    public void SetLightInfo()
    {
        FirePanel.SetActive(false);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(true);
        DarkPanel.SetActive(false);
    }

    public void SetDarkInfo()
    {
        FirePanel.SetActive(false);
        WaterPanel.SetActive(false);
        WindPanel.SetActive(false);
        EarthPanel.SetActive(false);
        LightPanel.SetActive(false);
        DarkPanel.SetActive(true);
    }
}

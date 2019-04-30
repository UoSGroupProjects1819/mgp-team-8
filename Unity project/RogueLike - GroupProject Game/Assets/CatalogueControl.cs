using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogueControl : MonoBehaviour
{
    public TextMeshProUGUI fire1;
    public TextMeshProUGUI fire2;
    public TextMeshProUGUI fire3;
    public TextMeshProUGUI fire4;

    public TextMeshProUGUI water1;
    public TextMeshProUGUI water2;
    public TextMeshProUGUI water3;
    public TextMeshProUGUI water4;

    public TextMeshProUGUI earth1;
    public TextMeshProUGUI earth2;
    public TextMeshProUGUI earth3;
    public TextMeshProUGUI earth4;

    public TextMeshProUGUI wind1;
    public TextMeshProUGUI wind2;
    public TextMeshProUGUI wind3;
    public TextMeshProUGUI wind4;

    public TextMeshProUGUI dark1;
    public TextMeshProUGUI dark2;
    public TextMeshProUGUI dark3;
    public TextMeshProUGUI dark4;

    public TextMeshProUGUI light1;
    public TextMeshProUGUI light2;
    public TextMeshProUGUI light3;
    public TextMeshProUGUI light4;

    private int stageOne = 10;
    private int stageTwo = 20;
    private int stageThree = 30;
    private int stageFour = 40;

    private void Start()
    {
        currentFireStageText = fire1;
        currentWaterStageText = water1;
        currentDarkStageText = dark1;
        currentLightStageText = light1;
        currentWindStageText = wind1;
        currentEarthStageText = earth1;
    }

    private void Update()
    {
        UpdateFireText();
        UpdateWaterText();
        UpdateDarkText();
        UpdateLightText();
        UpdateWindText();
        UpdateEarthText();
    }

    private int fireKillsRequired = 10;
    private TextMeshProUGUI currentFireStageText;
    private void UpdateFireText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Fire];

        if (kills >= fireKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Fire] -= fireKillsRequired;
            fireKillsRequired += 10;

            if (currentFireStageText == fire1)
            {
                currentFireStageText.text = "MAX";
                currentFireStageText = fire2;
            }
            else if (currentFireStageText == fire2)
            {
                currentFireStageText.text = "MAX";
                currentFireStageText = fire3;
            }
            else if (currentFireStageText == fire3)
            {
                currentFireStageText.text = "MAX";
                currentFireStageText = fire4;
            }
            else if (currentFireStageText == fire4)
            {
                currentFireStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentFireStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Fire].ToString() + " / " + fireKillsRequired.ToString();
    }

    private int waterKillsRequired = 10;
    private TextMeshProUGUI currentWaterStageText;
    private void UpdateWaterText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Water];

        if (kills >= waterKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Water] -= waterKillsRequired;
            waterKillsRequired += 10;

            if (currentWaterStageText == water1)
            {
                currentWaterStageText.text = "MAX";
                currentWaterStageText = water2;
            }
            else if (currentWaterStageText == water2)
            {
                currentWaterStageText.text = "MAX";
                currentWaterStageText = water3;
            }
            else if (currentWaterStageText == water3)
            {
                currentWaterStageText.text = "MAX";
                currentWaterStageText = water4;
            }
            else if (currentWaterStageText == water4)
            {
                currentWaterStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentWaterStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Water].ToString() + " / " + waterKillsRequired.ToString();
    }

    private int darkKillsRequired = 10;
    private TextMeshProUGUI currentDarkStageText;
    private void UpdateDarkText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Dark];

        if (kills >= darkKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Dark] -= darkKillsRequired;
            darkKillsRequired += 10;

            if (currentDarkStageText == dark1)
            {
                currentDarkStageText.text = "MAX";
                currentDarkStageText = dark2;
            }
            else if (currentDarkStageText == dark2)
            {
                currentDarkStageText.text = "MAX";
                currentDarkStageText = dark3;
            }
            else if (currentDarkStageText == dark3)
            {
                currentDarkStageText.text = "MAX";
                currentDarkStageText = dark4;
            }
            else if (currentDarkStageText == dark4)
            {
                currentDarkStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentDarkStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Dark].ToString() + " / " + darkKillsRequired.ToString();
    }

    private int lightKillsRequired = 10;
    private TextMeshProUGUI currentLightStageText;
    private void UpdateLightText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Light];

        if (kills >= lightKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Light] -= lightKillsRequired;
            lightKillsRequired += 10;

            if (currentLightStageText == light1)
            {
                currentLightStageText.text = "MAX";
                currentLightStageText = light2;
            }
            else if (currentLightStageText == light2)
            {
                currentLightStageText.text = "MAX";
                currentLightStageText = light3;
            }
            else if (currentLightStageText == light3)
            {
                currentLightStageText.text = "MAX";
                currentLightStageText = light4;
            }
            else if (currentLightStageText == light4)
            {
                currentLightStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentLightStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Light].ToString() + " / " + lightKillsRequired.ToString();
    }

    private int windKillsRequired = 10;
    private TextMeshProUGUI currentWindStageText;
    private void UpdateWindText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Wind];

        if (kills >= windKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Wind] -= windKillsRequired;
            windKillsRequired += 10;

            if (currentWindStageText == wind1)
            {
                currentWindStageText.text = "MAX";
                currentWindStageText = wind2;
            }
            else if (currentWindStageText == wind2)
            {
                currentWindStageText.text = "MAX";
                currentWindStageText = wind3;
            }
            else if (currentWindStageText == wind3)
            {
                currentWindStageText.text = "MAX";
                currentWindStageText = wind4;
            }
            else if (currentWindStageText == wind4)
            {
                currentWindStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentWindStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Wind].ToString() + " / " + windKillsRequired.ToString();
    }

    private int earthKillsRequired = 10;
    private TextMeshProUGUI currentEarthStageText;
    private void UpdateEarthText()
    {
        int kills = GameManager.instance.player.enemiesKilledByAbility[Element.Earth];

        if (kills >= earthKillsRequired)
        {
            GameManager.instance.player.enemiesKilledByAbility[Element.Earth] -= earthKillsRequired;
            earthKillsRequired += 10;

            if (currentEarthStageText == earth1)
            {
                currentEarthStageText.text = "MAX";
                currentEarthStageText = earth2;
            }
            else if (currentEarthStageText == earth2)
            {
                currentEarthStageText.text = "MAX";
                currentEarthStageText = earth3;
            }
            else if (currentWindStageText == earth3)
            {
                currentEarthStageText.text = "MAX";
                currentEarthStageText = earth4;
            }
            else if (currentEarthStageText == earth4)
            {
                currentEarthStageText.text = "MAX";
                //TODO Give narrative item and reset catalogue
            }
        }

        currentEarthStageText.text = GameManager.instance.player.enemiesKilledByAbility[Element.Earth].ToString() + " / " + earthKillsRequired.ToString();
    }
}

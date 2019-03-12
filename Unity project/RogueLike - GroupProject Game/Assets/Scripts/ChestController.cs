using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Runes
{
    Cooldown, Strength, Defense, Speed, Vitality
}

public class ChestController : MonoBehaviour
{
    public Image cooldownRune;
    public Image strengthRune;
    public Image defenseRune;
    public Image speedRune;
    public Image vitalityRune;

    public GameObject chest;

    // Start is called before the first frame update
    void Start()
    {
        chest.SetActive(false);

        cooldownRune.enabled = false;
        strengthRune.enabled = false;
        defenseRune.enabled = false;
        speedRune.enabled = false;
        vitalityRune.enabled = false;

        int runeNumber = Random.Range(1, 2);
        for (int i = 0; i < runeNumber; i++)
        {
            //SpawnRune();
        }
    }

    public void SpawnRune()
    {
        //Spawn Randomly a type of rune in the canvas
    }

    // Update is called once per frame
    void Update()
    { 
        //foreach ()      //check if all enemies are not alive
        {
            //chest.SetActive(true);
        }

    }
}

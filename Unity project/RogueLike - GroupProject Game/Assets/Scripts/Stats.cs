using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int strength { get; private set; }
    public int vitality { get; private set; }
    public int defense { get; private set; }

    public Stats(int strength, int vitality, int defense)
    {
        this.strength = strength;
        this.vitality = vitality;
        this.defense = defense;
    }

    public void SetStr(int newStr)
    {
        this.strength = newStr;
    }
    
    public void SetVit(int newVit)
    {
        this.vitality = newVit;
    }

    public void SetDef(int newDef)
    {
        this.defense = newDef;
    }
}

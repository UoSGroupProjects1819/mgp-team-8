using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Fire, Water, Wind, Earth, Light, Dark
}

public class Enemy
{
    private Stats stats;
    public float maxHp;
    public float currentHp;
    public float damage;
    public Element element;

    public Enemy(int str, int vit, int def, Element element)
    {
        stats = new Stats(str, vit, def);
        maxHp = stats.vitality + Random.Range(80f, 170f);
        currentHp = maxHp;
        damage = stats.strength + Random.Range(10f, 35f);
        this.element = element;
    }
}

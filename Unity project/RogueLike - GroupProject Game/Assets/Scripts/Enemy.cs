using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Fire, Water, Wind, Earth, Light, Dark
}

public class Enemy
{
    public Stats stats;
    public float maxHp;
    public float currentHp;
    public float damage;
    public Element element;
    public Ability ability;

    public Enemy(int str, int vit, int def, Element element)
    {
        stats = new Stats(str, vit, def);
        maxHp = stats.vitality + Random.Range(80f, 170f);
        currentHp = maxHp;
        damage = stats.strength + Random.Range(10f, 35f);
        this.element = element;
        this.ability = new Ability(0.5f, 2f, 5f);
    }
}

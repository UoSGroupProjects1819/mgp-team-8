using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public float cooldown;
    public float range;
    public float damage;

    public Ability(float cooldown, float range, float damage)
    {
        this.cooldown = cooldown;
        this.range = range;
        this.damage = damage;
    }
}

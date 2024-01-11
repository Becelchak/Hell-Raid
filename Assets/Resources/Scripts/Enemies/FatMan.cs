using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMan : Enemies
{
    int hpRegen = 10;

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        Health += hpRegen;
    }
}

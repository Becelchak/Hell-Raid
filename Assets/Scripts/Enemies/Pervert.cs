using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pervert : Enemies
{
    private readonly float maxSpeed = 1.8f;
    private readonly float deltaSpeed = 0.1f;

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        if (speed != maxSpeed)
            speed += deltaSpeed;
    }
}

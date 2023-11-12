using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePriest : Enemies
{
    public override void Attack()
    {
        base.Attack();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(attackDamage);
        }
    }
}

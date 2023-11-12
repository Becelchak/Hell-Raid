using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemies
{
    // Start is called before the first frame update
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

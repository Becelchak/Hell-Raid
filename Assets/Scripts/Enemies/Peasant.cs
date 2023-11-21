using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Peasant : Enemies
{
    public override void Attack()
    {
        base.Attack();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // print(this.gameObject);
        // print(other.tag);
        if (other.CompareTag("PlayerHitBox"))
        {
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(Weapon.damage);
        }
    }
}
